using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.Comment;
using SM.Domain.CommentAgg;

namespace SM.Infrastructure.EfCore.Repository
{
    public class CommentRepository:RepositoryBase<long,Comment>, ICommentRepository
    {
        private readonly ShopContext _shopContext;
        public CommentRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _shopContext
                .Comments
                .Include(x=>x.Product)
                .Select(x => new CommentViewModel
            {
                Id=x.Id,
                Name = x.Name,
                ProductId = x.ProductId,
                Message = x.Message,
                Email = x.Email,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                ProductName = x.Product.Name,
                CommentDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }

            //var result1 = query.ToList();
            //if (result1 == null)
            //{
            //    return new List<CommentViewModel>();
            //}

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;
        }
    }
}
