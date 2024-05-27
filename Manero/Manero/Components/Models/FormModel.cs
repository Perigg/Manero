namespace Manero.Components.Models
{
    public class FormModel
    {
        public int Id { get; set; }
        public int HolderId { get; set; }
        public string HolderName { get; set; }

        public string AddressOne { get; set; }

        public string? AddressTwo { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
