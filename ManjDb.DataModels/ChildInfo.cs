using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManjDb.DataModels
{
    public class ChildInfo
    {
        
        [JsonProperty("id")]
        [Key]
        public int ChildId { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("profile_photo")]
        public string? ProfilePhoto { get; set; }

        [JsonProperty("birth_date")]
        public string? BirthDate { get; set; }

        [JsonProperty("middle_name")]
        public string? MiddleName { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("hours_string")]
        public string? HoursString { get; set; }

        [JsonProperty("dominant_language")]
        public string? DominantLanguage { get; set; }

        [JsonProperty("allergies")]
        public string? Allergies { get; set; }

        [JsonProperty("program")]
        public string? Program { get; set; }

        [JsonProperty("first_day")]
        public string? FirstDay { get; set; }

        [JsonProperty("last_day")]
        public string? LastDay { get; set; }

        [JsonProperty("approved_adults_string")]
        public string? ApprovedAdultsString { get; set; }

        [JsonProperty("emergency_contacts_string")]
        public string? EmergencyContactsString { get; set; }

        [JsonProperty("classroom_ids")]
        [NotMapped] // Can't use array of ints in SQLite so I'm using a string instead
        public int[]? ClassroomIds { get; set; }

        public string? ClassroomIdsString
        {
            get => ClassroomIds != null ? string.Join(",", ClassroomIds) : null;
            set => ClassroomIds = value?.Split(',').Select(int.Parse).ToArray();
        }

        [JsonProperty("parent_ids")]
        [NotMapped]
        public int[]? ParentIds { get; set; }
        public string? ParentIdsString
        {
            get => ParentIds != null ? string.Join(",", ParentIds) : null;
            set => ParentIds = value?.Split(',').Select(int.Parse).ToArray();
        }

        // This is for storing raw JSON from forms
        public string? FormsRawJson { get; set; }
    }
}
