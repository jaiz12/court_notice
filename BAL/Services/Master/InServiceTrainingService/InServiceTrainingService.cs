using BAL.Services.Master.Common;
using Common.DbContext;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DTO.Models.Master
{
    public class InServiceTrainingService : MyDbContext, IInServiceTrainingService
    {
        private IMasterCommonService _commonService;
        public InServiceTrainingService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 06-11-2023
        public async Task<DataTable> GetInServiceTraining()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_inservice_training_master", CommandType.StoredProcedure));
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
        /// Date : 06-11-2023
        public DataResponse AddInServiceTraining(InServiceTraining_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_inservice_training_master", "inservice_training", model, false);
        }

        /// Author : Sandeep Chauhan
        /// Date : 06-11-2023
        public DataResponse EditInServiceTraining(InServiceTraining_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_inservice_training_master", "inservice_training", model, true);
        }

        /// Author : Sandeep Chauhan
        /// Date : 06-11-2023
        public async Task<DataResponse> DeleteInServiceTraining(long id)
        {
            return await _commonService.Delete("emp_delete_inservice_training_master", "inservice_training", id);
        }
    }
}
