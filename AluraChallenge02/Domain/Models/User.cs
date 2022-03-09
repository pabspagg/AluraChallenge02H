using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge02.Models
{
    [Table("Usuario")]
    public class User
    {
        #region construtor
        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
        #endregion

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Entre com o usuário.")]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Entre com a senha.")]
        [MaxLength(40)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Entre com o role.")]
        [MaxLength(40)]
        public string Role { get; set; }
    }
}