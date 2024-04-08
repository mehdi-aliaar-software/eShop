using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BM.Application.Contracts.Article;
using BM.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {

        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            var result = _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Title = x.Title,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                ShortDescription = x.ShortDescription

            }).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Article GetWithCategory(long id)
        {
            var result = _context.Articles
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CategoryId = x.CategoryId,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }
            if (searchModel.CategoryId>0)
            {
                query = query.Where(x => x.CategoryId==searchModel.CategoryId);
            }

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;

        }
    }
}
