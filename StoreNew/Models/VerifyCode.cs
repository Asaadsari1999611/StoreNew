using System.ComponentModel.DataAnnotations;

namespace StoreNew.Models
{
	public class VerifyCode
	{
		[Required]
		public string Code { get; set; }
	}
}
