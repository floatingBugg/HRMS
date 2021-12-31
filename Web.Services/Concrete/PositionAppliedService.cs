using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class PositionAppliedService : IPositionAppliedService
    {
        private readonly IHRMSPositionAppliedRepository _hrmspositionappliedRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public PositionAppliedService(IConfiguration config, IHRMSPositionAppliedRepository hrmspositionappliedRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmspositionappliedRepository = hrmspositionappliedRepository;
            _uow = uow;
        }

        public BaseResponse CreatePosition(RmsTblPositionAppliedVM position)
        {
            BaseResponse response = new BaseResponse();
            RmsTblPositionApplied rmsPosition = new RmsTblPositionApplied();
            if (!string.IsNullOrEmpty(position.RtpaPosition))

            {
                rmsPosition.RtpaPosId = position.RtpaPosId;
                rmsPosition.RtpaPosition = position.RtpaPosition;
                rmsPosition.RtpaCreatedBy = position.RtpaCreatedBy;
                rmsPosition.RtpaCreatedByName = position.RtpaCreatedByName;
                rmsPosition.RtpaCreatedByDate = position.RtpaCreatedByDate;
                rmsPosition.RtpaModifiedBy = position.RtpaModifiedBy;
                rmsPosition.RtpaModifiedByName = position.RtpaModifiedByName;
                rmsPosition.RtpaModifiedByDate = position.RtpaModifiedByDate;
                rmsPosition.RtpaIsDelete = position.RtpaIsDelete;

                _uow.Commit();
                _hrmspositionappliedRepository.Insert(rmsPosition);
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

        public BaseResponse UpdatePosition(RmsTblPositionAppliedVM position)
        {
            BaseResponse response = new BaseResponse();
            RmsTblPositionApplied rmsPosition = new RmsTblPositionApplied();
            bool count = _hrmspositionappliedRepository.Table.Where(p => p.RtpaPosId == position.RtpaPosId).Count() > 0;
            if (count == true)
            {
                rmsPosition.RtpaPosId = position.RtpaPosId;
                rmsPosition.RtpaPosition = position.RtpaPosition;
                rmsPosition.RtpaCreatedBy = position.RtpaCreatedBy;
                rmsPosition.RtpaCreatedByName = position.RtpaCreatedByName;
                rmsPosition.RtpaCreatedByDate = position.RtpaCreatedByDate;
                rmsPosition.RtpaModifiedBy = position.RtpaModifiedBy;
                rmsPosition.RtpaModifiedByName = position.RtpaModifiedByName;
                rmsPosition.RtpaModifiedByDate = position.RtpaModifiedByDate;
                rmsPosition.RtpaIsDelete = false;

                _hrmspositionappliedRepository.Update(rmsPosition);
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

        public BaseResponse DisplayPosition()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmspositionappliedRepository.Table.Count() > 0;
            var positiondata = _hrmspositionappliedRepository.Table.Where(z => z.RtpaIsDelete == false).Select(x => new RmsTblPositionAppliedVM()
            {
                RtpaPosId = x.RtpaPosId,
                RtpaPosition = x.RtpaPosition,
                RtpaCreatedBy = x.RtpaCreatedBy,
                RtpaCreatedByName = x.RtpaCreatedByName,
                RtpaCreatedByDate = x.RtpaCreatedByDate,
                RtpaModifiedBy = x.RtpaModifiedBy,
                RtpaModifiedByName = x.RtpaModifiedByName,
                RtpaModifiedByDate = x.RtpaModifiedByDate,
                RtpaIsDelete = false

            }).ToList().OrderByDescending(x => x.RtpaPosId);

            if (count == true)
            {
                response.Data = positiondata;
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

        public BaseResponse DeletePosition(int id)
        {
            BaseResponse responce = new BaseResponse();
            bool doesExistAlready = _hrmspositionappliedRepository.Table.Count(p => p.RtpaPosId == id) > 0;
            bool isDeletedAlready = _hrmspositionappliedRepository.Table.Count(p => p.RtpaPosId == id && p.RtpaIsDelete == true) > 0;

            _hrmspositionappliedRepository.Table.Where(p => p.RtpaPosId == id).ToList().ForEach(x =>
            {

                x.RtpaIsDelete = true;

            });
            _uow.Commit();
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                responce.Success = true;
                responce.Data = id;
                responce.Message = UserMessages.strDeleted;
            }
            else if (doesExistAlready == true && isDeletedAlready == true)
            {
                responce.Success = false;
                responce.Message = UserMessages.strAlrdeleted;
                responce.Data = null;
            }

            else if (doesExistAlready == false)
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = UserMessages.strNotfound;
            }
            return responce;
        }


    }
}
