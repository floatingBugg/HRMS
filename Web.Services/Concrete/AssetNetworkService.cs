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


        public BaseResponse CreateAssetnetwork(AssetCredential asset)
        {
            BaseResponse response = new BaseResponse();

            if (!string.IsNullOrEmpty(asset.assetname))

            {
                List<ImsTblAssets> assetnetwork = new List<ImsTblAssets>();

                assetnetwork.Add(new ImsTblAssets
                {
                    ItaAssetName = asset.assetname,
                    ItaQuantity = asset.quantity,
                    ItaCost = asset.cost,
                    ItaPurchaseDate = DateTime.Now,
                    ItaAssignedTo = asset.assignedto,

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
            throw new NotImplementedException();
        }

        public BaseResponse DisplayAssetNetwork()
        {
            throw new NotImplementedException();
        }

        public BaseResponse UpdateAssetNetwork(AssetCredential asset)
        {
            throw new NotImplementedException();
        }
    }
}
