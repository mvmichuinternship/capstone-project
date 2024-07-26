using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Enums;
using RealEstateAPI.Exceptions;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Properties;
using System.ComponentModel;

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
                property1 = await MapToProperty(property);
                property1 = await _propertyRepo.Add(property1);
                //propDet = await MapToPropertyDetails(property1);
                //propDet= await _propertyDetailsRepo.Add(propDet);
                media = await MapToMedia(property);
                //m= new Media();
                foreach (var i in media)
                {
                    m = await _mediaRepo.Add(i);
                }
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

        private async Task<PropertyDetails> MapToPropertyDetails( Property prop)
        {
            PropertyDetails propDet = new PropertyDetails();
            propDet.NumberOfBedrooms= prop.PropertyDetails.NumberOfBedrooms;
            propDet.NumberOfBathrooms = prop.PropertyDetails.NumberOfBathrooms;
            propDet.AreaInSqFt = prop.PropertyDetails.AreaInSqFt;
            propDet.PropertyDimensionsLength= prop.PropertyDetails.PropertyDimensionsLength ;
            propDet.PropertyDimensionsWidth= prop.PropertyDetails.PropertyDimensionsWidth ;
            propDet.CommercialAreaInSqFt= prop.PropertyDetails.CommercialAreaInSqFt ;
            propDet.WidthofFacingRoad= prop.PropertyDetails.WidthofFacingRoad ;
            propDet.PId = prop.PId;
            return propDet;
        }
        private async Task<Property> MapToProperty(PostPropertyDTO property)
        {
            Property getPropertyDTO = new Property();
            getPropertyDTO.PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), property.PropertyType, true);
            getPropertyDTO.PropertyDetails = await _propertyDetailsRepo.Add(property.PropertyDetails);
            getPropertyDTO.UserEmail = property.UserEmail;
            getPropertyDTO.Price = property.Price;
            getPropertyDTO.CommercialSubtype = property.CommercialSubtype == null ? null : (CommercialSubtype)Enum.Parse(typeof(CommercialSubtype), property.CommercialSubtype);
            getPropertyDTO.Name = property.Name;
            getPropertyDTO.Location = property.Location;
            getPropertyDTO.ResidentialSubtype =   property.ResidentialSubtype==null? null : (ResidentialSubtype)Enum.Parse(typeof(ResidentialSubtype), property.ResidentialSubtype);
            getPropertyDTO.Media = property.Media;
            return getPropertyDTO;
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

        public async Task<IEnumerable<GetPropertyDTO>> GetAllProperties()
        {
            GetPropertyDTO getPropertyDTO = null;
            IList<GetPropertyDTO> properties = new List<GetPropertyDTO>();
            try
            {
                var property = await _propertyRepo.GetAll();
                foreach (var item in property)
                {
                getPropertyDTO = new GetPropertyDTO();
                getPropertyDTO.PropertyType = item.PropertyType.ToString();
                getPropertyDTO.PropertyDetails = item.PropertyDetails;
                getPropertyDTO.UserEmail = item.UserEmail;
                getPropertyDTO.PId = item.PId;
                getPropertyDTO.Price = item.Price;
                getPropertyDTO.CommercialSubtype = item.CommercialSubtype.ToString();
                getPropertyDTO.Name = item.Name;
                getPropertyDTO.Location = item.Location;
                getPropertyDTO.ResidentialSubtype = item.ResidentialSubtype.ToString();
                getPropertyDTO.Media = item.Media;
                    properties.Add(getPropertyDTO);
                }

                return properties;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("Property not found");
            }
        }

        public async Task<GetPropertyDTO> GetPropertyById(int id)
        {
            GetPropertyDTO getPropertyDTO = null;
            try
            {
                var property = await _propertyRepo.Get(id);
                getPropertyDTO = new GetPropertyDTO();
                getPropertyDTO.PropertyType = property.PropertyType.ToString();
                getPropertyDTO.PropertyDetails = property.PropertyDetails;
                getPropertyDTO.UserEmail = property.UserEmail;
                getPropertyDTO.PId = property.PId;
                getPropertyDTO.Price = property.Price;
                getPropertyDTO.CommercialSubtype= property.CommercialSubtype.ToString();
                getPropertyDTO.Name = property.Name;
                getPropertyDTO.Location = property.Location;
                getPropertyDTO.ResidentialSubtype = property.ResidentialSubtype.ToString();
                getPropertyDTO.Media = property.Media;

                return getPropertyDTO;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("Property not found");
            }
            
          
            }

        public async Task<Property> RemoveProperty(int id)
        {
            try
            {
                var prop = await _propertyRepo.Get(id);
                var pd = await  _propertyDetailsRepo.Delete(prop.PropertyDetails.Id);
                var property = await _propertyRepo.Delete(id);
                return property;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("No such property exists");
            }
        }

        public async Task<Property> UpdateProperty(PostPropertyDTO property)
        {
            try
            {
                IList<Media> medias = new List<Media>();
                foreach (var item in medias)
                {
                    var md = await _mediaRepo.Update(item);
                    medias.Add(md);
                }
                PropertyDetails pd = new PropertyDetails();
                Property getPropertyDTO = await _propertyRepo.Get(property.PId);
                getPropertyDTO.PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), property.PropertyType, true);
                getPropertyDTO.PropertyDetails = await _propertyDetailsRepo.Update(property.PropertyDetails);
                getPropertyDTO.UserEmail = property.UserEmail;
                getPropertyDTO.Price = property.Price;
                getPropertyDTO.CommercialSubtype = property.CommercialSubtype == null ? null : (CommercialSubtype)Enum.Parse(typeof(CommercialSubtype), property.CommercialSubtype);
                getPropertyDTO.Name = property.Name;
                getPropertyDTO.PId = property.PId;
                getPropertyDTO.Location = property.Location;
                getPropertyDTO.ResidentialSubtype = property.ResidentialSubtype == null ? null : (ResidentialSubtype)Enum.Parse(typeof(ResidentialSubtype), property.ResidentialSubtype);
                getPropertyDTO.Media = property.Media;
                getPropertyDTO = await _propertyRepo.Update(getPropertyDTO);
                return getPropertyDTO;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("Property not found");
            }

        }
    }
}



