// ---------------------------------------------------------------------------------------
// <copyright file="GetCategoryResult.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetCategoryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}