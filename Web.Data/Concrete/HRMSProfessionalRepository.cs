using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Interfaces;
using Web.DLL.Db_Context;
using Web.DLL.Generic_Repository;
using Web.DLL.Models;

namespace Web.Data.Concrete
{
    public class HRMSProfessionalRepository : GenericRepository<EmsTblProfessionalQualification>, IHRMSProfessionalRepository
    {

        public HRMSProfessionalRepository(DbHRMSContext context)
              : base(context)
        {

        }

    }
}
