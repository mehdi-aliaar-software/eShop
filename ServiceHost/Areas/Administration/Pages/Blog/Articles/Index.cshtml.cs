using BM.Application.Contracts.Article;
using BM.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public ArticleSearchModel SearchModel;
        public List<ArticleViewModel> Articles;
        public SelectList ArticleCategories;

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        //[NeedsPermission(ShopPermissions.ListArticleCategories)]
        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.Search(searchModel);
            ArticleCategories = 
                new SelectList( _articleCategoryApplication.Search(new ArticleCategorySearchModel()),
                    "Id","Name");
        }
    }
}