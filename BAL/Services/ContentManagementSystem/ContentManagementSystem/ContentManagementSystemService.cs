using Common.DbContext;
using DTO.Models.ContentManagementSystem;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Common.Utilities;
using System.Diagnostics;

namespace BAL.Services.ContentManagementSystem.ContentManagementSystem
{
    public class ContentManagementSystemService : MyDbContext, IContentManagementSystemService
    {
        public async Task<DataResponse> PostCMSDetails(ContentManagementSystem_DTO cmsDetails)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_cms_title", cmsDetails.cms_title.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_cms_description", cmsDetails.cms_description.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", cmsDetails.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", cmsDetails.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", cmsDetails.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", cmsDetails.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_post_content_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("CMS Details Added Successfully", true);
                }
                else
                {
                    return new DataResponse("CMS Details Already Exists", false);
                }
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
        public async Task<DataResponse> PutCMSDetails(ContentManagementSystem_DTO cmsDetails)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_cms_id", cmsDetails.cms_id);
                _sqlCommand.Add_Parameter_WithValue("prm_cms_title", cmsDetails.cms_title.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_cms_description", cmsDetails.cms_description.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", cmsDetails.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", cmsDetails.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", cmsDetails.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", cmsDetails.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_update_content_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("CMS Details Updated Successfully", true);
                }
                else
                {
                    return new DataResponse("CMS Details Already Exists", false);
                }
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
        public async Task<DataResponse> DeleteCMSDetails(long cms_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_cms_id", cms_id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_delete_content_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("CMS Details Delete Successfully", true);
                }
                else
                {
                    return new DataResponse("Failed To Delete CMS Details", false);
                }
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
        public async Task<DataTable> GetCMSDetails()
        {
            try
            {
                OpenContext();
                DataTable result = _sqlCommand.Select_Table("cms_get_content_management_system_details", CommandType.StoredProcedure);
                return result;
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
        public async Task<DataTable> GetCMSDetailsOnLoad()
        {
            try
            {
                OpenContext();
                DataTable cmsDetailsDT = _sqlCommand.Select_Table("cms_getonload_content_management_system_details", CommandType.StoredProcedure);
                return cmsDetailsDT;
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
