
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Helper;
using Web.Data.Models;
using Web.Model;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public InventoryController(IInventoryService inventoryservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _inventoryservice = inventoryservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
        //create
        [HttpPost("/Assests/AddAssests")]
        public BaseResponse Create([FromBody] ImsTblAssetsCategory assests,string userName=null, string userId=null)
        {

            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _inventoryservice.CreateAssests(assests,userName,userId);

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

        // GET: Inventory/Details/5
        [HttpGet("/Assets/DisplayAssests")]
        public BaseResponse GetAllAssets()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _inventoryservice.GetAllAssets();
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

        [HttpGet("/Assets/GetAssestById")]
        public BaseResponse ViewAssetsById(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _inventoryservice.ViewAssetById(id);
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

      

        [HttpPost("/Assests/UpdateAssests")]
        public BaseResponse Update([FromBody] ImsTblAssetsCategory assests,string userName,string userId)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _inventoryservice.UpdateAssests(assests,userName,userId);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpDelete("/Assests/DeleteAssests")]

        public BaseResponse Delete(int Delid, string userName = null, string userId = null)
        {
            BaseResponse responce = new BaseResponse();
            try
            {
                responce = _inventoryservice.DeleteAssests(Delid, userName, userId);
                return responce;
            }

            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                responce.Data = null;
                responce.Message = ex.Message;
                responce.Success = false;
                return responce;
            }


        }
    }
    }


        

       

        
    

