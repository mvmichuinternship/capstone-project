using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Models
{
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
