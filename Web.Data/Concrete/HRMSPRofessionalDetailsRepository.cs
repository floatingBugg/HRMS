using Web.Data.Db_Context;
using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Data.Concrete
{
    public class HRMSPRofessionalDetailsRepository : GenericRepository<EmsTblEmployeeProfessionalDetails>, IHRMSProfessionalDetailsRepository
    {

        public HRMSPRofessionalDetailsRepository(DbHRMSContext context)
              : base(context)
        {

        }
    }
}