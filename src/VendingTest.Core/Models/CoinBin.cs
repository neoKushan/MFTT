namespace VendingTest.Core.Models
{
    public class CoinBin
    {
        public ValidCoin CoinType { get; set; } = ValidCoin.Unknown;
        public int Amount { get; set; } = 0;
    }
}
