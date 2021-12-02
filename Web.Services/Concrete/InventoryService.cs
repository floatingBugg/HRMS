﻿using Microsoft.Extensions.Configuration;
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

        public InventoryService(IConfiguration config, IHRMSIMSAssetsRepository hrmsassetsRepository, IUnitOfWork uow)
        {
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
            bool doesExistAlready = _hrmsassetscategoryRepository.Table.Count(p => p.ItacAcId == id) > 0;
            bool alreadyDelete = _hrmsassetscategoryRepository.Table.Count(p => p.ItacIsDelete == true && p.ItacAcId == id) > 0;
            _hrmsassetscategoryRepository.Table.Where(p => p.ItacAcId == id).ToList().ForEach(x =>
            {
                x.ItacIsDelete = true;
                x.ItacModifiedByDate = DateTime.Now;
                x.ItacModifiedBy = "admin";
                x.ItacModifiedByName = "admin";
               
                
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
            bool count = true; /*_hrmsassetsRepository.Table.Count() > 0;*/
            var inventoryData = _hrmsassetsRepository.Table.Where(z => z.ItaIsDelete == false).ToList();

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

        public BaseResponse UpdateEmployee(ImsTblAssests assests)
        {
            throw new NotImplementedException();
        }
    }
}
