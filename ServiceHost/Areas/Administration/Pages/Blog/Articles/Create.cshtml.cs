using BM.Application.Contracts.Article;
using BM.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        public SelectList ArticleCategories;
        public CreateArticle Command;

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet()
        {
            ArticleCategories = 
                new SelectList( _articleCategoryApplication
                    .Search(new ArticleCategorySearchModel()),"Id","Name");
        }

        public IActionResult OnPost(CreateArticle command)
        {
            var result= _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
