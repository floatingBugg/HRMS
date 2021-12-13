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
    public class AssetService : IAssetService
    {
        private readonly IHRMSAssetCategoryRepository _hrmsassetcategoryRepository;
        private readonly IHRMSAssetRepository _hrmsassetRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;
        public AssetService(IConfiguration config, IUnitOfWork uow, IHRMSAssetCategoryRepository hrmsassetcategoryRepository, IHRMSAssetRepository hrmsassetrepository)
        {
            _config = config;
            _uow = uow;
            _hrmsassetcategoryRepository = hrmsassetcategoryRepository;
            _hrmsassetRepository = hrmsassetrepository;
        }

        public BaseResponse CreateAsset(AssetCredential assets)
        {
            BaseResponse response = new BaseResponse();
            List<ImsTblAssets> asset = new List<ImsTblAssets>();

            if (!string.IsNullOrEmpty(assets.assetname))
            {
                asset.Add(new ImsTblAssets
                {
                    ItaAssetName = assets.assetname,
                    ItaQuantity = assets.quantity,
                    ItaCost = assets.cost,
                    ItacCategoryIdFk = assets.categoryid,
                    ItaDescription = assets.description,
                    ItaSerialNo=assets.serialno,
                    ItaModel=assets.model,
                    ItaCompanyName=assets.companyname,
                    ItaType=assets.type,
                    ItaAssignedToId=assets.assignid,
                    ItaAssignedToName = assets.assingedname,
                    ItaPurchaseDate = assets.purchaseddate.Date,
                    ItaCreatedBy = assets.createdby,
                    ItaCreatedByName = assets.createdbyname,
                    ItaCreatedByDate = DateTime.Now,
                    ItaIsDelete = false

                });
                _hrmsassetRepository.Insert(asset);


                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
            return response;
        }

        public BaseResponse CreateAssetCategory(AssetCategoryCredential category)
        {
            BaseResponse response = new BaseResponse();
            List<ImsTblAssetCategory> assetcategory = new List<ImsTblAssetCategory>();

            if (!string.IsNullOrEmpty(category.categoryname))
            {
                assetcategory.Add(new ImsTblAssetCategory
                {
                    ItacCategoryName = category.categoryname,
                    ItacCreatedBy = "Admin",
                    ItacCreatedByName = "Admin",
                    ItacCreatedByDate = DateTime.Now.Date,
                    ItacIsDelete = false

                });
                _hrmsassetcategoryRepository.Insert(assetcategory);



                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
            return response;
        }
    }
}
