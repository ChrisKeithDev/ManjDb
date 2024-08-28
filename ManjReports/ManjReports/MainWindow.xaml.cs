using System;
using System.Threading.Tasks;
using System.Windows;
using ManjTables.DataModels;
using ManjTables.DataModels.Models;

namespace ManjReports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        // The following properties will be used to interface with your database context and business logic.
        private bool isProgrammaticCheckChange = false;
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Now;
            LoadClassroomsAsync();

            cbAllClassrooms.IsChecked = true;
            cbAllFiles.IsChecked = true;
        }

        private async void btnBuildSelected_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = ((MainWindowViewModel)DataContext).SelectedDate;
            int classroomId = GetSelectedClassroomId();

            // Stub for new report generation process
            // You will replace this with calls to your business logic layer.
            await GenerateReportsForSelection(selectedDate, classroomId);
        }

        private async Task GenerateReportsForSelection(DateTime selectedDate, int classroomId)
        {
            // This method should be replaced with your business logic that interacts with the database.
            // The logic should:
            // 1. Determine which reports need to be generated based on the UI selection.
            // 2. Call the appropriate methods in your business logic layer to generate these reports.
            // 3. Save the generated reports to the filesystem or provide them directly to the user.

            // Example stub for report generation:
            // var reportsGenerator = new ReportsGenerator();
            // await reportsGenerator.GenerateReportsAsync(selectedDate, classroomId, GetReportOptions());

            MessageBox.Show("Files generated successfully!");
        }

        private ReportOptions GetReportOptions()
        {
            // This method should return an object that contains options for report generation
            // based on the UI checkboxes. You'll have to define the ReportOptions class
            // according to what options you need.
            return new ReportOptions
            {
                IncludePickup = cbPickup.IsChecked == true,
                IncludeEmergency = cbEmergency.IsChecked == true,
                IncludeAllergies = cbAllergies.IsChecked == true,
                // ... Add other options based on checkboxes
            };
        }

        private async Task LoadClassrooms()
        {
            // Replace this method to load classrooms from your SQLite database
            // using Entity Framework Core instead of making an HTTP request.
            // Example:
            // using (var context = new SchoolContext())
            // {
            //     classrooms = await context.Classrooms.ToListAsync();
            // }
        }

        private async void LoadClassroomsAsync()
        {
            await LoadClassrooms();
        }

        private int GetSelectedClassroomId()
        {
            // This method would get the ID of the selected classroom from the database.
            // You would adjust this to work with your new data models and database context.
        }

        // The checkbox event handlers below may remain the same, but ensure they work well
        // with your new logic for handling selections.

        private void OnCheckedAllClassrooms(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnCheckedClassroom(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnUncheckedClassroom(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnCheckedAllFiles(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnCheckedFile(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnUncheckedFile(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnUncheckedAllClassrooms(object sender, RoutedEventArgs e) { /* ... */ }
        private void OnUncheckedAllFiles(object sender, RoutedEventArgs e) { /* ... */ }
    }

}
