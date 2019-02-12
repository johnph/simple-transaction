namespace Transaction.Framework.Types
{
    public static class TransactionErrorCode
    {
        public const int AccountDoesNotExistError = 1001;
        public const int InsufficientBalance = 1002;
        public const int InvalidAmount = 1003;
        public const int InvalidCurrencyError = 1004;
        public const int CurrencyMismatchError = 1005;
    }
}
