using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class Contact
    {
        [Key]
        public int Cid { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public string UserEmail { get; set; }
        [ForeignKey("Property")]
        public int PId { get; set; }
    }
}
