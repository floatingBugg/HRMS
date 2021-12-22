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
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetService(IConfiguration config, IHRMSAssetRepository hrmsassetRepository,IHRMSEmployeeRepository hrmsemployeeRepository, IUnitOfWork uow)
        {
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _uow = uow;
        }

        public BaseResponse createAsset(AssetCredential asset)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(asset.assetname)){
                List<ImsAssetsVM> laptop = new List<ImsAssetsVM>();

                laptop.Add(new ImsAssets
                {
                    ItaAssetName=asset.assetname,
                    ItacCategoryId = asset.categoryid,
                    ItaSerialNo =asset.serialno,
                    ItaType=asset.type,
                    ItaModel=asset.model,
                    ItaSize=asset.size,
                    ItaCondition=asset.condition,
                    ItaGeneration=asset.generation,
                    ItaRam=asset.ram,
                    ItaProcessor=asset.processor,
                    ItaStorage=asset.storage,
                    ItaHardriveType=asset.hardtype,
                    ItaCompanyName=asset.companyname,
                    ItaQuantity=asset.quantity,
                    ItaRemaining= asset.quantity,
                    ItaAssignQuantity = 0,
                    ItaCost =asset.cost,
                    ItaPurchaseDate=asset.purchaseddate,
                    ItaCreatedBy=asset.createdby,
                    ItaCreatedByName=asset.createdbyname,
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

        public BaseResponse updateAsset(AssetCredential asset)
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
        
        public BaseResponse displayAllAssetUnAssigned(int type)
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetRepository.Table.Count() > 0;
            var empid = _hrmsassetRepository.Table.Where(z => z.ItaIsDelete == false && z.ItacCategoryId == type)/*.Select(x => x.EtedEmployeeId)*/;
            var assetData = _hrmsassetRepository.Table.Where(z => z.ItaIsDelete == false && z.ItacCategoryId == type && z.ItaRemaining>0).Select(x =>  new AssetCredential
            {   assetname=x.ItaAssetName,
                serialno=x.ItaSerialNo,
                companyname = x.ItaCompanyName,
                processor = x.ItaProcessor,
                quantity=x.ItaQuantity,
                remaining=x.ItaRemaining,
                cost=x.ItaCost,
                model=x.ItaModel,
                type=x.ItaType,
                condition=x.ItaCondition,
                size=x.ItaSize,
                storage=x.ItaStorage,
                ram = x.ItaRam,
                generation=x.ItaGeneration,
                /*assingedname =ham*/ /*_hrmsemployeeRepository.Table.Where(y=>y.EtedEmployeeId==x.EtedEmployeeId).Select(z=>z.EtedFirstName).FirstOrDefault()*/
            }).ToList().OrderByDescending(x => x.assetid);
            response.Data = assetData;
            response.Success = false;
            response.Message = "Show record";
            return response;
        }

        public BaseResponse getAssetByID(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsassetRepository.Table.Where(z => z.ItaIsDelete == false && z.ItaAssetId == id).Count() > 0;
            var assetdata=_hrmsassetRepository.Table.Where(x => x.ItaAssetId == id && x.ItaIsDelete == false).Select(x => new AssetCredential
            {
                assetname=x.ItaAssetName,
                serialno=x.ItaSerialNo,
                model=x.ItaModel,
                companyname=x.ItaCompanyName,
                type=x.ItaType,
                size=x.ItaSize,
                condition=x.ItaCondition,
                generation=x.ItaGeneration,
                ram=x.ItaRam,
                processor=x.ItaProcessor,
                storage=x.ItaStorage,
                hardtype=x.ItaHardriveType



            }).ToList().OrderByDescending(x=>x.assetid).Take(10);

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

        public BaseResponse deleteAsset(int assetid)
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

        public BaseResponse sumOfAsset(int categoryid)
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

        public BaseResponse totalQuantityAsset(int categoryid)
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


