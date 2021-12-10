﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Db_Context;
using Web.Data.Generic_Repository;
using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Data.Concrete
{
    public class HRMSAssetRepository : GenericRepository<ImsAssets>, IHRMSIMSAssetRepository
    {

        public HRMSAssetRepository(DbHRMSContext context)
              : base(context)
        {

        }

    }
}
