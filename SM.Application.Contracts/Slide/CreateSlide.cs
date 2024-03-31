using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Contracts.Slide
{
    public class CreateSlide
    {
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }

        public string Heading { get;  set; }
        public string Title { get;  set; }
        public string Text { get;  set; }

        public string BtnText { get;  set; }

        public bool IsRemoved { get;  set; }
    }

    //=== No need for this:
    //public class SlideSearchModel
    //{
    //}

}
