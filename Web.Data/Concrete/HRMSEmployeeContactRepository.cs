using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Data.Db_Context;

namespace Web.Data.Concrete
{
    public class HRMSEmployeeContactRepository : GenericRepository<EmsTblEmergencyContact>, IHRMSEmployeeContactRepository
    {
        public HRMSEmployeeContactRepository(DbHRMSContext context)
             : base(context)
        {

        }

    }
}
