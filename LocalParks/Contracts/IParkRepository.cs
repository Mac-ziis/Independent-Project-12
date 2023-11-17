using LocalParks.Models;

namespace LocalParks.Contracts
{
    public interface IParkRepository : IRepositoryBase<Book>
    {
        PagedList<Park> GetParks(PagedParameters parkParameters);
        Book GetParkById(Guid bookId);
    }
}