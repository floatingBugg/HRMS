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
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;

        public AssetAssignService(IConfiguration config, IUnitOfWork uow, IHRMSAssetAssignRepository hrmsassetassignRepository)
        {
            _config = config;
            _uow = uow;
            
            _hrmsassetassignRepository = hrmsassetassignRepository;
        }

        public BaseResponse AssignAsset(AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();
            List<ImsTblAssign> assetassign = new List<ImsTblAssign>();

            if (!string.IsNullOrEmpty(assign.assetname))
            {
                assetassign.Add(new ImsTblAssign
                {
                    ItasAssetName=assign.assetname,
                    ItasQuantity=assign.quantity,
                    ItasAssetIdFk=assign.assetId,
                    ItasAssignedToId=assign.assigntoId,
                    ItasAssignedToName=assign.assigntoName,
                    ItasCreatedBy=assign.createdby,
                    ItasCreatedByName=assign.creatbyname,
                    ItasCreatedByDate=DateTime.Now,
                    ItasIsDelete=false


                });

                _hrmsassetassignRepository.Insert(assetassign);
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


        public BaseResponse UpdateAssignAsset(AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assign.assignId).Count() > 0;
            if (count == true)
            {
                _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assign.assignId)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItasAssetName = assign.assetname;
                        x.ItasQuantity = assign.quantity;
                        x.ItasAssignedToName = assign.assigntoName;
                        x.ItasAssignedToId = assign.assigntoId;
                        x.ItasAssetIdFk = assign.assetId;
                        x.ItasIsDelete = false;
                        x.ItasModifiedBy = assign.modifiedby;
                        x.ItasModifiedByName = assign.modifiedbyname;
                        x.ItasModifiedByDate = DateTime.Now.Date;
                        x.ItasIsDelete = false;

                    });
                _uow.Commit();

                response.Success = true;
                response.Message = UserMessages.strUpdated;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;
            }

            return response;
        }
    }
}
