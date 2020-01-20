namespace VendingTest.Core.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface ICoinRepository
    {
        IEnumerable<CoinBin> GetAllCoinBins();
        IEnumerable<ValidCoin> GetAcceptedCoins();
    }
}
