using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.PlaceOfPostingService
{
    public class PlaceOfPostingService : MyDbContext, IPlaceOfPostingService
    {
        public async Task<DataResponse> PostPlaceOfPosting(PlaceOfPosting_DTO _placeOfPosting)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue("prm_state_id", _placeOfPosting.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", _placeOfPosting.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_name", _placeOfPosting.place_of_posting_name);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", _placeOfPosting.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", _placeOfPosting.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", _placeOfPosting.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", _placeOfPosting.updated_on = DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_place_of_posting", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Place Of Posting Added Successfully", true);

                else
                    return new DataResponse("Place Of Posting Already Exists", false);
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
        public async Task<DataResponse> PutPlaceOfPosting(PlaceOfPosting_DTO _placeOfPosting)
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", _placeOfPosting.place_of_posting_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", _placeOfPosting.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", _placeOfPosting.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_name", _placeOfPosting.place_of_posting_name);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", _placeOfPosting.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", _placeOfPosting.updated_on = DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_place_of_posting", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Place Of Posting Updated Successfully", true);

                else
                    return new DataResponse("Place Of Posting Already Exists", false);
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
        public async Task<List<PlaceOfPosting_DTO>> GetPlaceOfPosting()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_place_of_posting", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<PlaceOfPosting_DTO>(result);
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
        public async Task<DataResponse> DeletePlaceOfPosting(long place_of_posting_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", place_of_posting_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_place_of_posting", CommandType.StoredProcedure));
                if (result)
                    //return "Training type data archived successfully";
                    return new DataResponse("Place Of Posting Deleted Successfully", true);
                else
                    //return "Duplicate training type data archivation does not support!!!";
                    return new DataResponse("Failed To Delete Place Of Posting", false);
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
