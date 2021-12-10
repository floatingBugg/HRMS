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
        private readonly IHRMSIMSAssetACRepository _hrmsassetacRepository;
        private readonly IHRMSIMSAssetFurnitureRepository _hrmsassetfurnitureRepository;

        IConfiguration _config;
        private readonly UnitOfWork unitorWork;
        public AssetService(IConfiguration config, IHRMSIMSAssetRepository hrmsassetRepository, IHRMSIMSAssetLaptopRepository hrmsassetlaptopRepository, IHRMSIMSAssetACRepository hrmsassetacRepository, IHRMSIMSAssetFurnitureRepository  hrmsassetfurnitureRepository )

        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetlaptopRepository = hrmsassetlaptopRepository;
             _hrmsassetacRepository = hrmsassetacRepository;
            _hrmsassetfurnitureRepository = hrmsassetfurnitureRepository;

        }

        public BaseResponse CreateAssetLaptop(AssetLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();
            List<ImsAssets> asset = new List<ImsAssets>();
            List<ImsLaptop> assetlaptop = new List<ImsLaptop>();
            if (!string.IsNullOrEmpty(laptop.assetName)) {
                asset.Add(new ImsAssets
                {
                    ItaAssetName = laptop.assetName,
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


        public BaseResponse CreateAssetFurniture(AssetFurnitureCredential furniture)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Where(p => p.ItaAssetId == laptop.assestID).Count()> 0;
            if (count == true) { 
            _hrmsassetRepository.Table.Where(p => p.ItaAssetId == laptop.assestID)
                .ToList()
                .ForEach(x =>
                {
                    x.ItaAssetName = laptop.assetName;
                    x.ItaQuantity = laptop.quantity;
                    x.ItaCost = laptop.cost;
                    x.ItaPurchaseDate = laptop.purchaseDate.Date;
                    x.ItaCreatedBy = "Admin";
                    x.ItaCreatedByName = "Admin";
                    x.ItaCreatedByDate = DateTime.Now.Date;
                    x.ItaIsDelete = false;

                });
            _hrmsassetlaptopRepository.Table.Where(p => p.ItaAssetId == laptop.assestID)
              .ToList()
              .ForEach(x =>
              {

                  x.ItlCompanyName = laptop.companyName;
                  x.ItlGeneration = laptop.generation;
                  x.ItlSerialNo = laptop.serialno;
                  x.ItlRam = laptop.ram;
                  x.ItlHdd = laptop.hdd;
                  x.ItlProcessor = laptop.processor;
                  x.ItlCreatedBy = "Admin";
                  x.ItlCreatedByName = "Admin";
                  x.ItlIsDelete = false;

                });
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
                _hrmsassetfurnitureRepository.Insert(assetfurniture);
               

            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
            return response;
        }
        public BaseResponse CreateAssetAc(AssetAcCredential Ac)
        {
            BaseResponse response = new BaseResponse();
            List<ImsAssets> asset = new List<ImsAssets>();
            List<ImsAc> assetac = new List<ImsAc>();
            if (!string.IsNullOrEmpty(Ac.assestName))
            {
                asset.Add(new ImsAssets
                {
                    ItaAssetName = Ac.assestName,
                    ItaQuantity = Ac.quantity,
                    ItaCost = Ac.cost,
                    ItaPurchaseDate = Ac.purchaseDate.Date,
                    ItaCreatedBy = "Admin",
                    ItaCreatedByName = "Admin",
                    ItaCreatedByDate = DateTime.Now.Date,
                    ItaIsDelete = false


                });
                _hrmsassetRepository.Insert(asset);
                assetac.Add(new ImsAc
                {
                    ItaAssetId = asset.FirstOrDefault().ItaAssetId,
                    ItaCompanyName = Ac.companyname,
                    ItaSize = Ac.size,
                    ItaCreatedBy = "Admin",
                    ItaCreatedByName = "Admin",
                    ItaIsDelete = false

                });
                _hrmsassetacRepository.Insert(assetac);



                response.Success = true;
                response.Message = UserMessages.strAdded;
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

