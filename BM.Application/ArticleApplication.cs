using BM.Application.Contracts.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BM.Domain.ArticleAgg;
using BM.Domain.ArticleCategoryAgg;

namespace BM.Application
{
    public class ArticleApplication: IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation=new OperationResult();
            if (_articleRepository.Exists(x=>x.Title==command.Title))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug=command.Slug.Slugify();

            var filePath = $"{_articleCategoryRepository.GetSlugBy(command.CategoryId)}/{slug}";
            var picFile = _fileUploader.Upload(command.Picture, filePath);
            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
            var article = new Article(command.Title, picFile, command.PictureTitle, command.PictureAlt,
                command.ShortDescription, command.Description, command.Keywords, command.MetaDescription, slug, 
                command.CanonicalAddress, publishDate, command.CategoryId);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation= new OperationResult();
            var article = _articleRepository.GetWithCategory(command.Id);
            if (article == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slug=command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";
            var picFile= _fileUploader.Upload(command.Picture, path);

            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, picFile, command.PictureTitle, command.PictureAlt, command.ShortDescription,
                command.Description, command.Keywords, command.MetaDescription, slug, command.CanonicalAddress,
                publishDate, command.CategoryId);
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            var result = _articleRepository.GetDetails(id);
            return result;  
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var result = _articleRepository.Search(searchModel);
            return result;
        }
    }
}
