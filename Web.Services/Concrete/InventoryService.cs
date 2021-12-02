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
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class InventoryService : IInventoryService
    {
        private readonly IHRMSIMSAssetsRepository _hrmsassetsRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public InventoryService(IConfiguration config, IHRMSIMSAssetsRepository hrmsassetsRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetsRepository = hrmsassetsRepository;
            _uow = uow;
        }

        public BaseResponse CreateAssets(ImsTblAssests assests)
        {
            BaseResponse responce = new BaseResponse();
            if (!string.IsNullOrEmpty(assests.ItaAssetsName) && (assests.ItaAssetsSrNo!=null) && !string.IsNullOrEmpty(assests.ItaAssetDetails))
            {
                assests.ItaCreatedBy = "admin";
                assests.ItaCreatedByDate = DateTime.Now;
                assests.ItaCreatedByName = "admin";
                assests.ItaIsDelete = false;
                _hrmsassetsRepository.Insert(assests);
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

        public BaseResponse DeleteEmployee(int id)
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
