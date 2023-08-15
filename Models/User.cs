using System;
using System.ComponentModel.DataAnnotations;

namespace AUST_BUDDY_WEB.Models
{
	public class User
	{
		public string FullName { get; set; }
		public string LastName { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Retype your password")]
		[DataType(DataType.Password)]
		public String ConfirmPassword { get; set; }
	}
}