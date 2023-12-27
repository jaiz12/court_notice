using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.NationalityService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 02-Nov-2023
    // =============================================
    public class NationalityService : MyDbContext, INationalityService
    {
        public async Task<DataResponse> PostNationality(Nationality_DTO nationality)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_nationality_name", nationality.nationality_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", nationality.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", nationality.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_nationality_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Nationality Added Successfully", true);
                else
                    return new DataResponse("Nationality Already Exists", false);
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
        public async Task<DataResponse> PutNationality(Nationality_DTO nationality)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_nationality_id", nationality.nationality_id);
                _sqlCommand.Add_Parameter_WithValue("prm_nationality_name", nationality.nationality_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", nationality.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_nationality_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Nationality Updated Successfully", true);
                else
                    return new DataResponse("Nationality Already Exists", false);
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
        public async Task<List<Nationality_DTO>> GetNationality()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_nationality_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<Nationality_DTO>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> DeleteNationality(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_nationality_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_nationality_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Nationality Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Nationality", false);
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
