using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface IAssetAssignService
    {
        public BaseResponse createAssign(ImsAssignVM assign);

        public BaseResponse updateAssign(ImsAssignVM assign);

        public BaseResponse deleteAssign(int assignid);


        public BaseResponse displayAllAssetAssigned(int type);

        public BaseResponse getEmployee();

        public BaseResponse ViewAllDataAssign(int empid);

       

    }
}
