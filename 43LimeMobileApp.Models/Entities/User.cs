/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _43LimeMobileApp.Models.Entities
{
    public interface IUser
    {
        int Id { get; set; }
        bool? IsActive { get; set; }
        bool? IsOnline { get; set; }
        int RoleId { get; set; }
        string UserId { get; set; }
        string Username { get; set; }
    }

    public class User : IEntity<User>, IUser
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(4)]
        public string UserId { get; set; } = string.Empty;  // 4 Character String Token
        [MaxLength(56)]
        public string Username { get; set; } = string.Empty;
        public int RoleId { get; set; } = 3;
        public bool? IsActive { get; set; } = null;
        public bool? IsOnline { get; set; } = null;

        public User()
        {
            this.UserId = string.Empty;
            this.Username = string.Empty;
        }

        public User(string userId, string username, int roleId, bool isActive, bool isOnline)
        {
            this.UserId = userId;
            this.Username = username;
            this.RoleId = roleId;
            this.IsActive = isActive;
            this.IsOnline = isOnline;
        }
    }
}
