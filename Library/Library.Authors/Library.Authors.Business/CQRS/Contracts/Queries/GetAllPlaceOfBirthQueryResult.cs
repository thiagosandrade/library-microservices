// ---------------------------------------------------------------------------------------
// <copyright file="GetAllPlaceOfBirthQueryResult.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Library.Authors.Business.CQRS.Contracts.Queries
{
    public class GetAllPlaceOfBirthQueryResult
    {
        public List<GetPlaceOfBirthResult> PlaceOfBirths { get; set; }

    }
}