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
        BaseResponse createAssign(ImsAssignVM assign);

        BaseResponse updateAssign(ImsAssignVM assign);

        BaseResponse deleteAssign(int assignid);


        BaseResponse displayAllAssetAssigned(int type);

        BaseResponse getEmployee();

        BaseResponse ViewDataAssignByid(int id);

    }
}
