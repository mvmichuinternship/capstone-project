//using Microsoft.EntityFrameworkCore;
//using RealEstateAPI.Context;
//using RealEstateAPI.Interfaces;
//using RealEstateAPI.Models;
//using RealEstateAPI.Exceptions;

//namespace RealEstateAPI.Repositories
//{
//    public class OTPRepository : IRepository<string, OTP>
//    {
//        private RealEstateAppContext _realEstateAppContext;
//        public OTPRepository(RealEstateAppContext realEstateAppContext)
//        {
//            _realEstateAppContext = realEstateAppContext;
//        }

//        /// <summary>
//        /// Addition of otp data to db
//        /// </summary>
//        /// <param name="entity"> Of type OTP </param>
//        /// <returns> An object of type OTP </returns>
//        public async Task<OTP> Add(OTP entity)
//        {
//            var isregistered = await Get(entity.Phone);
//            if (isregistered == null)
//            {
//                _realEstateAppContext.Add(entity);
//                var res = await _realEstateAppContext.SaveChangesAsync();
//                return entity;
//            }
//            else return null;
//        }
//        /// <summary>
//        /// Deletes otp data from db
//        /// </summary>
//        /// <param name="key">email of otp data</param>
//        /// <returns>deleted data</returns>
//        public async Task<OTP> Delete(string key)
//        {
//            var otp  = await Get(key);
//            if (otp  != null)
//            {
//                _realEstateAppContext.Remove(otp );
//                await _realEstateAppContext.SaveChangesAsync();
//                return otp ;
//            }
//            return null;
//        }

//        /// <summary>
//        /// Read data of one otp data from db
//        /// </summary>
//        /// <param name="key">email of otp data</param>
//        /// <returns>data of otp data</returns>
//        public async Task<OTP> Get(string key)
//        {
//            return (await _realEstateAppContext.OTPs.SingleOrDefaultAsync(u => u.Phone == key)) ?? null;
//        }

//        /// <summary>
//        /// Returns list of all otp datas from db
//        /// </summary>
//        /// <returns></returns>
//        public async Task<IEnumerable<OTP>> GetAll()
//        {
//            return await _realEstateAppContext.OTPs.ToListAsync();
//        }

//        /// <summary>
//        /// Updates the otp data data
//        /// </summary>
//        /// <param name="entity">Of type OTP</param>
//        /// <returns> An object of type OTP </returns>
//        public async Task<OTP> Update(OTP entity)
//        {
//            var otp  = await Get(entity.Phone);
//            if (otp  != null)
//            {
//                _realEstateAppContext.Update(entity);
//                await _realEstateAppContext.SaveChangesAsync();
//                return otp ;
//            }
//            return null;
//        }
//    }
//}



using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Models;

public class OTPRepository 
{
    private readonly RealEstateAppContext _dbContext;

    public OTPRepository(RealEstateAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OTP> GetByPhoneNumber(string phoneNumber)
    {
        return await _dbContext.OtpRecords.SingleOrDefaultAsync(o => o.Phone == phoneNumber) ?? null;
    }

    public async Task<OTP>  Add(OTP otpRecord)
    {
        _dbContext.OtpRecords.Add(otpRecord);
         await _dbContext.SaveChangesAsync();
        return otpRecord;
    }

    public async Task<OTP> Remove(OTP otpRecord)
    {
        _dbContext.OtpRecords.Remove(otpRecord);
        await _dbContext.SaveChangesAsync();
        return otpRecord;
    }

    
}
