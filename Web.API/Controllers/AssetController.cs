﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Helper;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetLaptopservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetController(IAssetService assetLaptopservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetLaptopservice = assetLaptopservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
        [HttpPost("/Asset/AddAsset")]
        public BaseResponse CreateAsset([FromBody] AssetCredential asset)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetLaptopservice.createAsset(asset);



                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpPost("/Asset/UpdateAsset")]
        public BaseResponse UpdateAsset([FromBody] AssetCredential asset)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetLaptopservice.updateAsset(asset);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpDelete("/Asset/DeleteAsset")]
        public BaseResponse DeleteLaptop(int assetid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.deleteAsset(assetid);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpGet("Asset/GetAllSum")]
        public BaseResponse getTotalCostLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.sumOfAsset(categoryid);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpGet("Asset/GetAllQuantity")]
        public BaseResponse getTotalQuantityLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.totalQuantityAsset(categoryid);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpGet("/Asset/DisplayAllLaptop")]
        public BaseResponse GetAllEmployee(int type)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response =_assetLaptopservice.displayAllAssetUnAssigned(type);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}