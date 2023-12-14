using Data.Abstract;

namespace Data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        protected private ShopContext? _context;
        public UnitOfWork(ShopContext? context)
        {
            _context = context;
        }

        protected private EfCoreProductRepository? productRepository;
        protected private EfCoreCategoryRepository? categoryRepository;
        public IProductRepository Products => productRepository ??= new EfCoreProductRepository(_context!);

        public ICategoryRepository Categories => categoryRepository ??= new EfCoreCategoryRepository(_context!);

        public void Dispose() => _context!.Dispose();

    }

}