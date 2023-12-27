using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.DivisionService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 02-Nov-2023
    // =============================================
    public class DivisionService : MyDbContext, IDivisionService
    {
        public async Task<DataResponse> PostDivision(Division_DTO division)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_division_name", division.division_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", division.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", division.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", division.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_division_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Division Added Successfully", true);

                else
                    return new DataResponse("Division Already Exists", false);
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
        public async Task<DataResponse> PutDivision(Division_DTO division)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_division_id", division.division_id);
                _sqlCommand.Add_Parameter_WithValue("prm_division_name", division.division_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", division.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", division.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_division_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Division Updated Successfully", true);
                else
                    return new DataResponse("Division Already Exists", false);
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
        public async Task<List<Division_DTO>> GetDivision()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_division_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<Division_DTO>(result);
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
        public async Task<DataResponse> DeleteDivision(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_division_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_division_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Division Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Division", false);
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
