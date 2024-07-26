using RealEstateAPI.Enums;
using System.ComponentModel.DataAnnotations;
using Twilio.TwiML.Messaging;

namespace RealEstateAPI.Models
{
    public class Property
    {
        [Key]
        public int PId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyDetails PropertyDetails { get; set; }
        public ResidentialSubtype? ResidentialSubtype { get; set; }
        public CommercialSubtype? CommercialSubtype { get; set; }
        public string Location { get; set; }
        public List<Media> Media { get; set; }
        public decimal Price { get; set; }
    }
}
