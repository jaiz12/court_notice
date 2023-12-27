using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.DesignationService
{
    internal class DesignationService : MyDbContext, IDesignationService
    {
        private IMasterCommonService _commonService;
        public DesignationService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public async Task<DataTable> GetDesignations()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_designation_master", CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public DataResponse AddDesignation(Designation_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_designation_master", "designation", model, false);

        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public DataResponse EditDesignation(Designation_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_designation_master", "designation", model, true);

        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public async Task<DataResponse> DeleteDesignation(long id)
        {
            return await _commonService.Delete("emp_delete_designation_master", "designation", id);
        }

    }
}
