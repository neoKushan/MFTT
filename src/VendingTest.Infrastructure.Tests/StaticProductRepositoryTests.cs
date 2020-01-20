namespace VendingTest.Infrastructure.Tests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using Xunit;

    public class StaticProductRepositoryTests
    {
        [Fact]
        public void StaticProductRepository_GetAllProducts_ReturnsListOfProducts()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            var sut = fixture.Create<StaticProductRepository>();

            var results = sut.GetAllProducts();

            Assert.NotEmpty(results);
        }
    }
}
