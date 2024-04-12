namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDtails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
