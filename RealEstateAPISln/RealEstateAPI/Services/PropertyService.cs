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
                propDet = await MapToPropertyDetails(property1);
                propDet = await _propertyDetailsRepo.Update(propDet);
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
            PropertyDetails propDet;
            propDet = await _propertyDetailsRepo.Get(prop.PropertyDetails.Id);
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
            getPropertyDTO.Phone = property.Phone;
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

        public async Task<IList<GetPropertyDTO>> GetAllProperties()
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
                getPropertyDTO.Phone = item.Phone;
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
                getPropertyDTO.Phone = property.Phone;
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
                var property = await _propertyRepo.Delete(id);
                var pd = await  _propertyDetailsRepo.Delete(prop.PropertyDetails.Id);
                return property;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("No such property exists");
            }
        }

        //public async Task<Property> UpdateProperty(PostPropertyDTO property)
        //{
        //        PropertyDetails pd =null;
        //        Property getPropertyDTO =null;
        //        Media media = new Media();
        //    try
        //    {
        //        getPropertyDTO = await _propertyRepo.Get(property.PId);
        //        var medias = property.Media;
        //        foreach (var item in medias)
        //        {
        //            media.Id = item.Id;
        //            media.Url= item.Url;
        //            media.Type = item.Type;
        //            media = await _mediaRepo.Update(media);
        //            medias.Add(media);
        //        }
        //        pd = await MapToPD(property.PropertyDetails);
        //        pd = await _propertyDetailsRepo.Update(pd);
        //        getPropertyDTO.PropertyDetails = pd;
        //        getPropertyDTO.UserEmail = property.UserEmail;
        //        getPropertyDTO.PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), property.PropertyType, true);
        //        getPropertyDTO.Price = property.Price;
        //        getPropertyDTO.CommercialSubtype = property.CommercialSubtype == null ? null : (CommercialSubtype)Enum.Parse(typeof(CommercialSubtype), property.CommercialSubtype);
        //        getPropertyDTO.Name = property.Name;
        //        getPropertyDTO.PId = property.PId;
        //        getPropertyDTO.Location = property.Location;
        //        getPropertyDTO.ResidentialSubtype = property.ResidentialSubtype == null ? null : (ResidentialSubtype)Enum.Parse(typeof(ResidentialSubtype), property.ResidentialSubtype);
        //        getPropertyDTO.Media = medias;
        //        getPropertyDTO = await _propertyRepo.Update(getPropertyDTO);
        //        return getPropertyDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new NoPropertyException("Property not found");
        //    }

        //}


        public async Task<Property> UpdateProperty(PostPropertyDTO propertyDto)
        {
            
            Property getPropertyDTO = null;
            List<Media> updatedMedias = new List<Media>();

            try
            {
                getPropertyDTO = await _propertyRepo.Get(propertyDto.PId);
                Console.WriteLine(getPropertyDTO.Media[0].Url);
                Console.WriteLine(getPropertyDTO.PropertyDetails.Id);
                if (getPropertyDTO == null)
                {
                    throw new NoPropertyException("Property not found");
                }

                for (var i=0; i<propertyDto.Media.Count;i++)
                {
                    Media media = getPropertyDTO.Media[i];
                    if (media == null)
                    {
                        media = new Media();
                    }
                    media.Url = propertyDto.Media[i].Url;
                    media.Type = propertyDto.Media[i].Type;
                    media = await _mediaRepo.Update(media);
                    updatedMedias.Add(media);
                }

                
                var pd = await MapToPD(propertyDto.PropertyDetails, getPropertyDTO);
                pd = await _propertyDetailsRepo.Update(pd);

                getPropertyDTO.PropertyDetails = pd;
                getPropertyDTO.UserEmail = propertyDto.UserEmail;
                getPropertyDTO.PropertyType = (PropertyType)Enum.Parse(typeof(PropertyType), propertyDto.PropertyType, true);
                getPropertyDTO.Price = propertyDto.Price;
                getPropertyDTO.CommercialSubtype = propertyDto.CommercialSubtype == null ? null : (CommercialSubtype)Enum.Parse(typeof(CommercialSubtype), propertyDto.CommercialSubtype);
                getPropertyDTO.Name = propertyDto.Name;
                getPropertyDTO.Phone = propertyDto.Phone;
                getPropertyDTO.PId = propertyDto.PId;
                getPropertyDTO.Location = propertyDto.Location;
                getPropertyDTO.ResidentialSubtype = propertyDto.ResidentialSubtype == null ? null : (ResidentialSubtype)Enum.Parse(typeof(ResidentialSubtype), propertyDto.ResidentialSubtype);
                getPropertyDTO.Media = updatedMedias;

                getPropertyDTO = await _propertyRepo.Update(getPropertyDTO);
                return getPropertyDTO;
            }
            catch (Exception ex)
            {
                throw new NoPropertyException("Property not found");
            }
        }

        private async Task<PropertyDetails> MapToPD(PropertyDetails prop,Property property)
        {
            PropertyDetails propDet = await _propertyDetailsRepo.Get(property.PropertyDetails.Id);
            if (propDet == null)
            {
                throw new NoPropertyException("Property Details not found");
            }
            propDet.NumberOfBedrooms = prop.NumberOfBedrooms;
            propDet.NumberOfBathrooms = prop.NumberOfBathrooms;
            propDet.AreaInSqFt = prop.AreaInSqFt;
            propDet.PropertyDimensionsLength = prop.PropertyDimensionsLength;
            propDet.PropertyDimensionsWidth = prop.PropertyDimensionsWidth;
            propDet.CommercialAreaInSqFt = prop.CommercialAreaInSqFt;
            propDet.WidthofFacingRoad = prop.WidthofFacingRoad;
            propDet.PId = prop.PId;
            return propDet;
        }

    }
}



