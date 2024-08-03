using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models.DTOs.Medias
{
    [ExcludeFromCodeCoverage]
    public class MediaDTO
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public string? Url { get; set; }
        public string Type { get; set; }
        public int PropertyPId { get; set; }
    }
}
