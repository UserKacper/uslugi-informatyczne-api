using Microsoft.AspNetCore.Mvc;

public interface IPricingRepository
{
    Task<IEnumerable<PricingModel>> GetAllAsync(string type);
}