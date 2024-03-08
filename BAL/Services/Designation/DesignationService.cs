using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Designation
{
    public class DesignationService : MyDbContext, IDesignationService
    {
        public async Task<DataResponse> Post(DesignationDTO designationDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_designation_name", Regex.Replace(designationDTO.designation_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", designationDTO.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", designationDTO.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_designation_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Designation Added Successfully", true);
                else
                    return new DataResponse("Designation Already Exists", false);
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

        public async Task<DataResponse> Put(DesignationDTO designationDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", designationDTO.designation_id);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_name", Regex.Replace(designationDTO.designation_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", designationDTO.updated_by);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("update_designation_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Designation Updated Successfully", true);
                else
                    return new DataResponse("Designation Already Exists", false);
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
        public async Task<List<DesignationDTO>> Get()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("get_designation_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<DesignationDTO>(result);
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
        public async Task<DataResponse> Delete(long designation_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", designation_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_designation_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Designation Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Designation", false);
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
