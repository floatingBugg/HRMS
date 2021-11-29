using Web.Data.Db_Context;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Data.Generic_Repository;

namespace Web.Data.Concrete
{
    public class HRMSUserAuthRepository : GenericRepository<EmsTblHrmsUser>, IHRMSUserAuthRepository
	{
		public HRMSUserAuthRepository(DbHRMSContext context)
			  : base(context)
		{

		}
	}
}
