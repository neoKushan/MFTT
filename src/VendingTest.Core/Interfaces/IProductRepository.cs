namespace VendingTest.Core.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IProductRepository
    {
        IEnumerable<ProductBin> GetAllProducts();
    }
}
