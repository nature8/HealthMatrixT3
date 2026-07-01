using System.ComponentModel.DataAnnotations;

namespace BillingService.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}