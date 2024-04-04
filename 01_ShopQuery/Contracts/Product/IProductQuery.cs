namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel Getdetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
