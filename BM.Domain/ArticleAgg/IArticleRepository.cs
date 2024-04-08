using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BM.Application.Contracts.Article;

namespace BM.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        Article GetWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
