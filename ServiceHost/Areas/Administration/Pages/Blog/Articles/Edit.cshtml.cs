using BM.Application.Contracts.Article;
using BM.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        public SelectList ArticleCategories;
        public EditArticle Command;

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet(long id)
        {
            Command = _articleApplication.GetDetails(id);
            ArticleCategories =
                new SelectList(_articleCategoryApplication
                    .Search(new ArticleCategorySearchModel()), "Id", "Name");
        }

        public IActionResult OnPost(EditArticle command)
        {
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
