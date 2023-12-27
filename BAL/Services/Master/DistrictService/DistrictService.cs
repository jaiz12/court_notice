using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.DistrictService
{
    public class DistrictService : MyDbContext, IDistrictService
    {
        private IMasterCommonService _commonService;
        public DistrictService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 07-11-2023
        public async Task<DataTable> GetDistrict()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_district_master", CommandType.StoredProcedure));
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
        public async Task<DataResponse> AddDistrict(District_DTO model)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", model.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_name", model.district_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", model.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", model.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_district_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("District Added Successfully", true);
                else
                    return new DataResponse("District Already Exists", false);
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
        public async Task<DataResponse> EditDistrict(District_DTO model)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", model.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", model.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_name", model.district_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", model.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_district_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("District Updated Successfully", true);
                else
                    return new DataResponse("District Already Exists", false);
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
        public async Task<DataResponse> DeleteDistrict(long id)
        {
            return await _commonService.Delete("emp_delete_district_master", "district", id);
        }
    }
}
