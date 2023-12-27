using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Master.TrainingTypeService
{
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 03-Nov-2023
    // =============================================
    public class TrainingTypeService : MyDbContext, ITrainingTypeService
    {
        public async Task<DataResponse> PostTrainingType(TrainingType_DTO trainingType)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_training_type_name", Regex.Replace(trainingType.training_type_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", trainingType.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", trainingType.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_training_type_master", CommandType.StoredProcedure));

                if (item)
                    //return "Traning type data inserted successfully";
                    return new DataResponse("Training Type Added Successfully", true);

                else
                    //return "Duplicate traning type data insertion does not support!!!";
                    return new DataResponse("Training Type Already Exists", false);
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
        public async Task<DataResponse> PutTrainingType(TrainingType_DTO trainingType)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_training_type_id", trainingType.training_type_id);
                _sqlCommand.Add_Parameter_WithValue("@prm_training_type_name", Regex.Replace(trainingType.training_type_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_by", trainingType.updated_by);
                _sqlCommand.Add_Parameter_WithValue("@prm_updated_on", DateTime.Now);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_training_type_master", CommandType.StoredProcedure));

                if (result)
                    //return "Training type data updated successfully";
                    return new DataResponse("Training Type Updated Successfully", true);
                else
                    //return "Duplicate training type data updation does not support!!!";
                    return new DataResponse("Training Type Already Exists", false);
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
        public async Task<List<TrainingType_DTO>> GetTrainingType()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("emp_get_training_type_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<TrainingType_DTO>(result);
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
        public async Task<DataResponse> DeleteTrainingType(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_training_type_id", id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_training_type_master", CommandType.StoredProcedure));
                if (result)
                    //return "Training type data archived successfully";
                    return new DataResponse("Training Type Deleted Successfully", true);
                else
                    //return "Duplicate training type data archivation does not support!!!";
                    return new DataResponse("Failed To Delete Training Type", false);
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
