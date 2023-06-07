using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    public class Comments
    {
        [Key]
        public int id { get; set; }
        public int ComId { get; set; }
    }
}