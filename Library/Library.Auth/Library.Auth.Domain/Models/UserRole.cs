using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Auth.Domain.Models
{
    public class UserRole : Entity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
