using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.State
{
    public class StateService : MyDbContext, IStateService
    {
        private IMasterCommonService _commonService;
        public StateService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 07-11-2023
        public async Task<DataTable> GetState()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_state_master", CommandType.StoredProcedure));
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
        /// Date : 07-11-2023
        public DataResponse AddState(State_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_state_master", "state", model, false);
        }

        /// Author : Sandeep Chauhan
        /// Date : 07-11-2023
        public DataResponse EditState(State_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_state_master", "state", model, true);
        }

        /// Author : Sandeep Chauhan
        /// Date : 07-11-2023
        public async Task<DataResponse> DeleteState(long id)
        {
            return await _commonService.Delete("emp_delete_state_master", "state", id);
        }
    }
}
