using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Properties;

namespace RealEstateAPI.Interfaces
{
    public interface IPropertyService
    {
        Task<Property> AddNewProperty(PostPropertyDTO property);
        Task<Property> UpdateProperty(PostPropertyDTO property);
        Task<Property> RemoveProperty(int id);
        Task<GetPropertyDTO> GetPropertyById(int id);
        Task<IList<GetPropertyDTO>> GetAllProperties();
    }
}
