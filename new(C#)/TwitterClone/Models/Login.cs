using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string CurrentPass { get; set; }

    }
}