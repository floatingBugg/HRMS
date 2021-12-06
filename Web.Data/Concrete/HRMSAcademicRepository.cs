using Web.Data.Db_Context;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Data.Generic_Repository;


namespace Web.Data.Concrete
{
    public class HRMSAcademicRepository : GenericRepository<EmsTblAcademicQualificationVM>, IHRMSAcademicRepository
    {

        public HRMSAcademicRepository(DbHRMSContext context)
              : base(context)
        {

        }

    }
}
