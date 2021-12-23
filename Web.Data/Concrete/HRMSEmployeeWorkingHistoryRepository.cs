using Web.Data.Db_Context;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Data.Generic_Repository;

namespace Web.Data.Concrete
{
    public class HRMSEmployeeWorkingHistoryRepository : GenericRepository<EmsTblWorkingHistory>, IHRMSEmployeeWorkingHistoryRepository
    {
        public HRMSEmployeeWorkingHistoryRepository(DbHRMSContext context)
            : base(context)
        {

        }
    }
}
