using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPWZ
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int Id_role { get; set; }

        public User() { }
        public User(string? login, string? password, int id_role)
        {
            Login = login;
            Password = password;
            Id_role = id_role;
        }
    }
}
