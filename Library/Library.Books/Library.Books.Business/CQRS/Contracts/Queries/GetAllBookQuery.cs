
using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllBookQuery : IRequest<GetAllBookQueryResult>
    {
    }
}