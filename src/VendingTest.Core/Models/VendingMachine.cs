namespace VendingTest.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class VendingMachine
    {
        public List<CoinBin> CoinBins { get; }
        public List<ProductBin> ProductBins { get; }

        private readonly ICoinChecker coinChecker;

        private decimal currentCoinValue;

        public VendingMachine(ICoinChecker coinChecker, ICoinRepository coinRepository, IProductRepository productRepository)
        {
            this.coinChecker = coinChecker;
            this.CoinBins = coinRepository.GetAllCoinBins().ToList();
            this.ProductBins = productRepository.GetAllProducts().ToList();
        }

        public ValidCoin InsertCoin(InsertedCoin newCoin)
        {
            var coinType = this.coinChecker.CheckCoin(newCoin);
            if (coinType == ValidCoin.Unknown)
            {
                return ValidCoin.Unknown;
            }

            this.currentCoinValue += coinType.Value;
            this.CoinBins.First(x => x.CoinType == coinType).Amount++;

            return coinType;
        }

        public VendStatus Vend(string product)
        {
            var selectedProduct = this.ProductBins.FirstOrDefault(x =>
                string.Compare(x.ProductType.Name, product, StringComparison.CurrentCultureIgnoreCase) == 0) ?? new ProductBin();

            if (selectedProduct.ProductType.Name == "Unknown" || selectedProduct.Amount < 1)
            {
                return VendStatus.SoldOut;
            }

            if (selectedProduct.Cost > this.currentCoinValue)
            {
                return VendStatus.InsufficientFunds;
            }

            this.currentCoinValue -= selectedProduct.Cost;
            selectedProduct.Amount--;

            return VendStatus.Successful;
        }

        public void Refund() => throw new NotImplementedException();

        public string CoinDisplay => this.currentCoinValue > 0 ? $"{this.currentCoinValue:C}" : "Insert Coin";
    }
}
