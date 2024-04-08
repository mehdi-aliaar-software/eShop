using BM.Application.Contracts.ArticleCategory;

namespace BM.Application.Contracts.Article;

public class ArticleSearchModel
{
    public string Title { get; set; }
    public long CategoryId { get; set; }
    private List<ArticleCategoryViewModel>  Categories { get; set; }
}