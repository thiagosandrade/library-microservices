using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetAllUserQuery : IRequest<GetAllUserQueryResult>
    {
    }
}