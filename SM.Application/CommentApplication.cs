using SM.Application.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using SM.Domain.CommentAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SM.Application
{
    public class CommentApplication: ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation=new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.GetBy(id);
            if (comment == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Confirm();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.GetBy(id);
            if (comment==null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Cancel();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var result = _commentRepository.Search(searchModel);
            return result;
        }
    }
}
