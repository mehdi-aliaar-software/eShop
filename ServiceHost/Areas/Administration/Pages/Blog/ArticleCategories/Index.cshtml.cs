using BM.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> ArticleCategories;

        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        //[NeedsPermission(ShopPermissions.ListArticleCategories)]
        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        //[NeedsPermission(ShopPermissions.CreateArticleCategory)]
        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategoryApplication.Getdatails(id);
            return Partial("Edit", articleCategory);
        }

        //[NeedsPermission(ShopPermissions.EditArticleCategory)]
        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            if (ModelState.IsValid)
            {
            }

            var result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}