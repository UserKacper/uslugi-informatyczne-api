public interface ISmartHomeRepository
{
    Task<IEnumerable<SmartHomeInformation>> GetAllAsync();
}