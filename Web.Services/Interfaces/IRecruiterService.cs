using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface IRecruiterService
    {
        public BaseResponse CreateRecruit(RmsTblRecruiterVM recruit);

        public BaseResponse UpdateRecruit(RmsTblRecruiterVM recruit);

        public BaseResponse DisplayRecruit();

        public BaseResponse DeleteRecruit(int id);

        public BaseResponse ViewDataRecruitByid(int id);
    }
}
