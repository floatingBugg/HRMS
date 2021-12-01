using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Models;
using Web.Model;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class InventoryService : IInventoryService
    {
        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public InventoryService(IConfiguration config, , IUnitOfWork uow)
        {
            _config = config;
            
            _uow = uow;
        }

        public BaseResponse CreateAssets(ImsTblAssests assests)
        {
            throw new NotImplementedException();
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
