using System.ComponentModel.DataAnnotations;

namespace StoreNew.Models
{
    public class ForgetPasswordAddByMe
    {
        [Required][EmailAddress]
        public string Email { get; set; }
		public int PhoneNumber { get; set; }
	}
}
