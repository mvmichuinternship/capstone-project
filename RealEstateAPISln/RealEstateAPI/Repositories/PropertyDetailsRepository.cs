using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;

namespace RealEstateAPI.Repositories
{
    public class PropertyDetailsRepository : IRepository<int, PropertyDetails>
    {
        private RealEstateAppContext _realEstateAppContext;
        public PropertyDetailsRepository(RealEstateAppContext realEstateAppContext)
        {
            _realEstateAppContext = realEstateAppContext;
        }

        /// <summary>
        /// Addition of property details to db
        /// </summary>
        /// <param name="entity"> Of type PropertyDetails </param>
        /// <returns> An object of type PropertyDetails </returns>
        public async Task<PropertyDetails> Add(PropertyDetails entity)
        {
            var isregistered = await Get(entity.Id);
            if (isregistered == null)
            {
                _realEstateAppContext.Add(entity);
                var res = await _realEstateAppContext.SaveChangesAsync();
                return entity;
            }
            else return null;
        }
        /// <summary>
        /// Deletes property details from db
        /// </summary>
        /// <param name="key">email of property details</param>
        /// <returns>deleted data</returns>
        public async Task<PropertyDetails> Delete(int key)
        {
            var property  = await Get(key);
            if (property  != null)
            {
                _realEstateAppContext.Remove(property );
                await _realEstateAppContext.SaveChangesAsync();
                return property ;
            }
            return null;
        }

        /// <summary>
        /// Read data of one property details from db
        /// </summary>
        /// <param name="key">email of property details</param>
        /// <returns>data of property details</returns>
        public async Task<PropertyDetails> Get(int key)
        {
            return (await _realEstateAppContext.PropertyDetails.SingleOrDefaultAsync(u => u.Id == key)) ?? null;
        }

        /// <summary>
        /// Get PropertyDetails by Property id
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<PropertyDetails> GetByPropertyId(int pid)
        {
            return await _realEstateAppContext.PropertyDetails.SingleOrDefaultAsync(o => o.PId == pid);
        }


        /// <summary>
        /// Returns list of all property detailss from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PropertyDetails>> GetAll()
        {
            return await _realEstateAppContext.PropertyDetails.ToListAsync();
        }

        /// <summary>
        /// Updates the property details data
        /// </summary>
        /// <param name="entity">Of type PropertyDetails</param>
        /// <returns> An object of type PropertyDetails </returns>
        public async Task<PropertyDetails> Update(PropertyDetails entity)
        {
            var property  = await Get(entity.Id);
            if (property  != null)
            {
                _realEstateAppContext.Update(entity);
                await _realEstateAppContext.SaveChangesAsync();
                return property ;
            }
            return null;
        }
    }
}
