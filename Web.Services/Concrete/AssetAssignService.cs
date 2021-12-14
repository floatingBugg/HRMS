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
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;

        public AssetAssignService(IConfiguration config, IUnitOfWork uow, IHRMSAssetAssignRepository hrmsassetassignRepository)
        {
            _config = config;
            _uow = uow;
            
            _hrmsassetassignRepository = hrmsassetassignRepository;
        }

        public BaseResponse DeleteAssetAssign(int id)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == id).Count() > 0;
            if (count == true)
            {
                _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == id)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItasIsDelete = true;

                    });



                response.Success = true;
                response.Message = UserMessages.strDeleted;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;
            }
            _uow.Commit();
            return response;
        }

        public BaseResponse DisplayAllAssign()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetassignRepository.Table.Count() > 0;
            var assetdata = _hrmsassetassignRepository.Table.Where(z => z.ItasIsDelete == false).Select(x => new DisplayAssetAssignCredential
            {
                assignid = x.ItasAssignId,
                assetname = x.ItasAssetName,
                quantity = x.ItasQuantity,
                assingedname =x.ItasAssignedToName,
                
            }).ToList().OrderByDescending(x => x.assetid);

            if (count == true)
            {
                response.Data = assetdata;
                response.Success = true;
                response.Message = UserMessages.strSuccess;


            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }
            return response;
        }
    }
}
