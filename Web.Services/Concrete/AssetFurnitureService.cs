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
    public class AssetFurnitureService : IAssetFurnitureService
    {
        private readonly IHRMSIMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSIMSAssetFurnitureRepository _hrmsassetfurnitureRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;

        public AssetFurnitureService(IConfiguration config, IHRMSIMSAssetRepository hrmsassetRepository, IHRMSIMSAssetFurnitureRepository hrmsassetfurnitureRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetfurnitureRepository = hrmsassetfurnitureRepository;
            _uow = uow;
        }


        public BaseResponse CreateAssetFurniture(AssetFurnitureCredential furniture)
        {
            BaseResponse response = new BaseResponse();
            List<ImsAssets> asset = new List<ImsAssets>();
            List<ImsFurniture> assetfurniture = new List<ImsFurniture>();
            if (!string.IsNullOrEmpty(furniture.assetName))
            {
                asset.Add(new ImsAssets
                {
                    ItaAssetName = furniture.assetName,
                    ItaQuantity = furniture.quantity,
                    ItaCost = furniture.cost,
                    ItaPurchaseDate = furniture.purchaseDate.Date,
                    ItaCreatedBy = "Admin",
                    ItaCreatedByName = "Admin",
                    ItaCreatedByDate = DateTime.Now.Date,
                    ItaIsDelete = false

                });
                _hrmsassetRepository.Insert(asset);
                assetfurniture.Add(new ImsFurniture
                {
                    ItaAssetId = asset.FirstOrDefault().ItaAssetId,
                    ItfType = furniture.type,
                    ItfCreatedBy = "Admin",
                    ItfCreatedByName = "Admin",
                    ItfIsDelete = false

                });
                _hrmsassetfurnitureRepository.Insert(assetfurniture);


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

        public BaseResponse UpdateAssestFurniture(AssetFurnitureCredential furniture)
        {
            throw new NotImplementedException();
        }

        public BaseResponse DeleteAssestFurniture(int id)
        {
            throw new NotImplementedException();
        }
    }
}
