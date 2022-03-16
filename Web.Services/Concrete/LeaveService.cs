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
    public class LeaveService : ILeaveService
    {
        private readonly IHRMSLeaveRecordRepository _hrmsleaverecordrepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;

        private readonly IHRMSAcademicRepository _hrmsacademicrepository;
        private readonly IHRMSPRofessionalRepository _hrmsprofessionalrepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        private readonly IHRMSEmployeeWorkingHistoryRepository _workinghistoryRepository;
        private readonly IHRMSProfessionalDetailsRepository _hrmsprofessionaldetailsrepository;
        private readonly IHRMSUserAuthRepository _hrmsUserAuthRepository;
        private readonly IHRMSDropdownValueRepository _hrmsdropdownvaluerepository;
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSAssetAssignRepository _hrmsassetAssignRepository;
        private readonly IHRMSEmployementStatusRepository _hrmsstatusRepository;
        private readonly IHRMSEmployeeLeaveRepository _hrmsemployeeLeaveRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public LeaveService(IConfiguration config, IHRMSLeaveRecordRepository hrmsleaverecordrepository,IHRMSEmployeeRepository hrmsemployeeRepository,
            IHRMSEmployeeLeaveRepository hrmsemployeeLeaveRepository, IHRMSEmployementStatusRepository hrmsstatusRepository, IHRMSUserAuthRepository hrmsUserAuthRepository, IHRMSAssetRepository hrmsassetRepository, IHRMSAcademicRepository hRMSAcademicRepository, IHRMSEmployeeContactRepository employeeContactRepository, IHRMSPRofessionalRepository hRMSProfessionalRepository, IHRMSEmployeeWorkingHistoryRepository workingHistoryRepository, IHRMSProfessionalDetailsRepository hRMSProfessionalDetailsRepository, IHRMSDropdownValueRepository hrmsdropdownvaluerepository, IUnitOfWork uow, IHRMSAssetAssignRepository hrmsassetAssignRepository)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetAssignRepository = hrmsassetAssignRepository;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _employeeContactRepository = employeeContactRepository;
            _hrmsacademicrepository = hRMSAcademicRepository;
            _hrmsprofessionalrepository = hRMSProfessionalRepository;
            _workinghistoryRepository = workingHistoryRepository;
            _hrmsprofessionaldetailsrepository = hRMSProfessionalDetailsRepository;
            _hrmsUserAuthRepository = hrmsUserAuthRepository;
            _hrmsdropdownvaluerepository = hrmsdropdownvaluerepository;
            _hrmsstatusRepository = hrmsstatusRepository;
            _hrmsleaverecordrepository = hrmsleaverecordrepository;
            _hrmsemployeeLeaveRepository = hrmsemployeeLeaveRepository;
            _uow = uow;
        }
        public BaseResponse CreateLeave(LmsLeaveRecordVM leave)
        {
            LmsLeaveRecord empleave = new LmsLeaveRecord();
            BaseResponse response = new BaseResponse();
            if(leave.LmslrCasualAssign!=0 && leave.LmslrAnnualAssign!=0)
            { 
            empleave.LmslrEtedEmployeeId = leave.LmslrEtedEmployeeId;
            empleave.LmslrSickAssign = leave.LmslrSickAssign;
            empleave.LmslrCasualAssign = leave.LmslrCasualAssign;
            empleave.LmslrAnnualAssign = leave.LmslrAnnualAssign;
            empleave.LmslrTotalAssign = leave.LmslrAnnualAssign + leave.LmslrCasualAssign + leave.LmslrSickAssign;
            empleave.LmslrSickTaken= leave.LmslrSickAssign;
            empleave.LmslrAnnualTaken = leave.LmslrAnnualAssign;
            empleave.LmslrCasualTaken = leave.LmslrCasualAssign;
            empleave.LmslrTotalTaken = empleave.LmslrCasualTaken + empleave.LmslrAnnualTaken + empleave.LmslrSickTaken;
            empleave.LmslrCreatedBy = leave.LmslrCreatedBy;
            empleave.LmslrCreatedByName = leave.LmslrCreatedByName;
            empleave.LmslrCreatedByDate = leave.LmslrCreatedByDate;
            empleave.LmslrIsDelete = false;

            _hrmsleaverecordrepository.Insert(empleave);

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

        public BaseResponse UpdateLeave(LmsLeaveRecordVM leave)
        {
            LmsLeaveRecord empleave = new LmsLeaveRecord();
            BaseResponse response = new BaseResponse();
            if (leave.LmslrCasualAssign != 0 && leave.LmslrAnnualAssign != 0)
            {
                empleave.LmslrRecordId = leave.LmslrRecordId;
                empleave.LmslrEtedEmployeeId = leave.LmslrEtedEmployeeId;
                empleave.LmslrSickAssign = leave.LmslrSickAssign;
                empleave.LmslrCasualAssign = leave.LmslrCasualAssign;
                empleave.LmslrAnnualAssign = leave.LmslrAnnualAssign;
                empleave.LmslrTotalAssign = leave.LmslrAnnualAssign + leave.LmslrCasualAssign + leave.LmslrSickAssign;
                empleave.LmslrSickTaken = leave.LmslrSickAssign;
                empleave.LmslrAnnualTaken = leave.LmslrAnnualAssign;
                empleave.LmslrCasualTaken = leave.LmslrCasualAssign;
                empleave.LmslrTotalTaken = empleave.LmslrCasualTaken + empleave.LmslrAnnualTaken + empleave.LmslrSickTaken;
                empleave.LmslrCreatedBy = leave.LmslrCreatedBy;
                empleave.LmslrCreatedByName = leave.LmslrCreatedByName;
                empleave.LmslrCreatedByDate = leave.LmslrCreatedByDate;
                empleave.LmslrIsDelete = false;

                _hrmsleaverecordrepository.Update(empleave);

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

        public BaseResponse ViewLeaveByempid(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsleaverecordrepository.Table.Where(z => z.LmslrEtedEmployeeId == id && z.LmslrIsDelete==false).Count() > 0;
            var leavedata = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == id && x.LmslrIsDelete == false).Select(x => new LmsLeaveRecordVM
            {
                LmslrRecordId=x.LmslrRecordId,
                LmslrEtedEmployeeName=_hrmsemployeeRepository.Table.Where(z=>z.EtedEmployeeId==x.LmslrEtedEmployeeId).Select(z=>z.EtedFirstName+" "+z.EtedLastName).FirstOrDefault(),
                LmslrCasualTaken=x.LmslrCasualTaken,
                LmslrSickTaken=x.LmslrSickTaken,
                LmslrAnnualTaken=x.LmslrAnnualTaken,
                LmslrTotalTaken=x.LmslrTotalTaken,
                LmslrCasualAssign=x.LmslrCasualAssign,
                LmslrSickAssign=x.LmslrSickAssign,
                LmslrAnnualAssign=x.LmslrAnnualAssign,
                LmslrTotalAssign=x.LmslrTotalAssign

            }).ToList().OrderByDescending(x => x.LmslrRecordId);

            if (count == true)
            {
                response.Data = leavedata;
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

        public BaseResponse ViewLeaveByrecordid(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsleaverecordrepository.Table.Where(z => z.LmslrRecordId == id && z.LmslrIsDelete == false).Count() > 0;
            var leavedata = _hrmsleaverecordrepository.Table.Where(x => x.LmslrRecordId == id && x.LmslrIsDelete == false).Select(x => new LmsLeaveRecordVM
            {
                LmslrEtedEmployeeId = x.LmslrEtedEmployeeId,
                LmslrRecordId = x.LmslrRecordId,
                LmslrEtedEmployeeName = _hrmsemployeeRepository.Table.Where(z => z.EtedEmployeeId == x.LmslrEtedEmployeeId).Select(z => z.EtedFirstName + " " + z.EtedLastName).FirstOrDefault(),
                LmslrCasualTaken = x.LmslrCasualTaken,
                LmslrSickTaken = x.LmslrSickTaken,
                LmslrAnnualTaken = x.LmslrAnnualTaken,
                LmslrTotalTaken = x.LmslrTotalTaken,
                LmslrCasualAssign = x.LmslrCasualAssign,
                LmslrSickAssign = x.LmslrSickAssign,
                LmslrAnnualAssign = x.LmslrAnnualAssign,
                LmslrTotalAssign = x.LmslrTotalAssign

            }).ToList().OrderByDescending(x => x.LmslrRecordId);

            if (count == true)
            { 
                response.Data = leavedata;
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

        public BaseResponse ViewLeaveEmployee()
        {
            BaseResponse response = new BaseResponse();


            //dddddddddddd

           var LmsLeaveRecords = (from lr in this._hrmsleaverecordrepository.Table
                              join s in  this._hrmsemployeeRepository.Table on lr.LmslrEtedEmployeeId equals s.EtedEmployeeId
                              where s.EtedStatus== "active" && lr.LmslrIsDelete == false
                                  select new LmsLeaveRecordVM { 
                LmslrEtedEmployeeId = lr.LmslrEtedEmployeeId,
                LmslrRecordId = lr.LmslrRecordId,
                LmslrEtedEmployeeName = s.EtedFirstName+s.EtedLastName,
                LmslrCasualTaken = lr.LmslrCasualTaken,
                EmpDesignation = _hrmsemployeeRepository.Table.Where(z => z.EtedEmployeeId == lr.LmslrEtedEmployeeId).Select(z => z.EmsTblEmployeeProfessionalDetails.Where(i => i.EtepdEtedEmployeeId == lr.LmslrEtedEmployeeId).Select(u => u.EtepdDesignation).FirstOrDefault()).FirstOrDefault(),
                LmslrSickTaken = lr.LmslrSickTaken,
                LmslrAnnualTaken = lr.LmslrAnnualTaken,
                LmslrTotalTaken = lr.LmslrTotalTaken,
                LmslrCasualAssign = lr.LmslrCasualAssign,
                LmslrSickAssign = lr.LmslrSickAssign,
                LmslrAnnualAssign = lr.LmslrAnnualAssign,
                LmslrTotalAssign = lr.LmslrTotalAssign

            }).ToList().OrderByDescending(x => x.LmslrRecordId);

            var count = LmsLeaveRecords.Count()>0;

            //dddddddddddd



            //bool count = _hrmsleaverecordrepository.Table.Where(z => z.LmslrIsDelete == false).Count() > 0;
            //var leavedata = _hrmsleaverecordrepository.Table.Where(x => x.LmslrIsDelete == false).Select(x => new LmsLeaveRecordVM
            //{
            //    LmslrEtedEmployeeId = x.LmslrEtedEmployeeId,
            //    LmslrRecordId = x.LmslrRecordId,
            //    LmslrEtedEmployeeName = _hrmsemployeeRepository.Table.Where(z => z.EtedEmployeeId == x.LmslrEtedEmployeeId).Select(z => z.EtedFirstName + " " + z.EtedLastName).FirstOrDefault(),
            //    LmslrCasualTaken = x.LmslrCasualTaken,
            //    EmpDesignation = _hrmsemployeeRepository.Table.Where(z => z.EtedEmployeeId == x.LmslrEtedEmployeeId).Select(z => z.EmsTblEmployeeProfessionalDetails.Where(i => i.EtepdEtedEmployeeId == x.LmslrEtedEmployeeId).Select(u => u.EtepdDesignation).FirstOrDefault()).FirstOrDefault(),
            //    LmslrSickTaken = x.LmslrSickTaken,
            //    LmslrAnnualTaken = x.LmslrAnnualTaken,
            //    LmslrTotalTaken = x.LmslrTotalTaken,
            //    LmslrCasualAssign = x.LmslrCasualAssign,
            //    LmslrSickAssign = x.LmslrSickAssign,
            //    LmslrAnnualAssign = x.LmslrAnnualAssign,
            //    LmslrTotalAssign = x.LmslrTotalAssign

            //}).ToList().OrderByDescending(x => x.LmslrRecordId);

            if (count == true)
            {
                response.Data = LmsLeaveRecords;
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
