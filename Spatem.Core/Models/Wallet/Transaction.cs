namespace Spatem.Core.Models
{
    public class Transaction
    {
        public uint TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string ParticipantName { get; set; }
        public string Description { get; set; }

        public virtual Transaction TransactionRef { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}