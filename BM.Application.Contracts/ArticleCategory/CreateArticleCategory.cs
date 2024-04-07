﻿using Microsoft.AspNetCore.Http;

namespace BM.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        public string Name { get;  set; }
        public IFormFile Picture { get;  set; }
        public string Description { get;  set; }
        public int ShowOrder { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
        public string Slug { get;  set; }
        public string CanonicalAddress { get;  set; }
        
    }
}
