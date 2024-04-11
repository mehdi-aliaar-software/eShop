using _0_Framework.Application;
using _01_ShopQuery.Contracts.Article;
using BM.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_ShopQuery.Query
{
    public class ArticleQuery: IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var result = _context
                .Articles
                .Include(x => x.Category)
                .Where(x=>x.PublishDate<DateTime.Now)
                .Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                CanonicalAddress = x.CanonicalAddress,
                //CategoryId = x.CategoryId,
                CategorySlug = x.Category.Slug,
                CategoryName = x.Category.Name,
                Description = x.Description,
                Keywords = x.Keywords,
                
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription

            }).FirstOrDefault(x => x.Slug == slug);

            result.KeywordList = result.Keywords.Split(",").ToList();
            return result;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            var articles = _context.Articles
                .Include(x => x.Category)
                .Where(x=>x.PublishDate<DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,

                    //CanonicalAddress = x.CanonicalAddress,
                    //CategoryId = x.CategoryId,
                    //CategorySlug = x.Category.Slug,
                    //CategoryName = x.Category.Name,
                    //Description = x.Description,
                    //Keywords = x.Keywords,
                    //MetaDescription = x.MetaDescription,

                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription

                }).ToList();

            return articles;

        }
    }   
}
