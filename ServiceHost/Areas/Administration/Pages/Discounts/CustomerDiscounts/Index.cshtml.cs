using DM.Application.Contracts.CustomerDiscount;
using DM.Domain.CustomerDiscountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application;
using SM.Application.Contracts.Product;

//using ShopManagement.Application.Contracts.ProductCategory;
//using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        //[NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products =_productApplication.GetProducts()
            };

            return Partial("./Create", command);
        }

        //[NeedsPermission(ShopPermissions.CreateProductCategory)]
        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();

            return Partial("Edit", customerDiscount);
        }

        //[NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            if (ModelState.IsValid)
            {
            }

            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}