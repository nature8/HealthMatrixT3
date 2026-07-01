using System.ComponentModel.DataAnnotations;

namespace MedicalRecordService.DTOs
{
    public class CreateMedicalRecordDto
    {
        [Required(ErrorMessage = "Patient Id is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor Id is required")]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(500)]
        public string Diagnosis { get; set; }

        [Required]
        [StringLength(1000)]
        public string Prescription { get; set; }

        [StringLength(1000)]
        public string? MedicalNotes { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }
    }
}