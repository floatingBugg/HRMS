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
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
       
        public AssetAssignService(IConfiguration config, IHRMSAssetAssignRepository hrmsassetassignRepository, IHRMSAssetRepository hrmsassetRepository,IHRMSEmployeeRepository hrmsemployeeRepository, IUnitOfWork uow)
        {
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _config = config;
            _hrmsassetassignRepository = hrmsassetassignRepository;
            _hrmsassetRepository = hrmsassetRepository;
            _uow = uow;
        }
        public BaseResponse createAssign(AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();
            var remaining =_hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.assetid).Select(y => y.ItaRemaining).FirstOrDefault();
            var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.assetid).Select(y => y.ItaAssignQuantity).FirstOrDefault();
            assigned = assigned + assign.quantity;
            remaining = remaining - assign.quantity;
            if (!string.IsNullOrEmpty(assign.createdby)&& remaining>0)
            {
                List<ImsAssign> asset = new List<ImsAssign>();
                
                asset.Add(new ImsAssign
                {
                    ItasItaAssetId=assign.assetid,
                    ItasEtedEmployeeId=assign.empid,
                    ItasItacCategoryId=assign.categoryid,
                    ItasQuantity=assign.quantity,
                    ItasAssignedDate=DateTime.Now,
                    ItasCreatedBy=assign.createdby,
                    ItasCreatedByName=assign.createdbyname,
                    ItasCreatedByDate=DateTime.Now,
                   

                });
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.assetid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assigned;
                          x.ItaRemaining = remaining;

                      });
                _hrmsassetassignRepository.Insert(asset);
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

        public BaseResponse deleteAssign(int assignid)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsassetassignRepository.Table.Count(p => p.ItasAssignId == assignid) > 0;
            bool isDeletedAlready = _hrmsassetassignRepository.Table.Count(p => p.ItasIsDelete == true && p.ItasAssignId == assignid) > 0;
            var remaining = _hrmsassetassignRepository.Table.Where(x => x.ItasAssignId == assignid).Select(y => y.ItasQuantity).FirstOrDefault();
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assignid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItasQuantity = 0;
                          x.ItasIsDelete = true;
                      });
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assignid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaRemaining = x.ItaRemaining+remaining;
                          x.ItaAssignQuantity = x.ItaAssignQuantity - remaining;
                      });
                

                _uow.Commit();

                response.Message = UserMessages.strDeleted;
                response.Success = true;
                response.Data = assignid;
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

        public BaseResponse displayAllAssetAssigned(int type)
        {
            BaseResponse response = new BaseResponse();
            
            bool count = _hrmsassetRepository.Table.Count() > 0;
            var assetData = _hrmsassetRepository.Table.Where(z =>z.ItacCategoryId==type && z.ItaIsDelete == false).Select(x => new AssetAssignGrid()
            {
                
                assetname = x.ItaAssetName,
                model = x.ItaModel,
                companyname = x.ItaCompanyName,
                type = x.ItaType,
                quantity=x.ImsAssign.Count > 0 ? x.ImsAssign.Where(y=>y.ItasItaAssetId==x.ItaAssetId).Select(z=>z.ItasQuantity).FirstOrDefault() : 0

            }).ToList().OrderByDescending(x => x.assetid);

            if (count == true)
            {
                response.Data = assetData;
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

        public BaseResponse updateAssign(AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();
            var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.assetid).Select(y => y.ItaAssignQuantity).FirstOrDefault();
            var remaining = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.assetid).Select(y => y.ItaRemaining).FirstOrDefault();
            remaining = remaining + assigned;
            assigned = assigned - assigned;
            bool count = _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assign.assignid).Count() > 0;
            if (count == true)
            {
                _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assign.assignid)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ItasItaAssetId = assign.assetid;
                        x.ItasEtedEmployeeId = assign.empid;
                        x.ItasItacCategoryId = assign.categoryid;
                        x.ItasQuantity = assign.quantity;
                        x.ItasAssignedDate = assign.assigndate;
                        x.ItasModifiedBy = assign.modifiedby;
                        x.ItasModifiedByName = assign.modifiedbyname;
                        x.ItasModifiedByDate = DateTime.Now;
                    });
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.assetid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assigned;
                          x.ItaRemaining = remaining;

                      });

                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.assetid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assign.quantity;
                          x.ItaRemaining = remaining-assign.quantity;

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

        public BaseResponse getEmployee()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsemployeeRepository.Table.Count() > 0;
            var assigndata = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false).Select(x => new DisplayEmployeeGrid()
            {
                empID = (int)x.EtedEmployeeId,
                fullName = x.EtedFirstName + " " + x.EtedLastName


            }).ToList().OrderByDescending(x => x.empID);

            if (count == true)
            {
                response.Data = assigndata;
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
