// ---------------------------------------------------------------------------------------
// <copyright file="GetAllBookQueryResult.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllBookQueryResult
    {
        public List<GetBookQueryResult> Books { get; set; }
    }
}