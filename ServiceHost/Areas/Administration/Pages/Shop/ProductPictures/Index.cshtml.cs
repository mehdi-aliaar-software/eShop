using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;
using SM.Application.Contracts.ProductCategory;
using SM.Application.Contracts.ProductPicture;

//using ShopManagement.Application.Contracts.ProductCategory;
//using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictures;
        public SelectList Products;

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        //[NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList (_productApplication.GetProducts(),  "Id","Name") ;
            ProductPictures = _productPictureApplication.Search(searchModel);
            
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        //[NeedsPermission(ShopPermissions.CreateProductCategory)]
        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _productPictureApplication.GetDetails(id);
            productPicture.Products= _productApplication.GetProducts();
            return Partial("Edit", productPicture);
        }

        //[NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            if (ModelState.IsValid)
            {
            }

            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");
        }

    }
}