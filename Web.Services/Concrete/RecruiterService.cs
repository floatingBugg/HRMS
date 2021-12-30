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
    public class RecruiterService : IRecruiterService
    {
        private readonly IHRMSRecruiterRepository _hrmsrecruiterRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public RecruiterService(IConfiguration config, IHRMSRecruiterRepository hrmsrecruiterRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsrecruiterRepository = hrmsrecruiterRepository;
            _uow = uow;
        }

        public BaseResponse CreateRecruit(RmsTblRecruiterVM recruit)
        {
            BaseResponse response = new BaseResponse();
            RmsTblRecruiter rmsRecruiter = new RmsTblRecruiter();
            if (!string.IsNullOrEmpty(recruit.RtrEmail))

            {
                rmsRecruiter.RtrRecId = recruit.RtrRecId;
                rmsRecruiter.RtrEmail = recruit.RtrEmail;
                rmsRecruiter.RtrPhoneNo = recruit.RtrPhoneNo;
                rmsRecruiter.RtrLastDegree = recruit.RtrLastDegree;
                rmsRecruiter.RtrLastCompany = recruit.RtrLastCompany;
                rmsRecruiter.RtrExperience = recruit.RtrExperience;
                rmsRecruiter.RtrCurrentSalary = recruit.RtrCurrentSalary;
                rmsRecruiter.RtrExpectedSalary = recruit.RtrExpectedSalary;
                rmsRecruiter.RtrResume = recruit.RtrResume;
                rmsRecruiter.RtrRecruitStatus = recruit.RtrRecruitStatus;
                rmsRecruiter.RtrInterviewDateTime = recruit.RtrInterviewDateTime;
                rmsRecruiter.RtrInterviewStatus = recruit.RtrInterviewStatus;
                rmsRecruiter.RtrRecruitStatusRemarks = recruit.RtrRecruitStatusRemarks;
                rmsRecruiter.RtrShortList = recruit.RtrShortList;
                rmsRecruiter.RtrExpectedDate = recruit.RtrExpectedDate;
                rmsRecruiter.RtrSalaryNegotiation = recruit.RtrSalaryNegotiation;
                rmsRecruiter.RtrRecommendedBy = recruit.RtrRecommendedBy;
                rmsRecruiter.RtrRecommendedPersonRemarks = recruit.RtrRecommendedPersonRemarks;
                rmsRecruiter.RtrHiringStatus = recruit.RtrHiringStatus;
                rmsRecruiter.RtrCreatedBy = recruit.RtrCreatedBy;
                rmsRecruiter.RtrCreatedByName = recruit.RtrCreatedByName;
                rmsRecruiter.RtrCreatedByDate = recruit.RtrCreatedByDate;
                rmsRecruiter.RtrModifiedBy = recruit.RtrModifiedBy;
                rmsRecruiter.RtrModifiedByName = recruit.RtrModifiedByName;
                rmsRecruiter.RtrModifiedByDate = recruit.RtrModifiedByDate;
                rmsRecruiter.RtrIsDelete = false;
                _uow.Commit();
               _hrmsrecruiterRepository.Insert(rmsRecruiter);
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

        public BaseResponse UpdateRecruit(RmsTblRecruiterVM recruit)
        {
            BaseResponse response = new BaseResponse();
            RmsTblRecruiter rmsRecruiter = new RmsTblRecruiter();
            bool count = _hrmsrecruiterRepository.Table.Where(p => p.RtrRecId == recruit.RtrRecId).Count() > 0;
            if (count == true)
            {
                rmsRecruiter.RtrRecId = recruit.RtrRecId;
                rmsRecruiter.RtrEmail = recruit.RtrEmail;
                rmsRecruiter.RtrPhoneNo = recruit.RtrPhoneNo;
                rmsRecruiter.RtrLastDegree = recruit.RtrLastDegree;
                rmsRecruiter.RtrLastCompany = recruit.RtrLastCompany;
                rmsRecruiter.RtrExperience = recruit.RtrExperience;
                rmsRecruiter.RtrCurrentSalary = recruit.RtrCurrentSalary;
                rmsRecruiter.RtrExpectedSalary = recruit.RtrExpectedSalary;
                rmsRecruiter.RtrResume = recruit.RtrResume;
                rmsRecruiter.RtrRecruitStatus = recruit.RtrRecruitStatus;
                rmsRecruiter.RtrInterviewDateTime = recruit.RtrInterviewDateTime;
                rmsRecruiter.RtrInterviewStatus = recruit.RtrInterviewStatus;
                rmsRecruiter.RtrRecruitStatusRemarks = recruit.RtrRecruitStatusRemarks;
                rmsRecruiter.RtrShortList = recruit.RtrShortList;
                rmsRecruiter.RtrExpectedDate = recruit.RtrExpectedDate;
                rmsRecruiter.RtrSalaryNegotiation = recruit.RtrSalaryNegotiation;
                rmsRecruiter.RtrRecommendedBy = recruit.RtrRecommendedBy;
                rmsRecruiter.RtrRecommendedPersonRemarks = recruit.RtrRecommendedPersonRemarks;
                rmsRecruiter.RtrHiringStatus = recruit.RtrHiringStatus;
                rmsRecruiter.RtrCreatedBy = recruit.RtrCreatedBy;
                rmsRecruiter.RtrCreatedByName = recruit.RtrCreatedByName;
                rmsRecruiter.RtrCreatedByDate = recruit.RtrCreatedByDate;
                rmsRecruiter.RtrModifiedBy = recruit.RtrModifiedBy;
                rmsRecruiter.RtrModifiedByName = recruit.RtrModifiedByName;
                rmsRecruiter.RtrModifiedByDate = recruit.RtrModifiedByDate;
                rmsRecruiter.RtrIsDelete = false;

                _hrmsrecruiterRepository.Update(rmsRecruiter);
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

        public BaseResponse DisplayRecruit()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsrecruiterRepository.Table.Count() > 0;
            var recruitdata = _hrmsrecruiterRepository.Table.Where(z => z.RtrIsDelete == false).Select(x => new RmsTblRecruiterVM()
            {
                RtrRecId = x.RtrRecId,
                RtrEmail = x.RtrEmail,
                RtrPhoneNo = x.RtrPhoneNo,
                RtrLastDegree = x.RtrLastDegree,
                RtrLastCompany = x.RtrLastCompany,
                RtrExperience = x.RtrExperience,
                RtrCurrentSalary = x.RtrCurrentSalary,
                RtrExpectedSalary = x.RtrExpectedSalary,
                RtrResume = x.RtrResume,
                RtrRecruitStatus = x.RtrRecruitStatus,
                RtrInterviewDateTime = x.RtrInterviewDateTime,
                RtrInterviewStatus = x.RtrInterviewStatus,
                RtrRecruitStatusRemarks = x.RtrRecruitStatusRemarks,
                RtrShortList = x.RtrShortList,
                RtrExpectedDate = x.RtrExpectedDate,
                RtrSalaryNegotiation = x.RtrSalaryNegotiation,
                RtrRecommendedBy = x.RtrRecommendedBy,
                RtrRecommendedPersonRemarks = x.RtrRecommendedPersonRemarks,
                RtrHiringStatus = x.RtrHiringStatus,
                RtrCreatedBy = x.RtrCreatedBy,
                RtrCreatedByName = x.RtrCreatedByName,
                RtrCreatedByDate = x.RtrCreatedByDate,
                RtrModifiedBy = x.RtrModifiedBy,
                RtrModifiedByName = x.RtrModifiedByName,
                RtrModifiedByDate = x.RtrModifiedByDate,
                RtrIsDelete = false

          }).ToList().OrderByDescending(x => x.RtrRecId);

            if (count == true)
            {
                response.Data = recruitdata;
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

        public BaseResponse DeleteRecruit(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse ViewDataRecruitByid(int id)
        {
            throw new NotImplementedException();
        }
    }
}
 
