using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 09-Nov-2023
// =============================================

namespace BAL.Services.Master.BranchOfficeService
{
    public class BranchService : MyDbContext, IBranchService
    {
        public async Task<DataResponse> PostBranch(Branch_DTO branchOffice)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_branch_name", branchOffice.branch_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", branchOffice.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", branchOffice.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", branchOffice.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", branchOffice.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", branchOffice.updated_by);

                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_branch_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Branch Added Successfully", true);
                else
                    return new DataResponse("Branch Already Exists", false);
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
        public async Task<DataResponse> PutBranch(Branch_DTO branchOffice)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", branchOffice.branch_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branch_name", branchOffice.branch_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", branchOffice.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", branchOffice.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", branchOffice.updated_on = DateTime.Now);

                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_branch_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Branch Updated Successfully", true);
                else
                    return new DataResponse("Branch Already Exists", false);
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
        public async Task<DataResponse> DeleteBranch(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", id);

                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_branch_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Branch Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Branch", false);
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
        public async Task<List<Branch_DTO>> GetBranch()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_branch_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<Branch_DTO>(result);
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
