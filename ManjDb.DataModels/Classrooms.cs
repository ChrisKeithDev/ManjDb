using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ManjDb.DataModels
{
    public class Classrooms
    {
        [JsonProperty("id")]
        [Key]
        public int ClassroomId { get; set; }
        [JsonProperty("name")]
        public string? ClassroomName { get; set; }
        [JsonProperty("lesson_set_id")]
        public int LessonSetId { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("level")]
        public string? Level { get; set; }
    }
}
