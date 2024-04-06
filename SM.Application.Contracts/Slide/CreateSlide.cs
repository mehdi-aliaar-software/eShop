using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace SM.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(2*1024*1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile Picture { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Heading { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Text { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string BtnText { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public bool IsRemoved { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Link { get;  set; }
    }

    //=== No need for this:
    //public class SlideSearchModel
    //{
    //}

}
