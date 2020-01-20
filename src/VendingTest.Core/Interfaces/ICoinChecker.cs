namespace VendingTest.Core.Interfaces
{
    using Models;

    public interface ICoinChecker
    {
        ValidCoin CheckCoin(InsertedCoin coin);
    }
}
