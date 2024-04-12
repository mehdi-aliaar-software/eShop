using _0_Framework.Domain;
using CM.Application.Contracts.Comment;

namespace CM.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
