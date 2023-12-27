using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.SpecializationService
{
    public class Specialization : MyDbContext, ISpecialization
    {
        //== Author: Dewas Rai
        //== Created_Date: 03 November 2023
        public async Task<DataResponse> Post(Specialization_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_specialization_name", data.specialization_name);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", data.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_specialization_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Specialization Added Succesfully", true);
                else
                    return new DataResponse("Duplicate Specialization Added", false);
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

        public async Task<List<Specialization_DTO>> GetSpecialization()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable specializationDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_specialization_master", CommandType.StoredProcedure);
                List<Specialization_DTO> specializationList = new List<Specialization_DTO>();
                specializationList = DataTableVsListOfType.ConvertDataTableToList<Specialization_DTO>(specializationDT);
                return specializationList;
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

        public async Task<DataResponse> Update(Specialization_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_specialization_id", data.specialization_id);
                _sqlCommand.Add_Parameter_WithValue("prm_specialization_name", data.specialization_name);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_specialization_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Specialization Updated successfully", true);
                else
                    return new DataResponse("Specialization Update failed", false);
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
                _sqlCommand.Add_Parameter_WithValue("@prm_specialization_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_specialization_master", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Specialization Deleted Successfully", true);
                else
                    return new DataResponse("Specialization Delete Failed", false);
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

