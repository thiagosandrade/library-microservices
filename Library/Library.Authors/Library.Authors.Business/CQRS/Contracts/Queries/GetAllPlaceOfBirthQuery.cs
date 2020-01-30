// ---------------------------------------------------------------------------------------
// <copyright file="GetAllPlaceOfBirthQuery.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using MediatR;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAllPlaceOfBirthQuery : IRequest<GetAllPlaceOfBirthQueryResult>
    {
    }
}