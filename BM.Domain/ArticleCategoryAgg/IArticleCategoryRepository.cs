using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BM.Application.Contracts.ArticleCategory;

namespace BM.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long, ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> Search (ArticleCategorySearchModel searchModel);
    }
}
