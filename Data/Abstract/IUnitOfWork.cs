namespace Data.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Products {get;}
        ICategoryRepository Categories {get;}
        ICartRepository Carts {get;}
        IOrderRepository Orders {get;}
    }
}