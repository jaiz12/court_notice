using Common.DbContext;
using DTO.Models.ContentManagementSystem;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BAL.Services.ContentManagementSystem.ContentMessageManagementSystem
{
    public class ContentMessageManagementSystemService: MyDbContext, IContentMessageManagementSystemService
    {
        public async Task<DataResponse> PostCmsMessage(ContentMessageManagementSystem_DTO message)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_message", message.message);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", message.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", message.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", message.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", message.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_post_content_message_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("Message Added Successfully", true);
                }
                else
                {
                    return new DataResponse("Message Already Exists", false);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataResponse> PutCmsMessage(ContentMessageManagementSystem_DTO message)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_message_id", message.message_id);
                _sqlCommand.Add_Parameter_WithValue("prm_message", message.message);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", message.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", message.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_update_content_message_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("Message Updated Successfully", true);
                }
                else
                {
                    return new DataResponse("Message Already Exists", false);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataResponse> DeleteCmsMessage(long message_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_message_id", message_id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("cms_delete_content_message_management_system_details", CommandType.StoredProcedure));
                if (item)
                {
                    return new DataResponse("Message Deleted Successfully", true);
                }
                else
                {
                    return new DataResponse("Failed To Delete Message", false);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataTable> GetCmsMessage()
        {
            try
            {
                OpenContext();

                DataTable messageDT = await Task.Run(() => _sqlCommand.Select_Table("cms_get_content_message_management_system_details", CommandType.StoredProcedure));
                return messageDT;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataTable> GetCmsMessageForLogin()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DateTime currentDate = DateTime.Now;
                _sqlCommand.Add_Parameter_WithValue("prm_currentDate", Convert.ToDateTime(currentDate).ToString("yyyy-MM-dd"));
                DataTable messageForLoginDT = await Task.Run(()=> _sqlCommand.Select_Table("cms_getforlogin_content_message_management_system_details", CommandType.StoredProcedure));
                return messageForLoginDT;
            }
            catch(Exception ex)
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
