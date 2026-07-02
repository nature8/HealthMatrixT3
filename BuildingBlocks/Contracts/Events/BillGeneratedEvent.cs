namespace Contracts.Events
{
    public class BillGeneratedEvent
    {
        public int BillId { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
    }
}