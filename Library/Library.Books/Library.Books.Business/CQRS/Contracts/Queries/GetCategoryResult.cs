// ---------------------------------------------------------------------------------------
// <copyright file="GetCategoryResult.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using Library.Books.Domain.Models;
using System;
using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetCategoryResult
    {
        public virtual IEnumerable<BookCategory> Books { get; set; }

    }
}