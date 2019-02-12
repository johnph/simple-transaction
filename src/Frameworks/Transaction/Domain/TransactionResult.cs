namespace Transaction.Framework.Domain
{
    using Transaction.Framework.Types;

    public class TransactionResult
    {
        public int AccountNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public Money Balance { get; set; }
        public string Message { get; set; }        
    }
}
