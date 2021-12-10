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
    public class AssetService : IAssetService
    {
        private readonly IHRMSIMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSIMSAssetLaptopRepository _hrmsassetlaptopRepository;
        private readonly IHRMSIMSAssetFurnitureRepository _hrmsassetfurnitureRepository;
        IConfiguration _config;
        private readonly UnitOfWork unitorWork;
        public AssetService(IConfiguration config, IHRMSIMSAssetRepository hrmsassetRepository, IHRMSIMSAssetLaptopRepository hrmsassetlaptopRepository, IHRMSIMSAssetFurnitureRepository hrmsassetfurnitureRepository)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetlaptopRepository = hrmsassetlaptopRepository;
            _hrmsassetfurnitureRepository = hrmsassetfurnitureRepository;
        }

        public BaseResponse CreateAssestLaptop(AssestLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();
            List<ImsAssets> asset = new List<ImsAssets>();
            List<ImsLaptop> assetlaptop = new List<ImsLaptop>();
            if (!string.IsNullOrEmpty(laptop.assestName)) {
                asset.Add(new ImsAssets
                {
                    ItaAssetName = laptop.assestName,
                    ItaQuantity = laptop.quantity,
                    ItaCost = laptop.cost,
                    ItaPurchaseDate = laptop.purchaseDate.Date,
                    ItaCreatedBy = "Admin",
                    ItaCreatedByName = "Admin",
                    ItaCreatedByDate = DateTime.Now.Date,
                    ItaIsDelete = false

                });
                _hrmsassetRepository.Insert(asset);
                assetlaptop.Add(new ImsLaptop
                {
                    ItaAssetId = asset.FirstOrDefault().ItaAssetId,
                    ItlCompanyName = laptop.companyName,
                    ItlGeneration = laptop.generation,
                    ItlSerialNo = laptop.serialno,
                    ItlRam = laptop.ram,
                    ItlHdd = laptop.hdd,
                    ItlProcessor = laptop.processor,
                    ItlCreatedBy = "Admin",
                    ItlCreatedByName = "Admin",
                    ItlIsDelete = false

                });
                _hrmsassetlaptopRepository.Insert(assetlaptop);


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

        public BaseResponse CreateAssestFurniture(AssetFurnitureCredential furniture)
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
    }
    }

