using System.ComponentModel.DataAnnotations;

namespace ManjDb.DataModels
{
    public class FormTemplateIds
    {
        [Key]
        public int Id { get; set; }
        public int EnrollmentContractId { get; set; }
        public int EmergencyInformationId { get; set; }
        public int ApprovedPickupId { get; set; }
        public int PhotoReleaseId { get; set; }
        public int AnimalPermissionId { get; set; }
        public int GoingOutPermissionId { get; set; }
    }
}
