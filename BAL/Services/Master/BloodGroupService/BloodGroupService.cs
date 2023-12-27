using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Master.BloodGroupService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 01-Nov-2023
    // =============================================
    public class BloodGroupService : MyDbContext, IBloodGroupService
    {
        public async Task<DataResponse> PostBloodGroup(BloodGroup_DTO bloodGroupList)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_blood_group_name", Regex.Replace(bloodGroupList.blood_group_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", bloodGroupList.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", bloodGroupList.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_blood_group_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Blood Group Added Successfully", true);
                else
                    return new DataResponse("Blood Group Already Exists", false);
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
        public async Task<DataResponse> PutBloodGroup(BloodGroup_DTO bloodGroupList)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_blood_group_id", bloodGroupList.blood_group_id);
                _sqlCommand.Add_Parameter_WithValue("prm_blood_group_name", Regex.Replace(bloodGroupList.blood_group_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_by", bloodGroupList.updated_by);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_blood_group_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Blood Group Updated Successfully", true);
                else
                    return new DataResponse("Blood Group Already Exists", false);
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
        public async Task<List<BloodGroup_DTO>> GetBloodGroup()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_blood_group_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<BloodGroup_DTO>(result);
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
        public async Task<DataResponse> DeleteBloodGroup(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_blood_group_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_blood_group_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Blood Group Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Blood Group", false);
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
