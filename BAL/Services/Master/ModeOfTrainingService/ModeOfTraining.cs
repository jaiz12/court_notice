using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.ModeOfTrainingService
{
    public class ModeOfTraining : MyDbContext, IModeOfTraining
    {
        public async Task<DataResponse> Post(Mode_of_training_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_mode_of_training_name", data.mode_of_training_name);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", data.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_mode_of_training_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Mode of Training Added Succesfully", true);
                else
                    return new DataResponse("Duplicate Mode of Training Added", false);
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

        public async Task<List<Mode_of_training_DTO>> GetModeOfTraining()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable trainingDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_mode_of_training_master", CommandType.StoredProcedure);
                List<Mode_of_training_DTO> trainingList = new List<Mode_of_training_DTO>();
                trainingList = DataTableVsListOfType.ConvertDataTableToList<Mode_of_training_DTO>(trainingDT);
                return trainingList;
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

        public async Task<DataResponse> Update(Mode_of_training_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_mode_of_training_id", data.mode_of_training_id);
                _sqlCommand.Add_Parameter_WithValue("prm_mode_of_training_name", data.mode_of_training_name);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_mode_of_training_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Mode of Training Updated successfully", true);
                else
                    return new DataResponse("Mode of Training Update failed", false);
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

        public async Task<DataResponse> Delete(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_mode_of_training_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_mode_of_training_master", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Mode of Training Deleted Successfully", true);
                else
                    return new DataResponse("Mode of Training Delete Failed", false);
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
