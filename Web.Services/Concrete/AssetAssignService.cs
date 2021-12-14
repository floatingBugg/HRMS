using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class AssetAssignService : IAssetAssignService
    {
        private readonly IHRMSAssetAssignRepository _hrmsassetassignRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;

        public AssetAssignService(IConfiguration config, IUnitOfWork uow, IHRMSAssetAssignRepository hrmsassetassignRepository)
        {
            _config = config;
            _uow = uow;
            
            _hrmsassetassignRepository = hrmsassetassignRepository;
        }


    }
}
