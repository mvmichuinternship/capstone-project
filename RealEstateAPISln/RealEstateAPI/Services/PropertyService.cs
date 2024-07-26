using RealEstateAPI.Exceptions;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Properties;

namespace RealEstateAPI.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IRepository<int, Property> _propertyRepo;
        private readonly IRepository<int, PropertyDetails> _propertyDetailsRepo;
        private readonly IRepository<int, Media> _mediaRepo;

        public PropertyService(IRepository<int, Property> propertyRepo, IRepository<int, PropertyDetails> propertyDetailsRepo, IRepository<int, Media> mediaRepo)
        {
            _propertyRepo = propertyRepo;
            _propertyDetailsRepo = propertyDetailsRepo;
            _mediaRepo = mediaRepo;
        }
        public async Task<Property> AddNewProperty(PostPropertyDTO property)
        {
            PropertyDetails propDet = null;
            IList<Media> media = null;
            Property property1 = null;
            Media m = null;
            try
            {
                propDet = await MapToPropertyDetails(property);
                propDet= await _propertyDetailsRepo.Add(propDet);
                media = await MapToMedia(property);
                m= new Media();
                foreach (var i in media)
                {
                    m = await _mediaRepo.Add(i);
                }
                property1 = await _propertyRepo.Add(property);
                return property1;
            }
            catch (Exception ex) 
            {
                if (propDet != null)
                {
                    await RevertPropertyDetailsInsert(propDet);
                }
                if (media != null )
                {
                    await RevertMediaInsert(m);
                } 
                if (property1 != null && propDet==null && media==null)
                {
                    await RevertPropertyInsert(property1);
                }
                throw new UnableToAddException("Not able to register at this moment");
            }
        }

        private async Task RevertPropertyDetailsInsert(PropertyDetails propDet)
        {
            await _propertyDetailsRepo.Delete(propDet.Id);
        }

        private async Task RevertMediaInsert(Media media)
        {
            await _mediaRepo.Delete(media.Id);
        }

        private async Task RevertPropertyInsert(Property prop)
        {
            await _propertyRepo.Delete(prop.PId);
        }

        private async Task<PropertyDetails> MapToPropertyDetails(PostPropertyDTO postPropertyDTO)
        {
            PropertyDetails propDet = new PropertyDetails();
            propDet.NumberOfBedrooms= postPropertyDTO.PropertyDetails.NumberOfBedrooms;
            propDet.NumberOfBathrooms = postPropertyDTO.PropertyDetails.NumberOfBathrooms;
            propDet.AreaInSqFt = postPropertyDTO.PropertyDetails.AreaInSqFt;
            propDet.PropertyDimensionsLength= postPropertyDTO.PropertyDetails.PropertyDimensionsLength ;
            propDet.PropertyDimensionsWidth= postPropertyDTO.PropertyDetails.PropertyDimensionsWidth ;
            propDet.CommercialAreaInSqFt= postPropertyDTO.PropertyDetails.CommercialAreaInSqFt ;
            propDet.WidthofFacingRoad= postPropertyDTO.PropertyDetails.WidthofFacingRoad ;
            return propDet;
        }

        private async Task<IList<Media>> MapToMedia(PostPropertyDTO postPropertyDTO)
        {
            IList<Media> media = new List<Media>();
            foreach(var i in postPropertyDTO.Media)
            {
                media.Add(i);
            }
            return media;
        }

        public Task<IEnumerable<Property>> GetAllProperties()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetPropertyById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Property> RemoveProperty(int id)
        {
            throw new NotImplementedException();
        }
    }
}
