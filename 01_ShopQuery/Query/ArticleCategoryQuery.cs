using _01_ShopQuery.Contracts.ArticleCategory;
using BM.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_ShopQuery.Query
{
    public class ArticleCategoryQuery: IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            var result=_context
                .ArticleCategories
                .Include(x=>x.Articles)
                .Select(x=>new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ArticlesCount = x.Articles.Count

            }).ToList();
            return result;
        }
    }
}
