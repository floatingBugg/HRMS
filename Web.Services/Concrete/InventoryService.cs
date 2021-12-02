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
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class InventoryService : IInventoryService
    {
        private readonly IHRMSIMSAssetsCategoryRepository _hrmsassetscategoryRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public InventoryService(IConfiguration config, IHRMSIMSAssetsCategoryRepository hrmsassetscategoryRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetscategoryRepository = hrmsassetscategoryRepository;
            _uow = uow;
        }

        public BaseResponse CreateAssets(ImsTblAssetsCategory assests)
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
                responce.Message = "Added";


            }

            else
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = "failed";
            }

            return responce;


        }

        public BaseResponse DeleteEmployee()
        {
            throw new NotImplementedException();
        }

        public BaseResponse GetAllEmployee()
        {
            throw new NotImplementedException();
        }

        public BaseResponse UpdateEmployee(ImsTblAssests assests)
        {
            throw new NotImplementedException();
        }
    }
}
