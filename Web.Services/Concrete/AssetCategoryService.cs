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
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class AssetCategoryService : IAssetCategoryService
    {
        private readonly IHRMSAssetCategoryRepository _hrmsassetcategoryRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetCategoryService(IConfiguration config, IHRMSAssetCategoryRepository hrmsassetcategoryRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetcategoryRepository = hrmsassetcategoryRepository;
            _uow = uow;
        }

        public BaseResponse CreateAssetCategory(ImsAssetsCategoryVM asset)
        {
            BaseResponse response = new BaseResponse();
            ImsAssetsCategory imsAssetCatergory = new ImsAssetsCategory();
            if (!string.IsNullOrEmpty(asset.ItacCategoryName))

            {
                imsAssetCatergory.ItacCategoryName = asset.ItacCategoryName;
                imsAssetCatergory.ItacCreatedBy = asset.ItacCreatedBy;
                imsAssetCatergory.ItacCreatedByName = asset.ItacCreatedByName;
                imsAssetCatergory.ItacCreatedByDate = asset.ItacCreatedByDate;
                imsAssetCatergory.ItacIsDelete = false;
                _uow.Commit();
                _hrmsassetcategoryRepository.Insert(imsAssetCatergory);
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
            return response;

        }



        public BaseResponse UpdateAssetCategory(ImsAssetsCategoryVM asset)
        {
            BaseResponse response = new BaseResponse();
            ImsAssetsCategory imsAssetsCategory = new ImsAssetsCategory();
            bool count = _hrmsassetcategoryRepository.Table.Where(p => p.ItacCategoryId == asset.ItacCategoryId).Count() > 0;
            if (count == true)
            {
                        imsAssetsCategory.ItacCategoryId = asset.ItacCategoryId;  
                        imsAssetsCategory.ItacCategoryName = asset.ItacCategoryName;
                        imsAssetsCategory.ItacCreatedBy = asset.ItacCreatedBy;
                        imsAssetsCategory.ItacCreatedByName = asset.ItacCreatedByName;
                        imsAssetsCategory.ItacCreatedByDate = asset.ItacCreatedByDate;
                        imsAssetsCategory.ItacIsDelete = false;

                _hrmsassetcategoryRepository.Update(imsAssetsCategory);
                _uow.Commit();
                response.Success = true;
                response.Message = UserMessages.strUpdated;
                response.Data = null;
            }

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;
            }

            return response;
        }

        public BaseResponse DeleteAssetCategory(int id)
        {
            BaseResponse responce = new BaseResponse();
            bool doesExistAlready = _hrmsassetcategoryRepository.Table.Count(p => p.ItacCategoryId == id) > 0;
            bool isDeletedAlready = _hrmsassetcategoryRepository.Table.Count(p => p.ItacCategoryId == id && p.ItacIsDelete == true) > 0;

            _hrmsassetcategoryRepository.Table.Where(p => p.ItacCategoryId == id).ToList().ForEach(x =>
            {

                x.ItacIsDelete = true;

            });
            _uow.Commit();
            if (doesExistAlready==true && isDeletedAlready==false)
            {
                responce.Success = true;
                responce.Data = id;
                responce.Message = UserMessages.strDeleted;
            }
            else if(doesExistAlready==true && isDeletedAlready==true)
            {
                responce.Success = false;
                responce.Message = UserMessages.strAlrdeleted;
                responce.Data = null;
            }

            else if (doesExistAlready==false)
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = UserMessages.strNotfound;
            }
            return responce;


        }

        public BaseResponse DisplayAssetCategory()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetcategoryRepository.Table.Count() > 0;
            var categorydata = _hrmsassetcategoryRepository.Table.Where(z => z.ItacIsDelete == false).Select(x => new AssetCategoryCredential()
            {
                categoryId = x.ItacCategoryId,
                categoryname = x.ItacCategoryName


            }).ToList().OrderByDescending(x => x.categoryId);

            if (count == true)
            {
                response.Data = categorydata;
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
    }
}
