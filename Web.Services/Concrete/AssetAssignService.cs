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
using Web.Model.ViewModel;
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
        public BaseResponse createAssign(ImsAssignVM assign)
        {
            BaseResponse response = new BaseResponse();
            var remaining =_hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.ItasItaAssetId).Select(y => y.ItaRemaining).FirstOrDefault();
            var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.ItasItaAssetId).Select(y => y.ItaAssignQuantity).FirstOrDefault();
            assigned = assigned + assign.ItasQuantity;
            remaining = remaining - assign.ItasQuantity;
            
            if (/*!string.IsNullOrEmpty(assign.ItasQuantity)&&*/ remaining>=0)
            {
                ImsAssign imsAssign = new ImsAssign();

                imsAssign.ItasItacCategoryId = assign.ItasItacCategoryId;
                imsAssign.ItasItaAssetId = assign.ItasItaAssetId;
                imsAssign.ItasEtedEmployeeId = assign.ItasEtedEmployeeId;
                imsAssign.ItasQuantity = assign.ItasQuantity;
                imsAssign.ItasAssignedDate = assign.ItasAssignedDate;
                imsAssign.ItasCreatedBy = assign.ItasCreatedBy;
                imsAssign.ItasCreatedByName = assign.ItasCreatedByName;
                imsAssign.ItasCreatedByDate = DateTime.Now;
                imsAssign.ItasIsDelete = false;


                
                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.ItasItaAssetId)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assigned;
                          x.ItaRemaining = remaining;

                      });
                _hrmsassetassignRepository.Insert(imsAssign);
                _uow.Commit();

                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else if (remaining == 0)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strStockEmpty;
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
            
            bool count = _hrmsassetassignRepository.Table.Where(x=>x.ItasItacCategoryId==type).Count() > 0;
            /*var assignobj = _hrmsassetassignRepository.Table.Where(z => z.ItasItacCategoryId == type && z.ItasIsDelete == false).FirstOrDefault();*/
            var assetData = _hrmsassetassignRepository.Table.Where(z => z.ItasItacCategoryId == type && z.ItasIsDelete == false).Select(x => new AssetAssignGrid()
            {
                assetname = _hrmsassetRepository.Table.Where(y=>y.ItaAssetId==x.ItasItaAssetId).Select(z=>z.ItaAssetName).FirstOrDefault(),
                model = _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaModel).FirstOrDefault(),
                processor= _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaProcessor).FirstOrDefault(),
                generation= _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaGeneration).FirstOrDefault(),
                size= _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaSize ).FirstOrDefault(),
                storage = _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaStorage).FirstOrDefault(),
                ram = _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaRam).FirstOrDefault(),
                companyname = _hrmsassetRepository.Table.Where(y => y.ItaAssetId == x.ItasItaAssetId).Select(z => z.ItaCompanyName).FirstOrDefault(),
                quantity = x.ItasQuantity,
                assingeto = _hrmsemployeeRepository.Table.Where(y => y.EtedEmployeeId == x.ItasEtedEmployeeId && y.EtedIsDelete==false).Select(p=>p.EtedFirstName+" "+p.EtedLastName).FirstOrDefault(),

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

        public BaseResponse updateAssign(ImsAssignVM assign)
        {
            BaseResponse response = new BaseResponse();
            var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.ItasItaAssetId).Select(y => y.ItaAssignQuantity).FirstOrDefault();
            var remaining = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == assign.ItasItaAssetId).Select(y => y.ItaRemaining).FirstOrDefault();
            var qty=_hrmsassetassignRepository.Table.Where(x => x.ItasAssignId == assign.ItasAssignId).Select(y => y.ItasQuantity).FirstOrDefault();
            remaining = remaining + qty;
            assigned = assigned - qty;
            bool count = _hrmsassetassignRepository.Table.Where(p => p.ItasAssignId == assign.ItasAssignId).Count() > 0;
            if (count == true && remaining>0)
            {
                ImsAssign imsAssign = new ImsAssign();

                imsAssign.ItasAssignId = assign.ItasAssignId;
                imsAssign.ItasItacCategoryId = assign.ItasItacCategoryId;
                imsAssign.ItasItaAssetId = assign.ItasItaAssetId;
                imsAssign.ItasEtedEmployeeId = assign.ItasEtedEmployeeId;
                imsAssign.ItasQuantity = assign.ItasQuantity;
                imsAssign.ItasModifiedBy = assign.ItasModifiedBy;
                imsAssign.ItasModifiedByName = assign.ItasModifiedByName;
                imsAssign.ItasModifiedByDate = DateTime.Now;
                imsAssign.ItasIsDelete = false;


                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.ItasItaAssetId)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assigned;
                          x.ItaRemaining = remaining;

                      });


                _hrmsassetRepository.Table.Where(p => p.ItaAssetId == assign.ItasItaAssetId)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = x.ItaAssignQuantity + assign.ItasQuantity;
                          x.ItaRemaining = remaining-assign.ItasQuantity;

                      });

                _hrmsassetassignRepository.Update(imsAssign);
                _uow.Commit();
                response.Success = true;
                response.Message = UserMessages.strUpdated;
                response.Data = null;
            }
            else if (remaining == 0)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strStockEmpty;
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

        public BaseResponse ViewDataAssignByid(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsassetassignRepository.Table.Where(z => z.ItasIsDelete == false && z.ItasAssignId == id).Count() > 0;
            var assignData = _hrmsassetassignRepository.Table.Where(x => x.ItasAssignId == id).ToList();


            if (count == true)
            {
                response.Data = assignData;
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
