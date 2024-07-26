using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Models;
namespace RealEstateAPI.Context

{
    public class RealEstateAppContext : DbContext
    {
        public RealEstateAppContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<TokenData> TokenData { get; set; }
        public DbSet<OTP> OtpRecords { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<PropertyDetails> PropertyDetails { get; set; }
        public DbSet<Property> Properties { get; set; }

    }
}
