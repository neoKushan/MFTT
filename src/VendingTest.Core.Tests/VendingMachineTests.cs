namespace VendingTest.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Interfaces;
    using Models;
    using Moq;
    using Xunit;

    public class VendingMachineTests
    {
        [Fact]
        public void VendingMachine_ValidCoinInserted_DisplaysValueOfCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var repository = fixture.Freeze<Mock<ICoinRepository>>();

            checker.Setup(x => x.CheckCoin(It.IsAny<InsertedCoin>())).Returns(ValidCoin.Nickel);

            repository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() {Amount = 25, CoinType = coin}));

            var sut = fixture.Create<VendingMachine>();

            var insertedCoin = new InsertedCoin()
            {
                Diameter = ValidCoin.Nickel.Diameter,
                Weight = ValidCoin.Nickel.Weight
            };

            var result = sut.InsertCoin(insertedCoin);

            Assert.Equal($"{result.Value:C}", sut.CoinDisplay);
        }

        [Fact]
        public void VendingMachine_MultipleValidCoinInserted_DisplaysValueOfCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var repository = fixture.Freeze<Mock<ICoinRepository>>();

            var insertedCoin1 = new InsertedCoin()
            {
                Diameter = ValidCoin.Nickel.Diameter,
                Weight = ValidCoin.Nickel.Weight
            };

            var insertedCoin2 = new InsertedCoin()
            {
                Diameter = ValidCoin.Dime.Diameter,
                Weight = ValidCoin.Dime.Weight
            };

            checker.Setup(x => x.CheckCoin(It.Is<InsertedCoin>(x => Math.Abs(x.Weight - insertedCoin1.Weight) < 0.1f))).Returns(ValidCoin.Nickel);
            checker.Setup(x => x.CheckCoin(It.Is<InsertedCoin>(x => Math.Abs(x.Weight - insertedCoin2.Weight) < 0.1f))).Returns(ValidCoin.Dime);

            repository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin }));

            var sut = fixture.Create<VendingMachine>();

            sut.InsertCoin(insertedCoin1);
            sut.InsertCoin(insertedCoin2);

            Assert.Equal($"{ValidCoin.Nickel.Value + ValidCoin.Dime.Value:C}", sut.CoinDisplay);
        }

        [Fact]
        public void VendingMachine_InValidCoinInserted_DisplaysInsertCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var repository = fixture.Freeze<Mock<ICoinRepository>>();

            checker.Setup(x => x.CheckCoin(It.IsAny<InsertedCoin>())).Returns(ValidCoin.Unknown);

            repository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin }));

            var sut = fixture.Create<VendingMachine>();

            var insertedCoin = new InsertedCoin()
            {
                Diameter = 100f,
                Weight = 100f
            };

            sut.InsertCoin(insertedCoin);

            Assert.Equal("Insert Coin", sut.CoinDisplay);
        }

        [Fact]
        public void VendingMachine_VendProduct_CoinBinReduced()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var coinRepository = fixture.Freeze<Mock<ICoinRepository>>();
            var productRepository = fixture.Freeze<Mock<IProductRepository>>();
            var insertedCoin = fixture.Create<InsertedCoin>();

            checker.Setup(x => x.CheckCoin(It.IsAny<InsertedCoin>())).Returns(ValidCoin.Dollar);

            coinRepository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin }));

            productRepository.Setup(x => x.GetAllProducts())
                .Returns(new List<ProductBin>()
                {
                    new ProductBin() {ProductType = new Product() {Name = "Doritos"}, Cost = 1.00m, Amount = 10},
                });

            var sut = fixture.Create<VendingMachine>();

            sut.InsertCoin(insertedCoin);
            var vendStatus = sut.Vend("Doritos");

            Assert.Equal(VendStatus.Successful, vendStatus);
            Assert.Equal("Insert Coin", sut.CoinDisplay);
        }

        [Fact]
        public void VendingMachine_VendProductSoldOut_CoinBinUnchanged()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var coinRepository = fixture.Freeze<Mock<ICoinRepository>>();
            var productRepository = fixture.Freeze<Mock<IProductRepository>>();
            var insertedCoin = fixture.Create<InsertedCoin>();

            checker.Setup(x => x.CheckCoin(It.IsAny<InsertedCoin>())).Returns(ValidCoin.Dollar);

            coinRepository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin }));

            productRepository.Setup(x => x.GetAllProducts())
                .Returns(new List<ProductBin>()
                {
                    new ProductBin() {ProductType = new Product() {Name = "Doritos"}, Cost = 1.00m, Amount = 0},
                });

            var sut = fixture.Create<VendingMachine>();

            sut.InsertCoin(insertedCoin);
            var vendStatus = sut.Vend("Doritos");

            Assert.Equal(VendStatus.SoldOut, vendStatus);
            Assert.Equal($"{ValidCoin.Dollar.Value:C}", sut.CoinDisplay);
        }

        [Fact]
        public void VendingMachine_VendProductInsufficientFunds_CoinBinUnchanged()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinChecker>>();
            var coinRepository = fixture.Freeze<Mock<ICoinRepository>>();
            var productRepository = fixture.Freeze<Mock<IProductRepository>>();
            var insertedCoin = fixture.Create<InsertedCoin>();

            checker.Setup(x => x.CheckCoin(It.IsAny<InsertedCoin>())).Returns(ValidCoin.Dollar);

            coinRepository.Setup(x => x.GetAllCoinBins())
                .Returns(ValidCoin.List().Select(coin => new CoinBin() { Amount = 25, CoinType = coin }));

            productRepository.Setup(x => x.GetAllProducts())
                .Returns(new List<ProductBin>()
                {
                    new ProductBin() {ProductType = new Product() {Name = "Doritos"}, Cost = 2.00m, Amount = 1},
                });

            var sut = fixture.Create<VendingMachine>();

            sut.InsertCoin(insertedCoin);
            var vendStatus = sut.Vend("Doritos");

            Assert.Equal(VendStatus.InsufficientFunds, vendStatus);
            Assert.Equal($"{ValidCoin.Dollar.Value:C}", sut.CoinDisplay);
        }
    }
}
