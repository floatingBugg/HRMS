using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class AssetService : IAssetService
    {
        private readonly IHRMSIMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSIMSAssetLaptopRepository _hrmsassetlaptopRepository;
        IConfiguration _config;
        private readonly UnitOfWork unitorWork;
        public AssetService(IConfiguration config, IHRMSIMSAssetRepository hrmsassetRepository, IHRMSIMSAssetLaptopRepository hrmsassetlaptopRepository)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetlaptopRepository = hrmsassetlaptopRepository;
        }

        public string CreateAssestLaptop(ImsAssets laptop)
        {
            laptop.ItaCreatedBy = "1";
            laptop.ItaCreatedByDate= DateTime.Now;
            laptop.ItaCreatedByName= "Admin";
            laptop.ItaIsDelete= false;

            // Update AcademicQualification
            if (laptop.ImsLaptop.Count > 0)
            {
                foreach (var item in laptop.ImsLaptop)
                {
                    item.EtaqCreatedBy = userId;
                    item.EtaqCreatedByDate = DateTime.Now;
                    item.EtaqCreatedByName = userName;
                    item.EtaqIsDelete = false;

                }


                return null;
        }



    }
}
