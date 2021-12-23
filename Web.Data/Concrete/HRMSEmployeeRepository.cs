using Web.Data.Db_Context;
using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Data.Concrete
{
    public class HRMSEmployeeRepository : GenericRepository<EmsTblEmployeeDetails>, IHRMSEmployeeRepository
    {
        
        public HRMSEmployeeRepository(DbHRMSContext context)
              : base(context)
        {
            
        }

    }
}
