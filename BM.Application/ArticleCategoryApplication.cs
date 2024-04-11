using BM.Application.Contracts.ArticleCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BM.Domain.ArticleCategoryAgg;

namespace BM.Application
{
    public class ArticleCategoryApplication: IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation=new OperationResult();

            if  (_articleCategoryRepository.Exists(x=>x.Name== command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            var slug=command.Slug.Slugify();
            command.CanonicalAddress = command.CanonicalAddress == null ? "" : command.CanonicalAddress;
            
            var picturePath = _fileUploader.Upload(command.Picture,slug);
            var articleCategory = new ArticleCategory(command.Name, command.Description, picturePath, 
                command.PictureAlt, command.PictureTitle,command.ShowOrder, command.MetaDescription,
                slug, command.Keywords, command.CanonicalAddress);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory=_articleCategoryRepository.GetBy(command.Id);
            if (articleCategory==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id!=command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            
            command.CanonicalAddress = command.CanonicalAddress == null ? "" : command.CanonicalAddress;

            var slug = command.Slug.Slugify();
            var picturePath = _fileUploader.Upload(command.Picture, slug);

            articleCategory.Edit(command.Name, picturePath, command.PictureTitle, command.PictureAlt,
                command.Description, command.ShowOrder, command.Keywords, command.MetaDescription, slug
                , command.CanonicalAddress);


            //_articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticleCategory Getdatails(long id)
        {
            var result=_articleCategoryRepository.GetDetails(id);
            return result;
        }

        //public string GetSlugBy(long id)
        //{
        //    var result=_articleCategoryRepository.get
        //}

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var result= _articleCategoryRepository.Search(searchModel);
            return result;
        }
    }
}
