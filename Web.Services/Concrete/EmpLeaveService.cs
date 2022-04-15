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
    public class EmpLeaveService : IEmpLeaveService
    {
        private readonly IHRMSEmployeeLeaveRepository _hrmsemployeeleaverepository;
        private readonly IHRMSLeaveRecordRepository _hrmsleaverecordrepository;
        private readonly IHRMSLeaveTypeRepository _hrmsleavetyperepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeerepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmpLeaveService(IConfiguration config, IHRMSEmployeeLeaveRepository hrmsEmployeeLeaveRepository, IHRMSLeaveRecordRepository hrmsLeaverecordRepository, IHRMSLeaveTypeRepository hrmsLeaveTypeRepository, IHRMSEmployeeRepository hrmsEmployeeRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsemployeeleaverepository = hrmsEmployeeLeaveRepository;
            _hrmsleaverecordrepository = hrmsLeaverecordRepository;
            _hrmsleavetyperepository = hrmsLeaveTypeRepository;
            _hrmsemployeerepository = hrmsEmployeeRepository;

            _uow = uow;
        }

        public BaseResponse createLeave(LmsEmployeeLeaveVM leave)
            {
            BaseResponse response = new BaseResponse();
            leave.LmselStartDate = leave.LmselStartDateStr != null && leave.LmselStartDateStr != "" ? DateTime.Parse(leave.LmselStartDateStr) : leave.LmselStartDate;
            var totalTaken = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrTotalTaken).FirstOrDefault();
            var sickTaken = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrSickTaken).FirstOrDefault();
            var casualTaken = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrCasualTaken).FirstOrDefault();
            var annualTaken = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrAnnualTaken).FirstOrDefault();
            totalTaken = totalTaken + leave.LmselDays;
            var totalassign = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrTotalAssign).FirstOrDefault();
            var sickassign = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrSickAssign).FirstOrDefault();
            var annualassign = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrAnnualAssign).FirstOrDefault();
            var casualassign = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrCasualAssign).FirstOrDefault();

            if (leave.LmselLeaveType==2 && sickTaken <= sickassign) 
            {
                sickTaken = sickTaken + leave.LmselDays;
            }
            else if (leave.LmselLeaveType == 3 && casualTaken <= casualassign)
            {
                casualTaken = casualTaken + leave.LmselDays;
            }
            else if (leave.LmselLeaveType == 1 && annualTaken <= annualassign)
            {
                annualTaken = annualTaken + leave.LmselDays;
                
            }

            else
            {
                response.Message = UserMessages.strNoLeave;
                response.Data = null;
            }
            
            if (leave.LmselLeaveType != null && totalTaken<= totalassign && sickTaken <= sickassign && casualTaken<=casualassign&& annualTaken<=annualassign)
            {
                LmsEmployeeLeave lmsEmployeeLeave = new LmsEmployeeLeave();

                lmsEmployeeLeave.LmselEtedEmployeeId = leave.LmselEtedEmployeeId;
                lmsEmployeeLeave.LmselLeaveType = leave.LmselLeaveType;
                lmsEmployeeLeave.LmselStartDate = leave.LmselStartDate;
                lmsEmployeeLeave.LmselEndDate = leave.LmselEndDate;
                lmsEmployeeLeave.LmselDays = leave.LmselDays;
                lmsEmployeeLeave.LmselCreatedBy = leave.LmselCreatedBy;
                lmsEmployeeLeave.LmselCreatedByName = leave.LmselCreatedByName;
                lmsEmployeeLeave.LmselCreatedByDate = leave.LmselCreatedByDate;
                lmsEmployeeLeave.LmselIsDelete = leave.LmselIsDelete;
                lmsEmployeeLeave.LmselReason = leave.LmselReason;
                

                _hrmsleaverecordrepository.Table.Where(p => p.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId)
                    .ToList()
                    .ForEach(x => {
                        x.LmslrTotalTaken = totalTaken;
                        x.LmslrSickTaken = sickTaken;
                        x.LmslrCasualTaken = casualTaken;
                        x.LmslrAnnualTaken = annualTaken;
                    });

                _hrmsemployeeleaverepository.Insert(lmsEmployeeLeave);
                _uow.Commit();

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

        public BaseResponse updateLeave(LmsEmployeeLeaveVM leave)
        {

            BaseResponse response = new BaseResponse();
            LmsEmployeeLeave lmsEmployeeLeave = new LmsEmployeeLeave();
            bool count = _hrmsemployeeleaverepository.Table.Where(p => p.LmselLeaveId == leave.LmselLeaveId).Count() > 0;

            if (count == true)
            {
                if ((leave.LmselDays!=null))
                {

                    lmsEmployeeLeave.LmselLeaveId = leave.LmselLeaveId;
                    lmsEmployeeLeave.LmselEtedEmployeeId = leave.LmselEtedEmployeeId;
                    lmsEmployeeLeave.LmselLeaveType = leave.LmselLeaveType;
                    lmsEmployeeLeave.LmselStartDate = leave.LmselStartDate;
                    lmsEmployeeLeave.LmselEndDate = leave.LmselEndDate;
                    lmsEmployeeLeave.LmselDays = leave.LmselDays;
                    lmsEmployeeLeave.LmselIsDelete =leave.LmselIsDelete;
                    //lmsEmployeeLeave.LmselReason = leave.LmselReason;

                }

                _hrmsemployeeleaverepository.Update(lmsEmployeeLeave);
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
        public BaseResponse ViewLeaveRecordByempid(int empid)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsemployeeleaverepository.Table.Where(z => z.LmselEtedEmployeeId == empid && z.LmselIsDelete != true).Count() > 0;
            var leavedata = _hrmsemployeeleaverepository.Table.Where(x => x.LmselEtedEmployeeId == empid && x.LmselIsDelete != true).Select(x => new LmsEmployeeLeaveVM
            {
                LmselLeaveId = x.LmselLeaveId,
                LmselEtedEmployeeName = _hrmsemployeerepository.Table.Where(z => z.EtedEmployeeId == x.LmselEtedEmployeeId).Select(z => z.EtedFirstName + " " + z.EtedLastName).FirstOrDefault(),
                LmselLeaveType = x.LmselLeaveType,
                LmselStartDate = x.LmselStartDate,
                LmselDays = x.LmselDays,
                LmselCreatedBy = x.LmselCreatedBy,
                LmselCreatedByName = x.LmselCreatedByName,
                LmselCreatedByDate = x.LmselCreatedByDate,
                
            }).ToList().OrderByDescending(x => x.LmselLeaveId);

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

        public BaseResponse EmployeeData(int roleid, int empid)
        {
            

            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeeleaverepository.Table.Count() > 0;

            var employeeData = _hrmsemployeeleaverepository.Table.Where(z => z.LmselIsDelete == false && z.LmselEtedEmployeeId == empid).ToList().OrderByDescending(x => x.LmselEtedEmployeeId);



            var managerData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedIsManager == true && z.EtedEmployeeId==empid).ToList().OrderByDescending(x => x.EtedEmployeeId);

            if (roleid == 1 || roleid == 2)
            {
                var employeesData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId != empid && z.EtedStatus=="Active").Select(x => new DisplayEmployeeGrid()
                {
                    empID = x.EtedEmployeeId,
                    fullName = x.EtedFirstName,
                    empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                    empStatus = x.EtedStatus,
               
                }).ToList().OrderByDescending(x => x.empID);
                response.Data = employeesData;

            }

            else if (roleid == 3)
            {
                var employeesData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedManagerId == empid).Select(x => new DisplayEmployeeGrid()
                {
                    empID = x.EtedEmployeeId,
                    fullName = x.EtedFirstName,
                    empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                    empStatus = x.EtedStatus,
                }).ToList().OrderByDescending(x => x.empID);
                response.Data = employeesData;
            }

            if (count == true)
            {
                response.Data3 = managerData;
                response.Data2 = employeeData;

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
