public interface ISoftwareRepository
{
    Task<IEnumerable<SoftwareInformationModel>> GetAllAsync();
}