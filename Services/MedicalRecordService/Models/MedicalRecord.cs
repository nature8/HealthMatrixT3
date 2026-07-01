using System.ComponentModel.DataAnnotations;

namespace MedicalRecordService.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Diagnosis { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Prescription { get; set; }

        [MaxLength(1000)]
        public string? MedicalNotes { get; set; }

        public DateTime VisitDate { get; set; }
    }
}