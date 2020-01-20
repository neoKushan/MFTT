namespace VendingTest.Infrastructure.Tests
{
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Core.Models;
    using Xunit;

    public class StaticCoinRepositoryTests
    {
        [Fact]
        public void StaticCoinRepository_GetAllCoinBins_ReturnsListOfCoins()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var sut = fixture.Create<StaticCoinRepository>();

            var results = sut.GetAllCoinBins();

            Assert.Equal(ValidCoin.List().Count(x => x != ValidCoin.Unknown), results.Count());
        }

        [Fact]
        public void StaticCoinRepository_GetAcceptedCoins_ReturnsNickelsAndDimes()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var sut = fixture.Create<StaticCoinRepository>();

            var results = sut.GetAcceptedCoins();

            var collection = results as ValidCoin[] ?? results.ToArray();

            Assert.Equal(2, collection.Count());
            Assert.Contains(ValidCoin.Nickel, collection);
            Assert.Contains(ValidCoin.Dime, collection);
        }
    }
}
