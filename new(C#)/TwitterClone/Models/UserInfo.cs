using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    public class UserInfo
    {
        [Key]
        public int UserInfoId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string UserDesc { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string UserImage { get; set; }
        public int Following { get; set; }
        public int Followers { get; set; }
        public string Joined { get; set; }
        public string Location { get; set; }
        public string UserBackground { get; set; }

    }
}