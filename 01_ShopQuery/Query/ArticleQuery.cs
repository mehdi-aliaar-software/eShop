using _0_Framework.Application;
using _01_ShopQuery.Contracts.Article;
using _01_ShopQuery.Contracts.Comment;
using _01_ShopQuery.Contracts.Product;
using BM.Infrastructure.EfCore;
using CM.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_ShopQuery.Query
{
    public class ArticleQuery: IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var result = _context
                .Articles
                .Include(x => x.Category)
                .Where(x=>x.PublishDate<DateTime.Now)
                .Select(x => new ArticleQueryModel
            {
                Id=x.Id,
                Title = x.Title,
                CanonicalAddress = x.CanonicalAddress,
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

            if (!string.IsNullOrWhiteSpace(result.Keywords))
            {
                result.KeywordList = result.Keywords.Split(",").ToList();
            }

            var comments = _commentContext
            
                .Comments
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == result.Id)
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId>0)
                {
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }
            }

            result.Comments= comments;
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
