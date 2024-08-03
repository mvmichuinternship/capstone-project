using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class PropertyDetails
    {
        [Key]
        public int Id { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        public int? AreaInSqFt { get; set; }
        public int? PropertyDimensionsWidth { get; set; }
        public int? PropertyDimensionsLength { get; set; }
        public bool? HasConstructions { get; set; }
        public int? WidthofFacingRoad { get; set; }
        public int? CommercialAreaInSqFt { get; set; }

        [ForeignKey("Property")]
        public int? PId { get; set; }

    }
}
