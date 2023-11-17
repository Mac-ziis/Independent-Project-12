using LocalParks.Models;

namespace LocalParks.Contracts
{
    public interface IParkRepository : IRepositoryBase<Park>
    {
        PagedList<Park> GetParks(PagedParameters parkParameters);
        Park GetParkById(Guid parkId);
    }
}