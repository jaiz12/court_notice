using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Department
{
    public class DepartmentService : MyDbContext, IDepartmentService
    {
        public async Task<DataResponse> Post(DepartmentDTO departmentDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_department_name", Regex.Replace(departmentDTO.department_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", departmentDTO.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", departmentDTO.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_department_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Department Added Successfully", true);
                else
                    return new DataResponse("Department Already Exists", false);
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

        public async Task<DataResponse> Put(DepartmentDTO departmentDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_department_id", departmentDTO.department_id);
                _sqlCommand.Add_Parameter_WithValue("prm_department_name", Regex.Replace(departmentDTO.department_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", departmentDTO.updated_by);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("update_department_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Department Updated Successfully", true);
                else
                    return new DataResponse("Department Already Exists", false);
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
        public async Task<List<DepartmentDTO>> Get()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("get_department_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<DepartmentDTO>(result);
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
        public async Task<DataResponse> Delete(long department_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_department_id", department_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_department_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Department Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Department", false);
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
