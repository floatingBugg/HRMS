using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface IPositionAppliedService
    {
        public BaseResponse CreatePosition(RmsTblPositionAppliedVM position);

        public BaseResponse UpdatePosition(RmsTblPositionAppliedVM position);

        public BaseResponse DisplayPosition();

        public BaseResponse DeletePosition(int id);
    }
}
