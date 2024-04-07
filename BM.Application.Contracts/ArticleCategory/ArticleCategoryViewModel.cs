﻿namespace BM.Application.Contracts.ArticleCategory;

public class ArticleCategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }
    public int ShowOrder { get; set; }

    public string CreatinDate { get; set; }
    public int ArticlesCount { get; set; }

}