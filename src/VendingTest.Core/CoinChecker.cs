namespace VendingTest.Core
{
    using System;
    using System.Linq;
    using Interfaces;
    using Models;

    public class CoinChecker : ICoinChecker
    {
        private readonly ICoinRepository coinRepository;
        private const float Tolerance = 0.01f;

        public CoinChecker(ICoinRepository coinRepository) => this.coinRepository = coinRepository;

        public ValidCoin CheckCoin(InsertedCoin coin) =>
            this.coinRepository.GetAcceptedCoins().FirstOrDefault(x =>
                NearlyEqual(x.Diameter, coin.Diameter) && NearlyEqual(x.Weight, coin.Weight)) ?? ValidCoin.Unknown;

        private static bool NearlyEqual(float a, float b) => Math.Abs(a - b) < Tolerance;
    }
}
