namespace VendingTest.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Interfaces;
    using Core.Models;

    // This is a static or Hardcoded repository that loads the machine with 25 of each coin type
    // You could replace this with a Database or file loader for persistence
    public class StaticCoinRepository : ICoinRepository
    {
        public IEnumerable<CoinBin> GetAllCoinBins() => ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin } );

        public IEnumerable<ValidCoin> GetAcceptedCoins()
        {
            yield return ValidCoin.Dime;
            yield return ValidCoin.Nickel;
        }
    }
}
