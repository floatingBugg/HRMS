using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
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
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(assign.createdby))
            {
                List<ImsAssign> asset = new List<ImsAssign>();

                asset.Add(new ImsAssign
                {
                    ItasItaAssetId=assign.assetid,
                    ItasEtedEmployeeId=assign.empid,
                    ItasItacCategoryId=assign.categoryid,
                    ItasQuantity=assign.quantity,
                    ItasAssignedDate=DateTime.Now,
                    ItasCreatedBy=assign.createdby,
                    ItasCreatedByName=assign.createdbyname,
                    ItasCreatedByDate=DateTime.Now,
                   

                });
                _hrmsassetassignRepository.Insert(asset);
                _uow.Commit();

                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }

            return response;
        }

        public BaseResponse deleteAssign(int assetid)
        {
            throw new NotImplementedException();
        }

        public BaseResponse displayAllAssetAssigned(int type)
        {
            throw new NotImplementedException();
        }


        public BaseResponse updateAssign(AssetAssignCredential assign)
        {
            throw new NotImplementedException();
        }
    }
}
