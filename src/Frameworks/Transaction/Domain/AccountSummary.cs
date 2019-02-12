namespace Transaction.Framework.Domain
{
    using Transaction.Framework.Types;

    public class AccountSummary
    {
        public int AccountNumber { get; set; }
        public Money Balance { get; set; }
    }
}
