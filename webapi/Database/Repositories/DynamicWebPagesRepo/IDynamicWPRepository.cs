using Microsoft.AspNetCore.Mvc;

public interface IDynamicWPRepository
{
    Task<IEnumerable<DynamicWPInformationModel>> GetAllAsync();
}