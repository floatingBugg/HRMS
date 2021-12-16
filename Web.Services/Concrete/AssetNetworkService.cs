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
    public class AssetNetworkService : IAssetNetworkService
    {

        private readonly IHRMSAssetRepository _hrmsassetRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetNetworkService(IConfiguration config, IHRMSAssetRepository hrmsassetRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _uow = uow;
        }


        public BaseResponse CreateAssetnetwork(AssetCredential network)
        {
            BaseResponse response = new BaseResponse();

            if (!string.IsNullOrEmpty(network.assetname))

            {
                List<ImsTblAssets> assetnetwork = new List<ImsTblAssets>();

                assetnetwork.Add(new ImsTblAssets
                {
                    ItaAssetName = network.assetname,
                    ItaQuantity = network.quantity,
                    ItaCost = network.cost,
                    ItaPurchaseDate = DateTime.Now,
                    ItaAssignedTo = network.assignedto,
                    ItaIsDelete = false
                });
                _uow.Commit();
                _hrmsassetRepository.Insert(assetnetwork);
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
            return response;
        }

        public BaseResponse DeleteAssetNetwork(int id)
        {
            BaseResponse responce = new BaseResponse();
            bool doesExistAlready = _hrmsassetRepository.Table.Count(p => p.ItaAssetId == id) > 0;
            bool isDeletedAlready = _hrmsassetRepository.Table.Count(p => p.ItaAssetId == id && p.ItaIsDelete == true) > 0;

            _hrmsassetRepository.Table.Where(p => p.ItaAssetId == id).ToList().ForEach(x =>
            {

                x.ItaIsDelete = true;

            });
            _uow.Commit();
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                responce.Success = true;
                responce.Data = id;
                responce.Message = UserMessages.strDeleted;
            }
            else if (doesExistAlready == true && isDeletedAlready == true)
            {
                responce.Success = false;
                responce.Message = UserMessages.strAlrdeleted;
                responce.Data = null;
            }

            else if (doesExistAlready == false)
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = UserMessages.strNotfound;
            }
            return responce;
        }


        public BaseResponse DisplayAssetNetwork(int assetid)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Count() > 0;
            var networkdata = _hrmsassetRepository.Table.Where(z => z.ItaIsDelete == false && z.ItaAssetId == assetid).Select(x => new AssetCredential()
            {
                assetid = x.ItaAssetId,
                assetname = x.ItaAssetName,
                quantity = (int)x.ItaQuantity,
                cost = (int)x.ItaCost,
                purchaseddate = DateTime.Now,
                assignedto = x.ItaAssignedTo,



            }).ToList().OrderByDescending(x => x.assetid);

            if (count == true)
            {
                response.Data = networkdata;
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

        public BaseResponse UpdateAssetNetwork(AssetCredential network)
        {
            BaseResponse responce = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Where(p => p.ItaAssetId == network.assetid).Count() > 0;
            if (count == true)
            {
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == network.assetid).ToList().ForEach(x =>
                {
                    x.ItaAssetName = network.assetname;
                    x.ItaQuantity = network.quantity;
                    x.ItaCost = network.cost;
                    x.ItaPurchaseDate = DateTime.Now;
                    x.ItaAssignedTo = network.assignedto;
                    x.ItaIsDelete = false;


                });
                _uow.Commit();
                responce.Success = true;
                responce.Message = UserMessages.strUpdated;
                responce.Data = null;

            }
            else
            {
                responce.Success = false;
                responce.Message = UserMessages.strNotupdated;
                responce.Data = null;
            }
            return responce;
        }
    }
}
