using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiMovies.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public string image { get; set; }

        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Client,
        Administrator
    }
}
