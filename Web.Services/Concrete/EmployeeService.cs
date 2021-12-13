using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _uow = uow;
        }


        public BaseResponse GetAllEmployee()
        {
            
            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeeRepository.Table.Count() > 0;
            var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not assigned"
            }).ToList().OrderByDescending(x=>x.empID);

            if (count == true)
            {
                response.Data = employeesData;
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

        public BaseResponse CreateEmployee(EmsTblEmployeeDetails employee)
        {
            
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmailAddress == employee.EtedEmailAddress) > 0;
            if (!string.IsNullOrEmpty(employee.EtedFirstName) && !string.IsNullOrEmpty(employee.EtedLastName)
               && !string.IsNullOrEmpty(employee.EtedEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender) 
               && !string.IsNullOrEmpty(employee.EtedReligion)
               && (employee.EtedCnic!=null) && doesExistAlready == false )
            {
                employee.EtedCreatedBy = employee.EtedFirstName;
                employee.EtedCreatedByDate = DateTime.Now;
                employee.EtedCreatedByName = employee.EtedLastName;
                employee.EtedIsDelete = false;

                // Update AcademicQualification
                if (employee.EmsTblAcademicQualification.Count > 0)
                {
                    foreach (var item in employee.EmsTblAcademicQualification)
                    {
                        item.EtaqCreatedBy = employee.EtedFirstName;
                        item.EtaqCreatedByDate = DateTime.Now;
                        item.EtaqCreatedByName = employee.EtedLastName;
                        item.EtaqIsDelete = false;

                    }
                }

                //Update ProfessionalQualification
                if (employee.EmsTblProfessionalQualification.Count > 0)
                {
                    foreach (var item in employee.EmsTblProfessionalQualification)
                    {
                        item.EtpqCreatedBy = employee.EtedFirstName;
                        item.EtpqCreatedByDate = DateTime.Now;
                        item.EtpqCreatedByName = employee.EtedLastName;
                        item.EtpqIsDelete = false;
                    }
                }

                //Update ProfessionalDetails
                if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    foreach (var item in employee.EmsTblEmployeeProfessionalDetails)
                    {
                        item.EtepdCreatedBy = employee.EtedFirstName;
                        item.EtepdCreatedByDate = DateTime.Now;
                        item.EtepdCreatedByName = employee.EtedLastName;
                        item.EtepdIsDelete = false;
                    }
                }

                //Update Emergency Contact
                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    foreach (var item in employee.EmsTblEmergencyContact)
                    {
                        item.EtecCreatedBy = employee.EtedFirstName;
                        item.EtecCreatedByDate = DateTime.Now;
                        item.EtecCreatedByName = employee.EtedLastName;
                        item.EtecIsDelete = false;
                    }
                }

                // Working History
                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach (var item in employee.EmsTblWorkingHistory)
                    {
                        item.EtwhCreatedBy = employee.EtedFirstName;
                        item.EtwhCreatedByDate = DateTime.Now;
                        item.EtwhCreatedByName = employee.EtedLastName;
                        item.EtwhIsDelete = false;
                        response.Message = UserMessages.strUpdated;
                        response.Success = true;


                    }

                }

                _hrmsemployeeRepository.Insert(employee);
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }

         else if (doesExistAlready == true)
            {
                response.Message = UserMessages.strAlrexist;
            }

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
           
            return response;
        }

        public BaseResponse UpdateEmployee(EmsTblEmployeeDetails employee)
        {

            //Update EmployeeDetails
            BaseResponse response = new BaseResponse();
            
            employee.EtedModifiedBy = employee.EtedFirstName;
            employee.EtedModifiedByDate = DateTime.Now;
            employee.EtedModifiedByName = employee.EtedLastName;
            employee.EtedIsDelete = false;

            // Update AcademicQualification
            if (employee.EmsTblAcademicQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblAcademicQualification)
                {
                    item.EtaqModifiedBy = employee.EtedFirstName;
                    item.EtaqModifiedByDate = DateTime.Now;
                    item.EtaqModifiedByName = employee.EtedLastName;
                    item.EtaqIsDelete = false;
                 
                }
            }

            //Update ProfessionalQualification
            if (employee.EmsTblProfessionalQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblProfessionalQualification)
                {
                    item.EtpqModifiedBy = employee.EtedFirstName;
                    item.EtpqModifiedByDate = DateTime.Now;
                    item.EtpqModifiedByName = employee.EtedLastName;
                    item.EtpqIsDelete = false;
                }
            }

            //Update ProfessionalDetails
            if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
            {
                foreach (var item in employee.EmsTblEmployeeProfessionalDetails)
                {
                    item.EtepdModifiedBy = employee.EtedFirstName;
                    item.EtepdModifiedByDate = DateTime.Now;
                    item.EtepdModifiedByName = employee.EtedLastName;
                    item.EtepdIsDelete = false;
                }
            }

            //Update Emergency Contact
            if (employee.EmsTblEmergencyContact.Count > 0)
            {
                foreach (var item in employee.EmsTblEmergencyContact)
                {
                    item.EtecModifiedBy = employee.EtedFirstName;
                    item.EtecModifiedByDate = DateTime.Now;
                    item.EtecModifiedByName = employee.EtedLastName;
                    item.EtecIsDelete = false;
                }
            }

            // Working History
            if (employee.EmsTblWorkingHistory.Count > 0)
            {
                foreach (var item in employee.EmsTblWorkingHistory)
                {
                    item.EtwhModifiedBy = employee.EtedFirstName;
                    item.EtwhModifiedByDate = DateTime.Now;
                    item.EtwhModifiedByName = employee.EtedLastName;
                    item.EtwhIsDelete = false;
                    response.Message = UserMessages.strUpdated;
                    response.Success = true;
                   
                    
                }
               
            }
          

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;
            }
            _hrmsemployeeRepository.Update(employee);
            _uow.Commit();
            return response;
        }

        public BaseResponse DeleteEmployee(int empId)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmployeeId == empId) > 0;
            bool isDeletedAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedIsDelete == true && p.EtedEmployeeId== empId) > 0;
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == empId)
           .ToList()
           .ForEach(x =>
           {
               x.EtedIsDelete = true;
               x.EtedModifiedBy = "212";
               x.EtedModifiedByDate = DateTime.Now;
               x.EtedModifiedByName = "dsada";

           });
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                response.Message = UserMessages.strDeleted;
                response.Success = true;
                response.Data = empId;
            }
            else if(isDeletedAlready == true && doesExistAlready == true)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;
            }
            else if(doesExistAlready == false)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }
            

            _uow.Commit();
            return response;


        }

        public BaseResponse ViewDataEmployeeByid(int id)
        {
            BaseResponse response = new BaseResponse();
            
           bool count = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId == id).Count() > 0;
            var employeesData = _hrmsemployeeRepository.Table.Include(x => x.EmsTblAcademicQualification).Include(x => x.EmsTblEmergencyContact).Include(x => x.EmsTblEmployeeProfessionalDetails).Include(x => x.EmsTblProfessionalQualification).Include(x => x.EmsTblWorkingHistory).Where(x=>x.EtedEmployeeId == id).ToList();
        

            if (count == true)
            {
                response.Data = employeesData;
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
