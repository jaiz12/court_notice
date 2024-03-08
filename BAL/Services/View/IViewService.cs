using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.View
{
    public interface IViewService
    {

        Task<DataTable> GetCountOfView();
        Task<DataTable> GetMarriageRegisteredDetails();
        Task<object> GetLandRegisteredDetails();
    }
}
