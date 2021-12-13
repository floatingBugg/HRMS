﻿/*using Microsoft.Extensions.Configuration;
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
    public class AssetAcService: IAssetAcService
    {
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSAssetRepository _hrmsassetacRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;
        public AssetAcService(IConfiguration config, IHRMSAssetRepository hrmsassetRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            
            _uow = uow;

        }
        //ASSET Laptop
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
                response.Message = UserMessages.strNotinsert;
            }
            return response;
        }


        public BaseResponse UpdateAssestAc(AssetAcCredential Ac)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Where(p => p.ItaAssetId == Ac.assestID).Count() > 0;
            if (count == true)
            {
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == Ac.assestID)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItaAssetName = Ac.assestName;
                        x.ItaQuantity = Ac.quantity;
                        x.ItaCost = Ac.cost;
                        x.ItaPurchaseDate = Ac.purchaseDate.Date;
                        x.ItaCreatedBy = "Admin";
                        x.ItaCreatedByName = "Admin";
                        x.ItaCreatedByDate = DateTime.Now.Date;
                        x.ItaIsDelete = false;

                    });
                _hrmsassetacRepository.Table.Where(p => p.ItaAssetId == Ac.assestID)
                  .ToList()
                  .ForEach(x =>
                  {

                      x.ItaCompanyName = Ac.companyname;
                      x.ItaSize = Ac.size;
                      x.ItaCreatedBy = "Admin";
                      x.ItaCreatedByName = "Admin";
                      x.ItaIsDelete = false;

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

        public BaseResponse DeleteAssestAc(int id)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Where(p => p.ItaAssetId == id).Count() > 0;
            if (count == true)
            {
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == id)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItaIsDelete = true;

                    });
                _hrmsassetacRepository.Table.Where(p => p.ItaAssetId == id)
                  .ToList()
                  .ForEach(x =>
                  {
                      x.ItaIsDelete = true;

                  });
                _uow.Commit();

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

            return response;
        }


    }
}
*/