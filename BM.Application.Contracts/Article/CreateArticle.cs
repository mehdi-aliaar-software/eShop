using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BM.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile Picture { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }
        public string CanonicalAddress { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PublishDate { get;  set; }

        [Range(1,100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get;  set; }

    }
}
