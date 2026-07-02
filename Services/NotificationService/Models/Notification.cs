namespace NotificationService.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public DateTime SentDate { get; set; }
    }
}