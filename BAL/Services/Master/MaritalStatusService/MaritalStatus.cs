using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MaritalStatusService
{
    public class MaritalStatus : MyDbContext, IMaritalStatus
    {
        //== Author: Dewas Rai
        //== Created_Date: 02 November 2023
        public async Task<DataResponse> Post(MaritalStatus_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_marital_status_name", data.marital_status_name);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", data.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_marital_status_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Marital Status Added Succesfully", true);
                else
                    return new DataResponse("Duplicate Marital Status Added", false);
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

        public async Task<List<MaritalStatus_DTO>> GetMaritalStatus()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable maritalDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_marital_status_master", CommandType.StoredProcedure);
                List<MaritalStatus_DTO> maritalList = new List<MaritalStatus_DTO>();
                maritalList = DataTableVsListOfType.ConvertDataTableToList<MaritalStatus_DTO>(maritalDT);
                return maritalList;
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

        public async Task<DataResponse> Update(MaritalStatus_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_marital_status_id", data.marital_status_id);
                _sqlCommand.Add_Parameter_WithValue("prm_marital_status_name", data.marital_status_name);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_marital_status_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Marital Status Updated successfully", true);
                else
                    return new DataResponse("Marital Status Update failed", false);
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

        public async Task<DataResponse> Delete(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_marital_status_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_marital_status_master", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Marital Status Deleted Succesfully", true);
                else
                    return new DataResponse("Marital Status Delete Failed", false);
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
    }
}
