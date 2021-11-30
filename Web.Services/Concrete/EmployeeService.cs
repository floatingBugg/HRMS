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

        public string CreateEmployee(EmsTblEmployeeDetails employee)
        {
            if (!string.IsNullOrEmpty(employee.EtedFirstName) && !string.IsNullOrEmpty(employee.EtedLastName)
               && !string.IsNullOrEmpty(employee.EtedEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender))
            {
                employee.EtedCreatedBy = "test";
                employee.EtedCreatedByDate = DateTime.Now;
                employee.EtedCreatedByName = "test";
                employee.EtedModifiedBy = "test";
                employee.EtedModifiedByDate = DateTime.Now;
                employee.EtedModifiedByName = "test";
                employee.EtedIsDelete = false;
                _hrmsemployeeRepository.Insert(employee);
                return "success";
            }
            return null;
        }

       
    
        public string UpdateEmployee(EmsTblEmployeeDetails employee)
        {

            //Update EmployeeDetails
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == employee.EtedEmployeeId)
                .ToList()
                .ForEach(x =>
            {
                x.EtedFirstName = employee.EtedFirstName;
                x.EtedLastName = employee.EtedLastName;
                x.EtedEmailAddress = employee.EtedEmailAddress;
                x.EtedCnic = employee.EtedCnic;
                x.EtedDob = employee.EtedDob;
                x.EtedGender = employee.EtedGender;
                x.EtedAddress = employee.EtedEmailAddress;
                x.EtedMaritalStatus = employee.EtedMaritalStatus;
                x.EtedBloodGroup = employee.EtedBloodGroup;
                x.EtedContactNumber = employee.EtedContactNumber;
                x.EtedNationality = employee.EtedNationality;
                x.EtedReligion = employee.EtedReligion;
                x.EtedStatus = employee.EtedStatus;
                x.EtedPhotograph = "Hello";
                x.EtedOfficialEmailAddress = employee.EtedOfficialEmailAddress;
                x.EtedCreatedBy = "test";
                x.EtedCreatedByDate = DateTime.Now;
                x.EtedCreatedByName = "test";
                x.EtedModifiedBy = "test";
                x.EtedModifiedByDate = DateTime.Now;
                x.EtedModifiedByName = "test";
                x.EtedIsDelete = false;

            });

            //Update AcademicQualification
            //var academicQualification = employee.EmsTblAcademicQualification.Where(x => x.EtaqEtedEmployeeId == employee.EtedEmployeeId).FirstOrDefault();
            //_hrmsacademicrepository.Table.Where(p => p.EtaqEtedEmployeeId == employee.EtedEmployeeId)
            //    .ToList()
            //    .ForEach(x =>
            //    {
            //        x.EtaqQualification = academicQualification.EtaqQualification;
            //        x.EtaqPassingYear = academicQualification.EtaqPassingYear;
            //        x.EtaqCgpa = 1.2;
            //        x.EtaqInstituteName = employee.EmsTblAcademicQualification[0;
            //        x.EtaqUploadDocuments = "HI";
            //        x.EtaqCreatedBy = "test";
            //        x.EtaqCreatedByName = "test";
            //        x.EtaqCreatedByDate = DateTime.Now;
            //        x.EtaqModifiedBy = "test";
            //        x.EtaqModifiedByName = "test";
            //        x.EtaqModifiedByDate = DateTime.Now;
            //        x.EtaqIsDelete = false;

            //    });
            ////Update ProfessionalQualification

            //_hrmsprofessionalrepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
            //    .ToList()
            //    .ForEach(x =>
            //    {
            //        x.EtpqInstituteName = employee.InstituteName;
            //        x.EtpqDocuments = "Tarzan";
            //        x.EtpqCreatedBy = employee.created;
            //        x.EtpqCreatedByDate = DateTime.Now;
            //        x.EtpqCreatedByName = employee.createdName;
            //        x.EtpqModifiedBy = employee.modified;
            //        x.EtpqModifiedByDate = DateTime.Now;
            //        x.EtpqModifiedByName = employee.modifiedName;
            //        x.EtpqIsDelete = false;
            //        x.EtpqCertification = employee.Certification;


            //    });
            ////Update Emergency Contact
            //_employeeContactRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
            //    .ToList()
            //    .ForEach(x =>
            //    {
            //        x.EtecFirstName = employee.emergencyfirstname;
            //        x.EtecLastName = employee.emergencylastname;
            //        x.EtecRelation = employee.emergencyrelation;
            //        x.EtecContactNumber = employee.emergencycontact;
            //        x.EtecAddress = employee.emergencyaddress;
            //        x.EtecCreatedBy = "test";
            //        x.EtecCreatedByName = "test";
            //        x.EtecCreatedByDate = DateTime.Now;
            //        x.EtecModifiedBy = "test";
            //        x.EtecModifiedByDate = DateTime.Now;
            //        x.EtecModifiedByName = "test";
            //        x.EtecIsDelete = false;

            //    });
            //// Working History
            //_workinghistoryRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
            //   .ToList()
            //   .ForEach(x =>
            //   {
            //       x.EtwhCompanyName = employee.companyname;
            //       x.EtwhStratDate = DateTime.Now;
            //       x.EtwhEndDate = DateTime.Now;
            //       x.EtwhDuration = employee.duration;
            //       x.EtwhDesignation = employee.Olddesignation;
            //       x.EtwhExperienceLetter = "Twist";
            //       x.EtwhCreatedBy = "test";
            //       x.EtwhCreatedByName = "test";
            //       x.EtwhCreatedByDate = DateTime.Now;
            //       x.EtwhModifiedBy = "test";
            //       x.EtwhModifiedByDate = DateTime.Now;
            //       x.EtwhModifiedByName = "test";
            //       x.EtwhIsDelete = false;

            //   });



            //_hrmsprofessionaldetailsrepository.Table.Where(p => p.EtedEmployeeId == employee.empID).ToList().ForEach(x =>
            //    {
            //        x.EtepdDesignation = employee.NewDesignation;
            //        x.EtepdSalary = employee.Salary;
            //        x.EtepdJoiningDate = DateTime.Now;
            //        x.EtepdProbation = "1";
            //        x.EtepdCreatedBy = "test";
            //        x.EtepdCreatedByName = "test";
            //        x.EtepdCreatedByDate = DateTime.Now;
            //        x.EtepdModifiedBy = "test";
            //        x.EtepdModifiedByName = "test";
            //        x.EtepdModifiedByDate = DateTime.Now;
            //        x.EtepdIsDelete = false;

            //    });

            _uow.Commit();
            return "Success";
            
            }

        public string DeleteEmployee(int id)
        {

            //var Empacad = _hrmsacademicrepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_hrmsacademicrepository.Delete(Empacad);
            //var Empprof = _hrmsprofessionalrepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_hrmsprofessionalrepository.Delete(Empprof);
            //var empcontact = _employeeContactRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_employeeContactRepository.Delete(empcontact);
            //var empWork = _workinghistoryRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_workinghistoryRepository.Delete(empWork);
            //var empprofdetail = _hrmsprofessionaldetailsrepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_hrmsprofessionaldetailsrepository.Delete(empprofdetail);
            //var Emp = _hrmsemployeeRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            //_hrmsemployeeRepository.Delete(Emp);
            return "Successfully Deleted";
        }
            

        public List<DisplayEmployeeGrid> GetAllEmployee()
        {
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            //var employeesData = _hrmsemployeeRepository.Table.Include(x=>x.EmsTblEmployeeProfessionalDetail).Select(x=>new DisplayEmployeeGrid()
            //{
            //    empID = x.EtedEmployeeId,
            //           fullName = x.EtedFirstName,
            //           emailAddress = x.EtedEmailAddress,
            //           contactNumber = x.EtedContactNumber,
            //           empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y=> y.EtedEmployeeId == x.EtedEmployeeId).Select(z=>z.EtepdDesignation).FirstOrDefault() : "Not assigned"
            //}).ToList();

            return empCred;
        }
    }
}
