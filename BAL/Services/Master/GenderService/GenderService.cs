using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.GenderService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 03-Nov-2023
    // =============================================
    public class GenderService : MyDbContext, IGenderService
    {
        public async Task<DataResponse> PostGender(Gender_DTO gender)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_gender_name", gender.gender_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", gender.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", gender.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_gender_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Gender Added Successfully", true);
                else
                    return new DataResponse("Gender Already Exists", false);
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
        public async Task<DataResponse> PutGender(Gender_DTO gender)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_gender_id", gender.gender_id);
                _sqlCommand.Add_Parameter_WithValue("prm_gender_name", gender.gender_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", gender.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_gender_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Gender Updated Successfully", true);
                else
                    return new DataResponse("Gender Already Exists", false);
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
        public async Task<List<Gender_DTO>> GetGender()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_gender_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<Gender_DTO>(result);
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
        public async Task<DataResponse> DeleteGender(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_gender_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_gender_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Gender Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Gender", false);
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
