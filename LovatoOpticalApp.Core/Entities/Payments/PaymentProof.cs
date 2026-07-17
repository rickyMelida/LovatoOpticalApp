namespace LovatoOpticalApp.Core.Entities.Payments
{
    public class PaymentProof
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FileName { get; private set; }
        public string FileUrl { get; private set; }
        public DateTime ReceivedAt { get; private set; } = DateTime.UtcNow;
        public bool IsVerified { get; private set; } = false;

        private PaymentProof() { }

        public PaymentProof(string fileName, string fileUrl)
        {
            FileName = fileName;
            FileUrl = fileUrl;
        }

        public void Verify() => IsVerified = true;
    }
}
