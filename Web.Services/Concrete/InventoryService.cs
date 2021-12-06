using Microsoft.EntityFrameworkCore;
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
    public class InventoryService : IInventoryService
    {
        private readonly IHRMSIMSAssetsCategoryRepository _hrmsassetscategoryRepository;
        private readonly IHRMSIMSAssetsRepository _hrmsassetsRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;


        public InventoryService(IConfiguration config, IHRMSIMSAssetsRepository hrmsassetsRepository,IHRMSIMSAssetsCategoryRepository hrmsassetscategoryRepository, IUnitOfWork uow)

       

        {
            _hrmsassetscategoryRepository = hrmsassetscategoryRepository;
            _config = config;
            _hrmsassetsRepository = hrmsassetsRepository;
            _uow = uow;
        }

        public BaseResponse CreateAssests(ImsTblAssetsCategory assests)
        {
            BaseResponse responce = new BaseResponse();
          

            if (!string.IsNullOrEmpty(assests.ItacCategory))
            {
                assests.ItacCreatedBy = "admin";
                assests.ItacCreatedByDate= DateTime.Now;
                assests.ItacCreatedByName = "admin";
                assests.ItacIsDelete = false;
               

                if (assests.ImsTblAssests.Count > 0)
                {
                    foreach (var item in assests.ImsTblAssests)
                    {
                        item.ItaCreatedBy = "Admin";
                        item.ItaCreatedByDate = DateTime.Now;
                        item.ItaCreatedByName = "Admin";
                        item.ItaIsDelete = false;

                    }
                }
                _hrmsassetscategoryRepository.Insert(assests);
                responce.Success = true;
                responce.Message = UserMessages.strSuccess;
            }


            else
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = UserMessages.strAdded;
            }

            return responce;


        }

        public BaseResponse DeleteAssests(int id)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsassetsRepository.Table.Count(p => p.ItaAssetId == id) > 0;
            bool alreadyDelete = _hrmsassetsRepository.Table.Count(p => p.ItaIsDelete == true && p.ItaAssetId == id) > 0;
            _hrmsassetsRepository.Table.Where(p => p.ItaAssetId == id).ToList().ForEach(x =>
            {
                x.ItaIsDelete = true;
                x.ItaModifiedByDate = DateTime.Now;
                x.ItaModifiedBy = "admin";
                x.ItaModifiedByName = "admin";
                
                
            });

            if(doesExistAlready==true && alreadyDelete == false)
            {
                response.Success = true;
                response.Message = UserMessages.strDeleted;
            }
            
            else if(doesExistAlready==true && alreadyDelete==true)
            {
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;

            }
            
            else if(doesExistAlready==false)
            {
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }
          
          
            _uow.Commit();
            return response;
        }

        public BaseResponse GetAllAssets()
        {

            BaseResponse response = new BaseResponse();

           
            bool count = _hrmsassetsRepository.Table.Count() > 0;     
            var inventoryData = _hrmsassetsRepository.Table.Where(z => z.ItaIsDelete == false).OrderByDescending(x=>x.ItaAssetId).ToList().Take(10);


            if (count == true)
            {
                response.Data = inventoryData;
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

        public BaseResponse UpdateAssests(ImsTblAssetsCategory assests)
        {
            BaseResponse response = new BaseResponse();

            assests.ItacModifiedBy = "admin";
            assests.ItacModifiedByDate = DateTime.Now;
            assests.ItacModifiedByName = "admin";
            assests.ItacIsDelete = false;

            // Update AcademicQualification
            if (assests.ImsTblAssests.Count > 0)
            {
                foreach (var item in assests.ImsTblAssests)
                {
                    item.ItaModifiedBy = "admin";
                    item.ItaModifiedByDate = DateTime.Now;
                    item.ItaModifiedByName = "admin";
                    item.ItaIsDelete = false;
                    response.Success = true;
                    response.Message = UserMessages.strUpdated;
                }

                
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;

            }
            _hrmsassetscategoryRepository.Update(assests);
            _uow.Commit();
            return response;
        }

        public BaseResponse EditAssetById(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsassetsRepository.Table.Where(z => z.ItaIsDelete == false && z.ItaAssetId == id).Count() > 0;
            var assetsData = _hrmsassetsRepository.Table.Include(x => x.ItaCategory).Where(x => x.ItaAssetId == id).ToList();

            if (count == true)
            {
                response.Data = assetsData;
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
