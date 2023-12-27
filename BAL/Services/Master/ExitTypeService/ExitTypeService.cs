using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.ExitTypeService
{
    public class ExitTypeService : MyDbContext, IExitTypeService
    {
        private IMasterCommonService _commonService;
        public ExitTypeService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public async Task<DataTable> GetExitType()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_exit_type_master", CommandType.StoredProcedure));
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
        /// Date : 03-11-2023
        public DataResponse AddExitType(ExitType_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_exit_type_master", "exit_type", model, false);
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public DataResponse EditExitType(ExitType_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_exit_type_master", "exit_type", model, true);
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public async Task<DataResponse> DeleteExitType(long id)
        {
            return await _commonService.Delete("emp_delete_exit_type_master", "exit_type", id);
        }
    }
}
