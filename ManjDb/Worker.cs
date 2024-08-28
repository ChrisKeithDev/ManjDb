using ManjDb.DataModels;
using ManjDb.TcLib;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ManjDb
{
    public class Worker : BackgroundService
    {
        #region Properties
        private readonly ILogger<Worker> _logger;
        private readonly TcApi _tcApi;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        int _entriesCount = 0;
        #endregion

        public Worker(ILogger<Worker> logger, TcApi tcApi, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _logger = logger;
            _tcApi = tcApi;
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _ = _configuration["Database:DbPath"];
                string? absoluteDbPath = _configuration["Database:AbsoluteDbPath"];
                string? monitorLogFilePath = _configuration["MonitorLogFile"];
                string? controlLogFilePath = _configuration["ControlLogFile"];
                DateTime startTime = DateTime.Now;

                using (var scope = _scopeFactory.CreateScope())
                {
                    var myDbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                    try
                    {
                        // Clear existing ChildInfo entries
                        myDbContext.ChildInfos.RemoveRange(myDbContext.ChildInfos);
                        await myDbContext.SaveChangesAsync(stoppingToken);

                        // Remove old classrooms data
                        myDbContext.Classrooms.RemoveRange(myDbContext.Classrooms);
                        await myDbContext.SaveChangesAsync(stoppingToken);

                        // Get the forms template ids
                        FormTemplateIds formTemplateIds = await _tcApi.GetFormTemplateIds(stoppingToken);
                        myDbContext.FormTemplateIds.Add(formTemplateIds);
                        await myDbContext.SaveChangesAsync(stoppingToken);

                        // Fetch and populate ChildInfos table
                        List<ChildInfo> childrenInfoList = await _tcApi.GetAllChildrenInfo();

                        // Get the classrooms
                        List<Classrooms> classrooms = await _tcApi.GetClassrooms();

                        // Add new classrooms data
                        myDbContext.Classrooms.AddRange(classrooms);
                        await myDbContext.SaveChangesAsync(stoppingToken);

                        // Update ChildInfos table with forms data
                        foreach (var childInfo in childrenInfoList)
                        {
                            await _tcApi.UpdateChildInfoWithForms(childInfo, stoppingToken);
                            myDbContext.ChildInfos.Add(childInfo);
                            _entriesCount++;
                        }

                        await myDbContext.SaveChangesAsync(stoppingToken);

                        // Stop the timer and calculate total time taken
                        DateTime endTime = DateTime.Now;
                        TimeSpan timeTaken = endTime - startTime;

                        // Writing to monitoring log file
                        string logMessage = $"Timestamp: {startTime}, Duration: {timeTaken.TotalSeconds}s, Success: Yes, Entries: {_entriesCount}\n";
                        if (!string.IsNullOrEmpty(monitorLogFilePath))
                        {
                            File.AppendAllText(monitorLogFilePath, logMessage);
                        }
                        else
                        {
                            _logger.LogWarning("Monitor log file path is not configured.");
                        }

                        // Writing to control log file
                        if (!string.IsNullOrEmpty(controlLogFilePath))
                        {
                            File.WriteAllText(controlLogFilePath, $"Timestamp: {DateTime.Now}, DBPath: {absoluteDbPath}");
                        }
                        else
                        {
                            _logger.LogWarning("Control log file path is not configured.");
                        }

                        // Clear existing FormInfo entries
                        _entriesCount = 0;
                    }
                    catch (Exception ex)
                    {
                        // Stop the timer and calculate total time taken
                        DateTime endTime = DateTime.Now;
                        TimeSpan timeTaken = endTime - startTime;

                        // Writing to monitoring log file
                        string logMessage = $"Timestamp: {startTime}, Duration: {timeTaken.TotalSeconds}s, Success: No, ErrorMessage: {ex.Message}\n";
                        if (!string.IsNullOrEmpty(monitorLogFilePath))
                        {
                            File.AppendAllText(monitorLogFilePath, logMessage);
                        }
                        else
                        {
                            _logger.LogWarning("Monitor log file path is not configured.");
                        }

                        _logger.LogError(ex, "Error syncing raw data.");

                        // Clear existing FormInfo entries
                        _entriesCount = 0;
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
