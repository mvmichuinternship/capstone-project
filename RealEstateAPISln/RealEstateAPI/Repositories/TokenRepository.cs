using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Exceptions;

namespace RealEstateAPI.Repositories
{
    public class TokenRepository : IRepository<string, TokenData>
    {
        private RealEstateAppContext _realEstateAppContext;
        public TokenRepository(RealEstateAppContext realEstateAppContext)
        {
            _realEstateAppContext = realEstateAppContext;
        }

        /// <summary>
        /// Addition of user passwords to db
        /// </summary>
        /// <param name="entity"> Of type TokenData </param>
        /// <returns> An object of type TokenData </returns>
        public async Task<TokenData> Add(TokenData entity)
        {
            var isregistered = await Get(entity.UserEmail);
            if (isregistered == null)
            {
                _realEstateAppContext.Add(entity);
                var res = await _realEstateAppContext.SaveChangesAsync();
                return entity;
            }
            else return null;
        }
        /// <summary>
        /// Deletes data from db
        /// </summary>
        /// <param name="key">email of user</param>
        /// <returns>deleted data</returns>
        public async Task<TokenData> Delete(string key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _realEstateAppContext.Remove(user);
                await _realEstateAppContext.SaveChangesAsync();
                return user;
            }
            return null;
        }

        /// <summary>
        /// Read data of one user from db
        /// </summary>
        /// <param name="key">email of user</param>
        /// <returns>data of user</returns>
        public async Task<TokenData> Get(string key)
        {
            return (await _realEstateAppContext.TokenData.SingleOrDefaultAsync(u => u.UserEmail == key)) ?? null;
        }

        public async Task<TokenData> GetByPhoneNumber(string phoneNumber)
        {
            return await _realEstateAppContext.TokenData.SingleOrDefaultAsync(o => o.Phone == phoneNumber);
        }

        /// <summary>
        /// Returns list of all users passwords from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TokenData>> GetAll()
        {
            return await _realEstateAppContext.TokenData.ToListAsync();
        }

        /// <summary>
        /// Updates the user password data
        /// </summary>
        /// <param name="entity">Of type TokenData</param>
        /// <returns> An object of type TokenData </returns>
        public async Task<TokenData> Update(TokenData entity)
        {
            var user = await Get(entity.UserEmail);
            if (user != null)
            {
                _realEstateAppContext.Update(entity);
                await _realEstateAppContext.SaveChangesAsync();
                return user;
            }
            return null;
        }
    }
}
