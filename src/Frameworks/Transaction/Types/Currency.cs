namespace Transaction.Framework.Types
{
    using System.ComponentModel;

    public enum Currency
    {
        Unknown = 0,

        [Description("United States dollar")]
        USD = 840,

        [Description("Pound sterling")]
        GBP = 826,

        [Description("Euro")]
        EUR = 978,

        [Description("Indian rupee")]
        INR = 356
    }
}
