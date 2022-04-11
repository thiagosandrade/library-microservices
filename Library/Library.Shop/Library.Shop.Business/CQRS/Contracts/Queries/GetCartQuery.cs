using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Queries
{
    public class GetCartQuery : IRequest<GetCartQueryResult>
    {
        public GetCartQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}