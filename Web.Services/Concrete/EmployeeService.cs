using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSAcademicRepository _hrmsacademicrepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        private readonly IHRMSPRofessionalRepository _hrmsprofessionalrepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        private readonly IHRMSEmployeeWorkingHistoryRepository _workinghistoryRepository;
        private readonly IHRMSProfessionalDetailsRepository _hrmsprofessionaldetailsrepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository,IHRMSAcademicRepository hRMSAcademicRepository,IHRMSEmployeeContactRepository  employeeContactRepository,IHRMSPRofessionalRepository  hRMSProfessionalRepository,IHRMSEmployeeWorkingHistoryRepository workingHistoryRepository, IHRMSProfessionalDetailsRepository hRMSProfessionalDetailsRepository, IUnitOfWork uow)

 
        {
            _config = config;
            _employeeContactRepository = employeeContactRepository;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _hrmsacademicrepository = hRMSAcademicRepository;
            _hrmsprofessionalrepository = hRMSProfessionalRepository;
            _workinghistoryRepository = workingHistoryRepository;
            _hrmsprofessionaldetailsrepository = hRMSProfessionalDetailsRepository;
            _uow = uow;
        }


        public List<DisplayEmployeeGrid> GetAllEmployee()
        {
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not assigned"
            }).ToList();

            return employeesData;
        }

        public string CreateEmployee(EmsTblEmployeeDetails employee)
        {
            if (!string.IsNullOrEmpty(employee.EtedFirstName) && !string.IsNullOrEmpty(employee.EtedLastName)
               && !string.IsNullOrEmpty(employee.EtedEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender))
            {
                employee.EtedCreatedBy = "admin";
                employee.EtedCreatedByDate = DateTime.Now;
                employee.EtedCreatedByName = "admin";
                employee.EtedIsDelete = false;
                _hrmsemployeeRepository.Insert(employee);
                return "success";
            }
            return null;
        }
      
        public string UpdateEmployee(EmsTblEmployeeDetails employee)
        {

            //Update EmployeeDetails

               employee.EtedModifiedBy = "admin";
               employee.EtedModifiedByDate = DateTime.Now;
               employee.EtedModifiedByName = "admin";
               employee.EtedIsDelete = false;

            // Update AcademicQualification
            if (employee.EmsTblAcademicQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblAcademicQualification)
                {
                    item.EtaqModifiedBy = "admin";
                    item.EtaqModifiedByDate = DateTime.Now;
                    item.EtaqModifiedByName = "admin";
                    item.EtaqIsDelete = false;
                }
            }

            //Update ProfessionalQualification
            if (employee.EmsTblProfessionalQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblProfessionalQualification)
                {
                    item.EtpqModifiedBy = "admin";
                    item.EtpqModifiedByDate = DateTime.Now;
                    item.EtpqModifiedByName = "admin";
                    item.EtpqIsDelete = false;
                }
            }

            //Update ProfessionalDetails
            if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
            {
                foreach (var item in employee.EmsTblEmployeeProfessionalDetails)
                {
                    item.EtepdModifiedBy = "admin";
                    item.EtepdModifiedByDate = DateTime.Now;
                    item.EtepdModifiedByName = "admin";
                    item.EtepdIsDelete = false;
                }
            }

            //Update Emergency Contact
            if (employee.EmsTblEmergencyContact.Count > 0)
            {
                foreach (var item in employee.EmsTblEmergencyContact)
                {
                    item.EtecModifiedBy = "admin";
                    item.EtecModifiedByDate = DateTime.Now;
                    item.EtecModifiedByName = "admin";
                    item.EtecIsDelete = false;
                }
            }

            // Working History
            if (employee.EmsTblWorkingHistory.Count > 0)
            {
                foreach (var item in employee.EmsTblWorkingHistory)
                {
                    item.EtwhModifiedBy = "admin";
                    item.EtwhModifiedByDate = DateTime.Now;
                    item.EtwhModifiedByName = "admin";
                    item.EtwhIsDelete = false;
                }
            }

            _hrmsemployeeRepository.Update(employee);
       
            _uow.Commit();
            return "Success";
            
            }

        public string DeleteEmployee(int empId)
        {
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == empId)
           .ToList()
           .ForEach(x =>
           {
               x.EtedIsDelete = true;
               x.EtedModifiedBy = "admin";
               x.EtedModifiedByDate = DateTime.Now;
               x.EtedModifiedByName = "admin";
           });
            _uow.Commit();
            return "Successfully Deleted";
        }
           
    }
}
