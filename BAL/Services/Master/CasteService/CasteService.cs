using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.CasteService
{
    public class CasteService : MyDbContext, ICasteService
    {
        private IMasterCommonService _commonService;
        public CasteService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 01-11-2023
        public async Task<DataTable> GetCaste()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_caste_master", CommandType.StoredProcedure));
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
        /// Date : 01-11-2023
        public DataResponse AddCaste(Caste_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_caste_master", "caste", model, false);

        }

        /// Author : Sandeep Chauhan
        /// Date : 01-11-2023
        public DataResponse EditCaste(Caste_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_caste_master", "caste", model, true);

        }


        /// Author : Sandeep Chauhan
        /// Date : 01-11-2023
        public async Task<DataResponse> DeleteCaste(long id)
        {
            return await _commonService.Delete("emp_delete_caste_master", "caste", id);
        }

    }
}
