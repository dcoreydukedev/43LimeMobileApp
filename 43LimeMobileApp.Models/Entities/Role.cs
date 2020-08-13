/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _43LimeMobileApp.Models.Entities
{
    public interface IRole
    {
        int Id { get; set; }
        string RoleName { get; set; }
    }

    public class Role : IEntity<Role>, IRole
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [MaxLength(28)]
        public string RoleName { get; set; }

        public Role()
        {
            this.RoleName = string.Empty;
        }

        public Role(string roleName)
        {
            this.RoleName = roleName;
        }

        public Role(int id, string roleName)
        {
            this.Id = id;
            this.RoleName = roleName;
        }
    }
}
