using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface IEmpLeaveService
    {
        public BaseResponse createLeave(LmsEmployeeLeaveVM leave);
        public BaseResponse updateLeave(LmsEmployeeLeaveVM leave);
        public BaseResponse ViewLeaveRecordByempid(int id);

        public BaseResponse EmployeeData(int roleid, int empid);
    }
}

