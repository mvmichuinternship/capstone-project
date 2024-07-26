using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;

namespace RealEstateAPI.Repositories
{
    public class MediaRepository : IRepository<int, Media>
    {
        private RealEstateAppContext _realEstateAppContext;
        public MediaRepository(RealEstateAppContext realEstateAppContext)
        {
            _realEstateAppContext = realEstateAppContext;
        }

        /// <summary>
        /// Addition of media to db
        /// </summary>
        /// <param name="entity"> Of type Media </param>
        /// <returns> An object of type Media </returns>
        public async Task<Media> Add(Media entity)
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
        /// Deletes media from db
        /// </summary>
        /// <param name="key">email of media</param>
        /// <returns>deleted data</returns>
        public async Task<Media> Delete(int key)
        {
            var media = await Get(key);
            if (media != null)
            {
                _realEstateAppContext.Remove(media);
                await _realEstateAppContext.SaveChangesAsync();
                return media;
            }
            return null;
        }

        /// <summary>
        /// Read data of one media from db
        /// </summary>
        /// <param name="key">email of media</param>
        /// <returns>data of media</returns>
        public async Task<Media> Get(int key)
        {
            return (await _realEstateAppContext.Medias.SingleOrDefaultAsync(u => u.Id == key)) ?? null;
        }


        /// <summary>
        /// Returns list of all medias from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Media>> GetAll()
        {
            return await _realEstateAppContext.Medias.ToListAsync();
        }

        /// <summary>
        /// Updates the media data
        /// </summary>
        /// <param name="entity">Of type Media</param>
        /// <returns> An object of type Media </returns>
        public async Task<Media> Update(Media entity)
        {
            var media = await Get(entity.Id);
            if (media != null)
            {
                _realEstateAppContext.Update(entity);
                await _realEstateAppContext.SaveChangesAsync();
                return media;
            }
            return null;
        }
    }
}
