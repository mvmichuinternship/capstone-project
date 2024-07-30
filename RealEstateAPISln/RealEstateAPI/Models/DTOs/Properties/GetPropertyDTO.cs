using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Enums;

namespace RealEstateAPI.Models.DTOs.Properties
{
    public class GetPropertyDTO
    {
        public int PId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PropertyType { get; set; }
        public PropertyDetails PropertyDetails { get; set; }
        public string? ResidentialSubtype { get; set; }
        public string? CommercialSubtype { get; set; }
        public string Location { get; set; }
        public List<Media> Media { get; set; }
        public decimal Price { get; set; }
    }
}
