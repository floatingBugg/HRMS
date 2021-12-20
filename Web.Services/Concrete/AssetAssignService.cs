using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class AssetAssignService : IAssetAssignService
    {
        private readonly IHRMSAssetAssignRepository _hrmsassetassignRepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetAssignService(IConfiguration config, IHRMSAssetAssignRepository hrmsassetassignRepository, IHRMSEmployeeRepository hrmsemployeeRepository, IUnitOfWork uow)
        {
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _config = config;
            _hrmsassetassignRepository = hrmsassetassignRepository;
            _uow = uow;
        }
        public BaseResponse createAssign(AssetAssignCredential assign)
        {
            throw new NotImplementedException();
        }

        public BaseResponse deleteAssign(int assetid)
        {
            throw new NotImplementedException();
        }

        public BaseResponse displayAllAssetAssigned(int type)
        {
            throw new NotImplementedException();
        }

        public BaseResponse updateAssign(AssetCredential asset)
        {
            throw new NotImplementedException();
        }
    }
}
