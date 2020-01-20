namespace VendingTest.Infrastructure
{
    using System.Collections.Generic;
    using Core.Interfaces;
    using Core.Models;

    // This is a static or Hardcoded repository that loads the machine with Several products
    // You could replace this with a Database or file loader for persistence
    public class StaticProductRepository : IProductRepository
    {
        private readonly List<ProductBin> products = new List<ProductBin>()
        {
            new ProductBin() {ProductType = new Product() {Name = "Doritos"}, Cost = 0.60m, Amount = 10},
            new ProductBin() {ProductType = new Product() {Name = "Quavers"}, Cost = 0.60m, Amount = 1},
            new ProductBin() {ProductType = new Product() {Name = "Coke Zero"}, Cost = 0.130m, Amount = 5},
            new ProductBin() {ProductType = new Product() {Name = "Water"}, Cost = 0.100m, Amount = 5},
            new ProductBin() {ProductType = new Product() {Name = "Galaxy Caramel"}, Cost = 0.80m, Amount = 3},
            new ProductBin() {ProductType = new Product() {Name = "Dairy Milk"}, Cost = 0.80m, Amount = 4},
            new ProductBin() {ProductType = new Product() {Name = "Chewing Gum"}, Cost = 0.25m, Amount = 0}
        };

        public IEnumerable<ProductBin> GetAllProducts() => this.products;
    }
}
