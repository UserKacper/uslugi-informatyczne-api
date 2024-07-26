public interface ISmartHomeRepository
{
    Task<IEnumerable<SmartHomeInformatioModel>> GetAllAsync();
}