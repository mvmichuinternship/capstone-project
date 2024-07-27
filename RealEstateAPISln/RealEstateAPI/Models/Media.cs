using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        [ForeignKey("Property")]
        public int PropertyPId { get; set; }
    }
}
