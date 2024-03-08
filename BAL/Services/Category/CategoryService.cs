using BAL.Services.Category;
using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.category
{
    public class CategoryService : MyDbContext, ICategoryService
    {
        public async Task<DataResponse> Post(CategoryDTO categoryDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_category_name", Regex.Replace(categoryDTO.category_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", categoryDTO.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", categoryDTO.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_category_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Category Added Successfully", true);
                else
                    return new DataResponse("Category Already Exists", false);
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

        public async Task<DataResponse> Put(CategoryDTO categoryDTO)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_category_id", categoryDTO.category_id);
                _sqlCommand.Add_Parameter_WithValue("prm_category_name", Regex.Replace(categoryDTO.category_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", categoryDTO.updated_by);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("update_category_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Category Updated Successfully", true);
                else
                    return new DataResponse("Category Already Exists", false);
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
        public async Task<List<CategoryDTO>> Get()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("get_category_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<CategoryDTO>(result);
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
        public async Task<DataResponse> Delete(long category_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_category_id", category_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_category_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Category Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Category", false);
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
