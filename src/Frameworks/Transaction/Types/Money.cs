namespace Transaction.Framework.Types
{
    using Transaction.Framework.Exceptions;

    public struct Money 
    {
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money ToAmount(decimal amount) => new Money(amount, Currency);

        public static Money operator +(decimal d, Money m)
        {
            return new Money(m.Amount + d, m.Currency);
        }

        public static Money operator +(Money m, decimal d)
        {
            return d + m;
        }

        public static Money operator -(Money m, decimal d)
        {
            return new Money(m.Amount - d, m.Currency);
        }

        public static bool operator >(Money m, decimal d)
        {
            return m.Amount > d;
        }

        public static bool operator <(Money m, decimal d)
        {
            return m.Amount < d;
        }

        public static bool operator >=(Money m, decimal d)
        {
            return m.Amount >= d;
        }

        public static bool operator <=(Money m, decimal d)
        {
            return m.Amount <= d;
        }

        public static Money operator +(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1 + m2.Amount;
        }

        public static Money operator -(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1 - m2.Amount;
        }

        public static bool operator >(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount > m2.Amount;
        }

        public static bool operator <(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount < m2.Amount;
        }

        public static bool operator >=(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount >= m2.Amount;
        }

        public static bool operator <=(Money m1, Money m2)
        {
            RequireSameCurrency(m1, m2);
            return m1.Amount <= m2.Amount;
        }

        private static void RequireSameCurrency(Money m1, Money m2)
        {
            if (!m1.Currency.Equals(m2.Currency))
                throw new CurrencyMismatchException(m1.Currency, m2.Currency);
        }
    }
}
