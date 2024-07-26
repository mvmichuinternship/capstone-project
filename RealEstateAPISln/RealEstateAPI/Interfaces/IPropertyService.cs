using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Properties;

namespace RealEstateAPI.Interfaces
{
    public interface IPropertyService
    {
        Task<Property> AddNewProperty(PostPropertyDTO property);
        Task<Property> RemoveProperty(int id);
        Task<Property> GetPropertyById(int id);
        Task<IEnumerable<Property>> GetAllProperties();
    }
}
