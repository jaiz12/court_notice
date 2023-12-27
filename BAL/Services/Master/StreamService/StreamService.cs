using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.StreamService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 02-Nov-2023
    // =============================================
    public class StreamService : MyDbContext, IStreamService
    {
        public async Task<DataResponse> PostStream(Stream_DTO stream)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_stream_name", stream.stream_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", stream.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", stream.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_stream_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Stream Added Successfully", true);
                else
                    return new DataResponse("Stream Already Exists", false);
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
        public async Task<DataResponse> PutStream(Stream_DTO stream)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_stream_id", stream.stream_id);
                _sqlCommand.Add_Parameter_WithValue("prm_stream_name", stream.stream_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", stream.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_stream_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Stream Updated Successfully", true);
                else
                    return new DataResponse("Stream Already Exists", false);
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
        public async Task<List<Stream_DTO>> GetStream()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_stream_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<Stream_DTO>(result);
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
        public async Task<DataResponse> DeleteStream(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_stream_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_stream_master", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("Stream Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Stream", false);
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
