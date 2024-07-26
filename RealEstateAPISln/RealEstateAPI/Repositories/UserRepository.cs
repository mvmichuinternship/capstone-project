using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Exceptions;

namespace RealEstateAPI.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private RealEstateAppContext _realEstateAppContext;
        public UserRepository(RealEstateAppContext realEstateAppContext) 
        {
            _realEstateAppContext = realEstateAppContext;
        }

    /// <summary>
    /// Addition of user to db
    /// </summary>
    /// <param name="entity"> Of type User </param>
    /// <returns> An object of type User </returns>
        public async Task<User> Add(User entity)
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
        /// Deletes user from db
        /// </summary>
        /// <param name="key">email of user</param>
        /// <returns>deleted data</returns>
        public async Task<User> Delete(string key)
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
        public async Task<User> Get(string key)
        {
            return (await _realEstateAppContext.Users.SingleOrDefaultAsync(u => u.UserEmail == key)) ?? null;
        }

        /// <summary>
        /// Get User by Phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<User> GetByPhoneNumber(string phoneNumber)
        {
            return await _realEstateAppContext.Users.SingleOrDefaultAsync(o => o.Phone == phoneNumber);
        }

        /// <summary>
        /// Returns list of all users from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _realEstateAppContext.Users.ToListAsync();
        }

        /// <summary>
        /// Updates the user data
        /// </summary>
        /// <param name="entity">Of type User</param>
        /// <returns> An object of type User </returns>
        public async Task<User> Update(User entity)
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
