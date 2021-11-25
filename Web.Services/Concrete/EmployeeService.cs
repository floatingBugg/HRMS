using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.Interfaces;
using Web.DLL.Models;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSAcademicRepository _hrmsacademicrepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        private readonly IHRMSProfessionalRepository _hrmsprofessionalrepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        private readonly IHRMSEmployeeWorkingHistoryRepository _workinghistoryRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository,IHRMSEmployeeContactRepository employeeContactRepository, IHRMSEmployeeWorkingHistoryRepository workingHistoryRepository, IHRMSAcademicRepository hRMSAcademicRepository,IHRMSProfessionalRepository hRMSProfessionalRepository, IUnitOfWork uow)

 
        {
            _config = config;
            _employeeContactRepository = employeeContactRepository;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _hrmsacademicrepository = hRMSAcademicRepository;
            _hrmsprofessionalrepository = hRMSProfessionalRepository;
            _workinghistoryRepository = workingHistoryRepository;

            _uow = uow;
        }

        public string CreateEmployee(EmployeeCredential employee)
        {
            if (!string.IsNullOrEmpty(employee.firstname) && !string.IsNullOrEmpty(employee.Lastname)
               && !string.IsNullOrEmpty(employee.personalemail) && !string.IsNullOrEmpty(employee.address) && !string.IsNullOrEmpty(employee.gender))
            {
                List<EmsTblWorkingHistory> obj4 = new List<EmsTblWorkingHistory>();
                List<EmsTblEmergencyContact> obj3 = new List<EmsTblEmergencyContact>();
                List<EmsTblEmployeeDetails> obj = new List<EmsTblEmployeeDetails>();
                List<EmsTblAcademicQualification> obj1 = new List<EmsTblAcademicQualification>();
                List<EmsTblProfessionalQualification> obj2 = new List<EmsTblProfessionalQualification>();

                //Create Method Employee Details

                obj.Add(new EmsTblEmployeeDetails
                {

     
                    EtedFirstName = employee.firstname,
                    EtedLastName = employee.Lastname,
                    EtedEmailAddress = employee.personalemail,
                    EtedCnic = employee.cnic,
                    EtedDob = DateTime.Now,
                    EtedGender = employee.gender,
                    EtedAddress = employee.address,
                    EtedMaritalStatus = employee.martialstatus,
                    EtedBloodGroup = employee.bloodgroup,
                    EtedContactNumber = employee.contact,
                    EtedNationality = employee.nationality,
                    EtedReligion = employee.religion,
                    EtedStatus = employee.empstatus,
                    EtedPhotograph = new byte[2],
                    EtedOfficialEmailAddress = employee.officialemail,
                    EtedCreatedBy = "test",
                    EtedCreatedByDate = DateTime.Now,
                    EtedCreatedByName = "test",
                    EtedModifiedBy = "test",
                    EtedModifiedByDate = DateTime.Now,
                    EtedModifiedByName = "test",
                    EtedIsDelete = "no",


                });
                _hrmsemployeeRepository.Insert(obj);


                //Create Method academic Qualification

                obj1.Add(new EmsTblAcademicQualification
                {
                    EtedEmployeeId = obj.FirstOrDefault().EtedEmployeeId,
                    EtaqQualification = employee.Qualification,
                    EtaqPassingYear = DateTime.Now,
                    EtaqCgpa = 1.2,
                    EtaqInstituteName = employee.InstituteName,
                    EtaqUploadDocuments = new byte[2],
                    EtaqCreatedBy = "test",
                    EtaqCreatedByDate = DateTime.Now,
                    EtaqCreatedByName = "Test",
                    EtaqModifiedBy = "test",
                    EtaqModifiedByDate = DateTime.Now,
                    EtaqModifiedByName = "test",
                    EtaqIsDelete = "No",
                });
                _hrmsacademicrepository.Insert(obj1);

                //Create Method Professional Qualification

                obj2.Add(new EmsTblProfessionalQualification
                {
                    EtedEmployeeId=obj.FirstOrDefault().EtedEmployeeId,
                    EtpqCertification=employee.Certification,
                    EtpqStratDate=DateTime.Now,
                    EtpqEndDate=DateTime.Now,
                    EtpqDocuments=new byte[2],
                    EtpqInstituteName=employee.InstituteName,
                    EtpqCreatedBy="test",
                    EtpqCreatedByName="test",
                    EtpqCreatedByDate=DateTime.Now,
                    EtpqModifiedBy="test",
                    EtpqModifiedByName="test",
                    EtpqModifiedByDate=DateTime.Now,
                    EtpqIsDelete="no"



                });
                _hrmsprofessionalrepository.Insert(obj2);

                obj3.Add(new EmsTblEmergencyContact
                {
                    
                    EtecFirstName=employee.emergencyfirstname,
                    EtecLastName=employee.emergencylastname,
                    EtecRelation=employee.emergencyrelation,
                    EtecContactNumber=employee.emergencycontact,
                    EtecAddress=employee.emergencyaddress,
                    EtedEmployeeId=obj.FirstOrDefault().EtedEmployeeId,
                    EtecCreatedBy= "test",
                    EtecCreatedByName = "test",
                    EtecCreatedByDate = DateTime.Now,
                    EtecModifiedBy = "test",
                    EtecModifiedByDate = DateTime.Now,
                    EtecModifiedByName = "test",
                    EtecIsDelete = "no",

                });
                _employeeContactRepository.Insert(obj3);
                obj4.Add(new EmsTblWorkingHistory
                {
                    EtwhCompanyName=employee.companyname,
                    EtwhStratDate = DateTime.Now,
                    EtwhEndDate= DateTime.Now,
                    EtwhDuration=employee.duration,
                    EtwhDesignation=employee.designation,
                    EtwhExperienceLetter=new byte[2],
                    EtedEmployeeId = obj.FirstOrDefault().EtedEmployeeId,
                    EtwhCreatedBy = "test",
                    EtwhCreatedByName = "test",
                    EtwhCreatedByDate = DateTime.Now,
                    EtwhModifiedBy = "test",
                    EtwhModifiedByDate = DateTime.Now,
                    EtwhModifiedByName = "test",
                    EtwhIsDelete = "no",
                });
                _workinghistoryRepository.Insert(obj4);
                _uow.Commit();

            }
            return null;
        }

        public IEnumerable<EmsTblEmployeeDetails> GetAllEmployee()
        {

            
            //var getAlldata= _hrmsemployeeRepository.Table.Where(EmsTblAcad
            return _hrmsemployeeRepository.GetList().ToList();
           
            
        }
        
    
        public string UpdateEmployee(EmployeeCredential employee)
        {
            //Update EmployeeDetails
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
                .ToList()
                .ForEach(x =>
            {
                x.EtedFirstName = employee.firstname;
                x.EtedLastName = employee.Lastname;
                x.EtedEmailAddress = employee.personalemail;
                x.EtedCnic = employee.cnic;
                x.EtedDob = DateTime.Now;
                x.EtedGender = employee.gender;
                x.EtedAddress = employee.address;
                x.EtedMaritalStatus = employee.martialstatus;
                x.EtedBloodGroup = employee.bloodgroup;
                x.EtedContactNumber = employee.contact;
                x.EtedNationality = employee.nationality;
                x.EtedReligion = employee.religion;
                x.EtedStatus = employee.empstatus;
                x.EtedPhotograph = new byte[2];
                x.EtedOfficialEmailAddress = employee.officialemail;
                x.EtedCreatedBy = "test";
                x.EtedCreatedByDate = DateTime.Now;
                x.EtedCreatedByName = "test";
                x.EtedModifiedBy = "test";
                x.EtedModifiedByDate = DateTime.Now;
                x.EtedModifiedByName = "test";
                x.EtedIsDelete = "no";

            });

            //Update AcademicQualification

            _hrmsacademicrepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
                .ToList()
                .ForEach(x =>
                {
                x.EtaqQualification = employee.Qualification;
                x.EtaqPassingYear = DateTime.Now;
                x.EtaqCgpa = 1.2;
                x.EtaqInstituteName = employee.InstituteName;
                x.EtaqUploadDocuments = new byte[2];
                x.EtaqCreatedBy = "test";
                x.EtaqCreatedByName = "test";
                x.EtaqCreatedByDate = DateTime.Now;
                x.EtaqModifiedBy = "test";
                x.EtaqModifiedByName = "test";
                x.EtaqModifiedByDate = DateTime.Now;
                x.EtaqIsDelete = "no";

                    });
            //Update ProfessionalQualification

            _hrmsprofessionalrepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
                .ToList()
                .ForEach(x =>
                {
                    x.EtpqInstituteName = employee.InstituteName;
                    x.EtpqDocuments = new byte[2];
                    x.EtpqCreatedBy = employee.created;
                    x.EtpqCreatedByDate = DateTime.Now;
                    x.EtpqCreatedByName = employee.createdName;
                    x.EtpqModifiedBy = employee.modified;
                    x.EtpqModifiedByDate = DateTime.Now;
                    x.EtpqModifiedByName = employee.modifiedName;
                    x.EtpqIsDelete = "no";
                    x.EtpqCertification = employee.Certification;
                    

                });
                  
            _employeeContactRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
                .ToList()
                .ForEach(x => 
                {
                    x.EtecFirstName = employee.emergencyfirstname;
                    x.EtecLastName = employee.emergencylastname;
                    x.EtecRelation = employee.emergencyrelation;
                    x.EtecContactNumber = employee.emergencycontact;
                    x.EtecAddress = employee.emergencyaddress;
                    x.EtecCreatedBy = "test";
                    x.EtecCreatedByName = "test";
                    x.EtecCreatedByDate = DateTime.Now;
                    x.EtecModifiedBy = "test";
                    x.EtecModifiedByDate = DateTime.Now;
                    x.EtecModifiedByName = "test";
                    x.EtecIsDelete = "no";       

                });

            _workinghistoryRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
               .ToList()
               .ForEach(x =>
               {
                   x.EtwhCompanyName = employee.companyname;
                   x.EtwhStratDate = DateTime.Now;
                   x.EtwhEndDate = DateTime.Now;
                   x.EtwhDuration = employee.duration;
                   x.EtwhDesignation = employee.designation;
                   x.EtwhExperienceLetter = new byte[2];
                   x.EtwhCreatedBy = "test";
                   x.EtwhCreatedByName = "test";
                   x.EtwhCreatedByDate = DateTime.Now;
                   x.EtwhModifiedBy = "test";
                   x.EtwhModifiedByDate = DateTime.Now;
                   x.EtwhModifiedByName = "test";
                   x.EtwhIsDelete = "no";

               });

            _uow.Commit();






        _uow.Commit();
            return "Success";
            
            }

        public string DeleteEmployee(int id)
        {

            var Empacad = _hrmsacademicrepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            _hrmsacademicrepository.Delete(Empacad);
            var Empprof = _hrmsprofessionalrepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            _hrmsprofessionalrepository.Delete(Empprof);
            var empcontact = _employeeContactRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            _employeeContactRepository.Delete(empcontact);
            var empWork = _workinghistoryRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            _workinghistoryRepository.Delete(empWork);
            var Emp = _hrmsemployeeRepository.Table.Where(emp => emp.EtedEmployeeId == id).FirstOrDefault();
            _hrmsemployeeRepository.Delete(Emp);

            _uow.Commit();
            return "Successfully Deleted";
        }
            

        

        public IEnumerable<EmsTblEmployeeDetails> GetAllEmployeeContact()
        {
            var employee = _hrmsemployeeRepository.GetList().ToList();
            var empContacts = _employeeContactRepository.GetList().ToList();
            return _hrmsemployeeRepository.GetList().ToList();
        }

       
    }
}
