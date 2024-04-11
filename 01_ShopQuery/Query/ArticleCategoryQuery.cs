using _0_Framework.Application;
using _01_ShopQuery.Contracts.Article;
using _01_ShopQuery.Contracts.ArticleCategory;
using BM.Domain.ArticleAgg;
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

        public ArticleCategoryQueryModel GetArticleCategoryBy(string slug)
        {
            var result = _context
                .ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    Articles = MapArticles(x.Articles), 
                    
                }).FirstOrDefault(x => x.Slug == slug);
            if (result!=null)
            {
                result.KeywordList = result.Keywords.Split(",").ToList();
            }
           

            return result;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            var result = articles.Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ShortDescription =  x.ShortDescription,
                PublishDate = x.PublishDate.ToFarsi()
                
            }).ToList();
            return result;  
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
