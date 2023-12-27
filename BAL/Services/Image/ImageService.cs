using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Image
{
    public class ImageService : MyDbContext, IImageService
    {
        public async Task<DataResponse> PostAsync(Image_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_filepath", data.filepath);
                _sqlCommand.Add_Parameter_WithValue("prm_filename", data.filename);
                _sqlCommand.Add_Parameter_WithValue("prm_UserName", data.UserName);
                var _spName = "album_update";
                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_file", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Role Company Permission Added Successfully", true);
                else
                    return new DataResponse("Role Company Permission Already Exists", false);
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

        public async Task<DataResponse> PutAsync(int? id, Image_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_Id", data.Id);
                _sqlCommand.Add_Parameter_WithValue("prm_filepath", data.filepath);
                _sqlCommand.Add_Parameter_WithValue("prm_filename", data.filename);
                var _spName = "album_update";
                var item = await Task.Run(() => _sqlCommand.Execute_Query("put_file", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Role Company Permission Added Successfully", true);
                else
                    return new DataResponse("Role Company Permission Already Exists", false);
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

        public async Task<List<Image_DTO>> GetAsync()
        {
            try
            {
                
                OpenContext();
                var Data = await Task.Run(() => _sqlCommand.Select_Table("get_file", CommandType.StoredProcedure));
                var images = DataTableVsListOfType.ConvertDataTableToList<Image_DTO>(Data);
                return images;

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
