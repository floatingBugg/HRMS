
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

        [HttpPost("/Assests/Add")]
        public BaseResponse Create([FromBody] ImsTblAssetsCategory assests)
        {

            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _inventoryservice.CreateAssests(assests);

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
        [HttpGet("/Assets/Display")]
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

            [HttpDelete("/Assests/Remove")]

            public BaseResponse Delete(int id)
            {
                BaseResponse responce = new BaseResponse();
                try
                {
                    responce = _inventoryservice.DeleteAssests(id);
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


        

       

        
    

