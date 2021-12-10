﻿
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Web.API.Helper;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetController(IAssetService assetservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetservice = assetservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger (_hostEnvironment);
        }

        //Assest Laptop CRUD 
        [HttpPost("/Assest/Laptop/AddLaptop")]
        public BaseResponse CreateLaptop([FromBody] AssetLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetservice.CreateAssetFurniture(furniture);



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
        
        [HttpPost("/Assest/Laptop/UpdateLaptop")]
        public BaseResponse UpdateLaptop([FromBody] AssetLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetservice.UpdateAssestLaptop(laptop);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpPost("/Assest/Ac/AddAc")]
        public BaseResponse CreateAssetAc([FromBody] AssetAcCredential Ac)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetservice.CreateAssetAc(Ac);



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
