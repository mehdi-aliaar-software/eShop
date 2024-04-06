using SM.Application.Contracts.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using SM.Domain.SlideAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SM.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operaion = new OperationResult();

            var picturePath = _fileUploader.Upload(command.Picture, "slides");

            var slide = new Slide(picturePath, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.Link, command.BtnText);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operaion.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operaion = new OperationResult();

            var slide = _slideRepository.GetBy(command.Id);
            if (slide == null)
            {
                return operaion.Failed(ApplicationMessages.RecordNotFound);
            }

            var picturePath = _fileUploader.Upload(command.Picture, "slides");


            slide.Edit(picturePath, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Title, command.Text, command.Link, command.BtnText);

            _slideRepository.SaveChanges();
            return operaion.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operaion = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operaion.Failed(ApplicationMessages.RecordNotFound);
            }

            slide.Remove();
            _slideRepository.SaveChanges();
            return operaion.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operaion = new OperationResult();
            var slide = _slideRepository.GetBy(id);
            if (slide == null)
            {
                return operaion.Failed(ApplicationMessages.RecordNotFound);
            }

            slide.Restore();
            _slideRepository.SaveChanges();
            return operaion.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            var slide = _slideRepository.GetDetails(id);
            return slide;

        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }
    }
}
