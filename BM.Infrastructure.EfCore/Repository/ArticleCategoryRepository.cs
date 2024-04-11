using BM.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BM.Application.Contracts.ArticleCategory;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.EfCore.Repository
{
    public  class ArticleCategoryRepository: RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }


        public EditArticleCategory GetDetails(long id)
        {
            var result = _blogContext.ArticleCategories.Select(x=>new EditArticleCategory
            {
                Id = x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
                
                
            }).FirstOrDefault(x => x.Id == id);
            return result;
        }

        public string GetSlugBy(long id)
        {
            var result = _blogContext.ArticleCategories.Select(x => new { x.Id, x.Slug })
                .FirstOrDefault(x => x.Id == id).Slug;
            return result;
        }   

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryViewModel
            {
                Id= x.Id,
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder,
                CreatinDate = x.CreationDate.ToFarsi(),
                ArticlesCount = x.Articles.Count
            });

            if ( !string.IsNullOrWhiteSpace(searchModel.Name)  )
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            var result=query.OrderByDescending(x=>x.ShowOrder).ToList();

            return result;  
        }
    }
}
