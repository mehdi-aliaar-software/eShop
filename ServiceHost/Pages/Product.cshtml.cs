using _01_ShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SM.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;

        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.Getdetails(id);
            //return View();
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            var result= _commentApplication.Add(command);
            //return RedirectToPage("/Product", new {Id= command.ProductId});
            return RedirectToPage("/Product", new { id = productSlug } );

        }
    }
}
