namespace VendingTest.Core.Models
{
    public class ProductBin
    {
        public Product ProductType { get; set; } = new Product();
        public decimal Cost { get; set; } = 0;
        public int Amount { get; set; } = 0;
    }
}
