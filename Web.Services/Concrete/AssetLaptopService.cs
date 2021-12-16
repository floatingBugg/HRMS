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
    public class AssetLaptopService : IAssetLaptopService
    {
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetLaptopService(IConfiguration config, IHRMSAssetRepository hrmsassetRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _uow = uow;
        }

        public BaseResponse creatLaptop(AssetLaptopCredential asset)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(asset.assetname)){
                List<ImsTblAssets> laptop = new List<ImsTblAssets>();

                laptop.Add(new ImsTblAssets
                {
                    ItaAssetName=asset.assetname,
                    ItaSerialNo=asset.serialno,
                    ItacCategoryId=asset.categoryid,
                    EtedEmployeeId=asset.empid,
                    ItaGeneration=asset.generation,
                    ItaRam=asset.ram,
                    ItaProcessor=asset.processor,
                    ItaStorage=asset.storage,
                    ItaHardriveType=asset.hardtype,
                    ItaCompanyName=asset.companyname,
                    ItaQuantity=asset.quantity,
                    ItaCost=asset.cost,
                    ItaPurchaseDate=asset.purchaseddate,
                    ItaCreatedBy=asset.createdby,
                    ItaCreatedByName=asset.createdbyname,
                    ItaAssign = false,
                    ItaCreatedByDate =DateTime.Now,
                    ItaIsDelete=false,
                    
                    
                });
                _hrmsassetRepository.Insert(laptop);
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

        public BaseResponse updatelaptop(AssetLaptopCredential asset)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsassetRepository.Table.Where(p => p.ItaAssetId == asset.assetid).Count() > 0;
            if (count == true)
            {
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == asset.assetid)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItaAssetName = asset.assetname;
                        x.ItaSerialNo = asset.serialno;
                        x.ItacCategoryId = asset.categoryid;
                        x.EtedEmployeeId = asset.empid;
                        x.ItaGeneration = asset.generation;
                        x.ItaRam = asset.ram;
                        x.ItaProcessor = asset.processor;
                        x.ItaStorage = asset.storage;
                        x.ItaHardriveType = asset.hardtype;
                        x.ItaCompanyName = asset.companyname;
                        x.ItaQuantity = asset.quantity;
                        x.ItaCost = asset.cost;
                        x.ItaPurchaseDate = asset.purchaseddate;
                        x.ItaModifiedBy = asset.modifiedby;
                        x.ItaModifiedByName = asset.modifiedbyname;
                        x.ItaModifiedByDate = DateTime.Now;

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
        
        public BaseResponse displayAllLaptop(int type)
        {
            throw new NotImplementedException();
        }

        public BaseResponse getLaptopByID(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse deleteLaptop(int assetid)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsassetRepository.Table.Count(p => p.ItaAssetId == assetid) > 0;
            bool isDeletedAlready = _hrmsassetRepository.Table.Count(p => p.ItaIsDelete == true && p.ItaAssetId == assetid) > 0;
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assetid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaIsDelete = true;
                      });
                    _uow.Commit();
            
                response.Message = UserMessages.strDeleted;
                response.Success = true;
                response.Data = assetid;
            }
            else if (isDeletedAlready == true && doesExistAlready == true)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;
            }
            else if (doesExistAlready == false)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }

            return response;
        }

        public BaseResponse sumOfLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            var totalCost = _hrmsassetRepository.Table.Where(y => y.ItacCategoryId == categoryid).Sum(x => x.ItaCost * x.ItaQuantity);
            var totalAssetData = _hrmsassetRepository.Table.Where(p => p.ItacCategoryId == categoryid).ToList();
            var result = (from a in totalAssetData
                          select new AssetTotalVM()
                          {
                              cost = a.ItaCost,
                              quantity = a.ItaQuantity,
                              totalCost = Convert.ToInt32(a.ItaQuantity * a.ItaCost),
                              subTotalCost = _hrmsassetRepository.Table.Sum(x => x.ItaCost * x.ItaQuantity)
                          }).ToList();
            
            response.Data = totalCost;
            response.Success = false;
            response.Message = "Sum Caluclated";
            return response;
        }

        public BaseResponse totalQuantityLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            var totalQuan = _hrmsassetRepository.Table.Where(y=>y.ItacCategoryId==categoryid).Sum(x => x.ItaQuantity);
            response.Data = totalQuan;
            response.Success = false;
            response.Message = "Total Quantity Caluclated";
            return response;
        }
    }

    }


