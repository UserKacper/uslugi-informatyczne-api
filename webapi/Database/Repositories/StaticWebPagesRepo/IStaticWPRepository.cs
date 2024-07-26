public interface IStaticWPRepository
{
    Task<IEnumerable<StaticWPInformationModel>> GetAllAsync();
}