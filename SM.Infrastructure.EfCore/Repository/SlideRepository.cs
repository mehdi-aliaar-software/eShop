using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Slide;
using SM.Domain.SlideAgg;

namespace SM.Infrastructure.EfCore.Repository
{
    public class SlideRepository:RepositoryBase<long,Slide>, ISlideRepository
    {
        private readonly ShopContext _Context;
        public SlideRepository(ShopContext context) : base(context)
        {
            _Context = context;
        }

        public EditSlide GetDetails(long id)
        {
            var result = _Context.Slides
                .Select(x => new EditSlide
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Heading = x.Heading,
                    Title = x.Title,
                    BtnText = x.BtnText,
                    Text = x.Text,
                    Link = x.Link,
                    IsRemoved = x.IsRemoved
                })
                .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public List<SlideViewModel> GetList()
        {
            var result = _Context.Slides
                .Select(x => new SlideViewModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    Heading = x.Heading,
                    Title = x.Title,
                    IsRemoved = x.IsRemoved,
                    CreationDate = x.CreationDate.ToFarsi()
                })
                .OrderByDescending(x=>x.Id).ToList();
            return result;
        }
    }
}
