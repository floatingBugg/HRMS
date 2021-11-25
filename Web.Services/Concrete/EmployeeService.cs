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
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository, IHRMSEmployeeContactRepository employeeContactRepository, IUnitOfWork uow)
        {
            _config = config;
            _employeeContactRepository = employeeContactRepository;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _uow = uow;
        }

        public string CreateEmployee(EmployeeCredential employee)
        {
            if (!string.IsNullOrEmpty(employee.firstname) && !string.IsNullOrEmpty(employee.Lastname)
               && !string.IsNullOrEmpty(employee.personalemail) && !string.IsNullOrEmpty(employee.address) && !string.IsNullOrEmpty(employee.gender))
            {
                List<EmsTblEmergencyContact> obj2 = new List<EmsTblEmergencyContact>();
                List<EmsTblEmployeeDetails> obj = new List<EmsTblEmployeeDetails>();
                obj.Add(new EmsTblEmployeeDetails
                {
                    //EtedEmployeeId = employee.empID,
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

                obj2.Add(new EmsTblEmergencyContact
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
                _employeeContactRepository.Insert(obj2);
                _uow.Commit();
            }
            return null;
        }

        public IEnumerable<EmsTblEmployeeDetails> GetAllEmployee()
        {
            var employee = _hrmsemployeeRepository.GetList().ToList();
            var empContacts = _employeeContactRepository.GetList().ToList();
            var result = (from emp in employee
                          join empC in empContacts on emp.EtedEmployeeId equals empC.EtedEmployeeId
                          select emp).ToList();
            return employee;
        }
        
      
        public string UpdateEmployee(EmployeeCredential employee)
        {
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

            _uow.Commit();

            return "Success";
            

            

        }

        public IEnumerable<EmsTblEmployeeDetails> GetAllEmployeeContact()
        {
            var employee = _hrmsemployeeRepository.GetList().ToList();
            var empContacts = _employeeContactRepository.GetList().ToList();
            return _hrmsemployeeRepository.GetList().ToList();
        }
    }
}
