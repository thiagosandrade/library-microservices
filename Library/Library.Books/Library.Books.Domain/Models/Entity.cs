// ---------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="NoTie S.à r.l.">
//     This file is property of NoTie S.à r.l. All right reserved.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Books.Domain.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}