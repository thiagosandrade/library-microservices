// ---------------------------------------------------------------------------------------
// <copyright file="GetAllBookQuery.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllBookQuery : IRequest<GetAllBookQueryResult>
    {
    }
}