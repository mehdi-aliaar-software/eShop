using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BM.Domain.ArticleCategoryAgg;

namespace BM.Domain.ArticleAgg
{
    public class Article:EntityBase
    {
        public string Title { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
       
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }

        public DateTime PublishDate { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory  Category { get; private set;}


        public Article(string title, string picture, string pictureTitle, string pictureAlt, 
            string shortDescription, string description, string keywords, 
            string metaDescription, string slug, string canonicalAddress, DateTime publishDate,
            long categoryId)
        {
            Title = title;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }

        public void Edit(string title, string picture, string pictureTitle, string pictureAlt,
            string shortDescription, string description,  string keywords,
            string metaDescription, string slug, string canonicalAddress, DateTime publishDate,
            long categoryId)
        {
            Title = title;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            ShortDescription = shortDescription;
            Description = description;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }
    }

    
}
