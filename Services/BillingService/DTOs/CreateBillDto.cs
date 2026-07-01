using System.ComponentModel.DataAnnotations;

namespace BillingService.DTOs
{
    public class CreateBillDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; }
    }
}