using MediatR;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAllAuthorQuery : IRequest<GetAllAuthorQueryResult>
    {
    }
}