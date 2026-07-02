namespace NotificationService.Events
{
    public class BillGeneratedEvent
    {
        public int BillId { get; set; }

        public string PatientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Amount { get; set; }
    }
}