using Newtonsoft.Json;
using ManjDb.DataModels;
using Microsoft.Extensions.Options;

namespace ManjDb.TcLib
{
    public class TcApi
    {
        #region Properties
        private readonly HttpClient httpClient;
        private readonly string baseUrl;
        private readonly string apiToken;
        private readonly TcApiOptions _tcApiOptions;
        #endregion

        public TcApi(HttpClient httpClient, string baseUrl, IOptions<TcApiOptions> tcApiOptions)
        {
            if (baseUrl == null)
            {
                throw new ArgumentNullException(nameof(baseUrl), "baseUrl cannot be null");
            }
            if (tcApiOptions.Value == null || string.IsNullOrEmpty(tcApiOptions.Value.AuthToken))
            {
                throw new ArgumentNullException(nameof(tcApiOptions), "API token cannot be null or empty");
            }

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient cannot be null");
            _tcApiOptions = tcApiOptions.Value;
            this.baseUrl = baseUrl.TrimEnd('/');
            this.apiToken = _tcApiOptions.AuthToken;
            this.httpClient.DefaultRequestHeaders.Add("X-TransparentClassroomToken", apiToken);
        }

        public async Task<T> GetApiResponse<T>(string endpoint, CancellationToken cancellationToken = default!) where T : notnull
        {
            try
            {
                string url = $"{baseUrl}/{endpoint}";
                HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
#pragma warning disable CA2016 // Forward the 'CancellationToken' parameter to methods that take one

                    string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

#pragma warning restore CA2016

                    // Deserialize with custom converter
                    JsonSerializerSettings settings = new();

                    if (JsonConvert.DeserializeObject<T>(responseBody, settings) == null)
                    {
                        throw new JsonSerializationException("Deserialization returned null.");
                    }

                    return JsonConvert.DeserializeObject<T>(responseBody, settings)!;
                }
                else
                {
                    throw new HttpRequestException($"Failed to retrieve API response. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"API request failed: {ex.Message}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new Exception($"JSON deserialization failed: {ex.Message}", ex);
            }
        }

        public async Task<string> GetRawJsonResponse(string endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                string url = $"{baseUrl}/{endpoint}";
                HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw new HttpRequestException($"Failed to retrieve API response. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"API request failed: {ex.Message}", ex);
            }
        }

        public async Task<string> GetRawFormData(int ChildId, CancellationToken cancellationToken = default)
        {
            return await GetRawJsonResponse($"forms.json?child_id={ChildId}", cancellationToken);
        }

        public async Task<List<ChildInfo>> GetAllChildrenInfo()
        {
            List<ChildInfo> childrenInfoList = await GetApiResponse<List<ChildInfo>>("children.json");
            return childrenInfoList;
        }

        public async Task<List<Classrooms>> GetClassrooms()
        {
            List<Classrooms> classroomsList = await GetApiResponse<List<Classrooms>>("classrooms.json");
            return classroomsList;
        }

        public async Task<FormTemplateIds> GetFormTemplateIds(CancellationToken cancellationToken = default)
        {
            var formTemplates = await GetApiResponse<List<dynamic>>("form_templates.json", cancellationToken);
            FormTemplateIds formTemplateIds = new FormTemplateIds();
            int targetFormId = 49600;
            foreach (var formTemplate in formTemplates)
            {
                if (formTemplate.id == targetFormId)
                {
                    foreach (var widget in formTemplate.widgets)
                    {
                        if (widget.type == "FormItem")
                        {
                            string title = widget.title;
                            int formType = widget.form_type;

                            switch (title)
                            {
                                case "Enrollment Contract":
                                    formTemplateIds.EnrollmentContractId = formType;
                                    break;
                                case "Emergency Information":
                                    formTemplateIds.EmergencyInformationId = formType;
                                    break;
                                case "Authorized Pick Up List":
                                    formTemplateIds.ApprovedPickupId = formType;
                                    break;
                                case "Photo Release":
                                    formTemplateIds.PhotoReleaseId = formType;
                                    break;
                                case "Animal Permission Slip":
                                    formTemplateIds.AnimalPermissionId = formType;
                                    break;
                                case "Going Out":
                                    formTemplateIds.GoingOutPermissionId = formType;
                                    break;
                            }
                        }
                    }
                    break;
                }
            }

            return formTemplateIds;
        }

        public async Task UpdateChildInfoWithForms(ChildInfo childInfo, CancellationToken cancellationToken)
        {
            string rawJson = await GetRawFormData(childInfo.ChildId, cancellationToken);
            childInfo.FormsRawJson = rawJson;
        }
    }
}