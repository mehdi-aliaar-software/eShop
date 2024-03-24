namespace SM.Application.Contracts.ProductCategory;

public class ProductCategoryViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string CreationDate { get; set; }
        
    public int ProductsCount { get; set; }

}