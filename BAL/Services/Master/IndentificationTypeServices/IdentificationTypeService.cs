using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.IndentificationTypeServices
{
    public class IdentificationTypeService : MyDbContext, IIdentificationTypeService
    {
        private IMasterCommonService _commonService;
        public IdentificationTypeService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public async Task<DataTable> GetIndentificationType()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_identification_type_master", CommandType.StoredProcedure));
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
        public DataResponse AddIndentificationType(IdentificationType_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_identification_type_master", "identification_type", model, false);
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public DataResponse EditIndentificationType(IdentificationType_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_identification_type_master", "identification_type", model, true);
        }

        /// Author : Sandeep Chauhan
        /// Date : 03-11-2023
        public async Task<DataResponse> DeleteIndentificationType(long id)
        {
            return await _commonService.Delete("emp_delete_identification_type_master", "identification_type", id);
        }
    }
}
