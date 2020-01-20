namespace VendingTest.Core.Tests
{
    using System.Collections.Generic;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Interfaces;
    using Models;
    using Moq;
    using Xunit;

    public class CoinCheckerTests
    {
        [Fact]
        public void CoinChecker_ValidCoinInserted_ReturnsValidCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var checker = fixture.Freeze<Mock<ICoinRepository>>();

            checker.Setup(x => x.GetAcceptedCoins()).Returns(
                new List<ValidCoin>() { ValidCoin.Nickel });

            var sut = fixture.Create<CoinChecker>();

            var insertedCoin = new InsertedCoin()
            {
                Diameter = ValidCoin.Nickel.Diameter,
                Weight = ValidCoin.Nickel.Weight
            };

            var result = sut.CheckCoin(insertedCoin);

            Assert.Equal(ValidCoin.Nickel, result);
        }

        [Fact]
        public void CoinChecker_UnacceptedCoinInserted_ReturnsUnknownCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var repository = fixture.Freeze<Mock<ICoinRepository>>();

            repository.Setup(x => x.GetAcceptedCoins()).Returns(
                new List<ValidCoin>() { ValidCoin.Dime });

            var sut = fixture.Create<CoinChecker>();

            var insertedCoin = new InsertedCoin()
            {
                Diameter = ValidCoin.Nickel.Diameter,
                Weight = ValidCoin.Nickel.Weight
            };

            var result = sut.CheckCoin(insertedCoin);

            Assert.Equal(ValidCoin.Unknown, result);
        }

        [Fact]
        public void CoinChecker_InvalidCoinInserted_ReturnsUnknownCoin()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var repository = fixture.Freeze<Mock<ICoinRepository>>();

            repository.Setup(x => x.GetAcceptedCoins()).Returns(
                new List<ValidCoin>() { ValidCoin.Dime });

            var sut = fixture.Create<CoinChecker>();

            var insertedCoin = new InsertedCoin()
            {
                Diameter = 200f,
                Weight = 100f
            };

            var result = sut.CheckCoin(insertedCoin);

            Assert.Equal(ValidCoin.Unknown, result);
        }
    }
}
