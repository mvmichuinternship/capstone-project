using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;

namespace RealEstateAPI.Repositories
{
    public class PropertyRepository : IRepository<int, Property>
    {
        private RealEstateAppContext _realEstateAppContext;
        public PropertyRepository(RealEstateAppContext realEstateAppContext)
        {
            _realEstateAppContext = realEstateAppContext;
        }

        /// <summary>
        /// Addition of property to db
        /// </summary>
        /// <param name="entity"> Of type Property </param>
        /// <returns> An object of type Property </returns>
        public async Task<Property> Add(Property entity)
        {
            var isregistered = await Get(entity.PId);
            if (isregistered == null)
            {
                _realEstateAppContext.Add(entity);
                var res = await _realEstateAppContext.SaveChangesAsync();
                return entity;
            }
            else return null;
        }
        /// <summary>
        /// Deletes property from db
        /// </summary>
        /// <param name="key">email of property</param>
        /// <returns>deleted data</returns>
        public async Task<Property> Delete(int key)
        {
            var property = await Get(key);
            if (property != null)
            {
                _realEstateAppContext.Remove(property);
                await _realEstateAppContext.SaveChangesAsync();
                return property;
            }
            return null;
        }

        /// <summary>
        /// Read data of one property from db
        /// </summary>
        /// <param name="key">email of property</param>
        /// <returns>data of property</returns>
        public async Task<Property> Get(int key)
        {
            return (await _realEstateAppContext.Properties.SingleOrDefaultAsync(u => u.PId == key)) ?? null;
        }


        /// <summary>
        /// Returns list of all propertys from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Property>> GetAll()
        {
            return await _realEstateAppContext.Properties.ToListAsync();
        }

        /// <summary>
        /// Updates the property data
        /// </summary>
        /// <param name="entity">Of type Property</param>
        /// <returns> An object of type Property </returns>
        public async Task<Property> Update(Property entity)
        {
            var property = await Get(entity.PId);
            if (property != null)
            {
                _realEstateAppContext.Update(entity);
                await _realEstateAppContext.SaveChangesAsync();
                return property;
            }
            return null;
        }
    }
}
