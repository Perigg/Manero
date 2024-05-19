using System.ComponentModel.DataAnnotations;

namespace Manero.Client.Models
{
	public class AddressForm
	{
		public int HolderId { get; set; }
		[Display(Name = "Addressee Name", Prompt = "Enter full name")]
		[Required(ErrorMessage = "Name is required")]
		[RegularExpression(@"^[_A-zA-Z]*((-|\s)*[_A-zA-Z])*$", ErrorMessage = "Only spaces and letters in name allowed")]
		public string HolderName { get; set; }
		[Display(Name = "Address", Prompt = "Enter address")]
		[Required(ErrorMessage = "Address is required")]
		public string AddressOne { get; set; }
		[Display(Name = "Second Address", Prompt = "Optional secondary address or C/O")]
		public string? AddressTwo { get; set; }
		//[DataType(DataType.PostalCode)]
		[Display(Name = "Postal Code", Prompt = "Enter postal code")]
		[Required(ErrorMessage = "Postal code is required")]
		public string PostalCode { get; set; }
		[Display(Name = "City", Prompt = "Enter city")]
		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }
		[Display(Name = "Country", Prompt = "Enter country")]
		[Required(ErrorMessage = "Country is required")]
		public string Country { get; set; }

	}
}
