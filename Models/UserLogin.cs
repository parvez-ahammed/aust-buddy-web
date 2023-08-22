using System.ComponentModel.DataAnnotations;

namespace AUST_BUDDY_WEB.Models
{
    public class UserLogin
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}