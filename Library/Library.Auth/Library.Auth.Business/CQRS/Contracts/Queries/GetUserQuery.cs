using MediatR;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetUserQuery : IRequest<GetUserQueryResult>
    {
        public GetUserQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}