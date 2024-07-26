using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
