namespace Data.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        IProductRepository Products {get;}
        ICategoryRepository Categories {get;}
    }
}