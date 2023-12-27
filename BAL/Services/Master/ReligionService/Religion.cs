using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.ReligionService
{
    public class Religion : MyDbContext, IReligion
    {
        //== Author: Dewas Rai
        //== Created_Date: 02 November 2023
        public async Task<List<Religion_DTO>> GetReligion()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable religionDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_religion_master", CommandType.StoredProcedure);
                List<Religion_DTO> religionList = new List<Religion_DTO>();
                religionList = DataTableVsListOfType.ConvertDataTableToList<Religion_DTO>(religionDT);
                return religionList;
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

        public async Task<DataResponse> Post(Religion_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_religion_name", data.religion_name);
                _sqlCommand.Add_Parameter_WithValue("@prm_created_by", data.created_by);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("@prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_religion_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Religion Added Successfully", true);
                else
                    return new DataResponse("Duplicate Religion Added", false);
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

        public async Task<DataResponse> Update(Religion_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_religion_id", data.religion_id);
                _sqlCommand.Add_Parameter_WithValue("@prm_religion_name", data.religion_name);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_religion_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Religion Updated Successfully", true);
                else
                    return new DataResponse("Religion Update Failed", false);
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
                _sqlCommand.Add_Parameter_WithValue("@prm_religion_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_religion_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Religion Deleted Successfully", true);
                else
                    return new DataResponse("Religion Delete Failed", false);
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
