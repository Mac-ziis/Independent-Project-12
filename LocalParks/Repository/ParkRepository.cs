using LocalParks.Contracts;
using LocalParks.Models;

namespace LocalParks.Repository
{
    public class ParkRepository : RepositoryBase<Park>, IParkRepository
    {
        public ParkRepository(LocalParksContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public PagedList<Park> GetParks(PagedParameters parkParameters)
        {
            return PagedList<Park>.ToPagedList(FindAll(),
                parkParameters.PageNumber,
                parkParameters.PageSize);
        }

        public Park GetParkById(Guid parkId)
        {
            return FindByCondition(cust => cust.ParkId.Equals(parkId))
                .DefaultIfEmpty(new Park())
                .FirstOrDefault();
        }

    }
}