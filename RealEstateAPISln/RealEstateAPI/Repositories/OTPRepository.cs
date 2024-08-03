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
