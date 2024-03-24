using SM.Application.Contracts.ProductCategory;
using System.Linq.Expressions;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        ProductCategory GetBy(long id);
        List<ProductCategory> GetAll();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        void Save();
    }
}
