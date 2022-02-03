using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface ILeaveService
    {
        public BaseResponse CreateLeave(LmsLeaveRecordVM leave);

        public BaseResponse UpdateLeave(LmsLeaveRecordVM leave);

        public BaseResponse ViewLeaveByempid(int id);

        public BaseResponse ViewLeaveByrecordid(int id);

    }
}
