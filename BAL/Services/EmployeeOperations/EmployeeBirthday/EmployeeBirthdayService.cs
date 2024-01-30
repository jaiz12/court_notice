using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.BirthdayWishesDTO;
using DTO.Models.Employee;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeBirthday
{
    public class EmployeeBirthdayService: MyDbContext , IEmployeeBirthdayService
    {
        public async Task<List<EmployeeBirthday_DTO>> GetBirthday()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable birthdayDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_employee_birthday", CommandType.StoredProcedure);
                List<EmployeeBirthday_DTO> birthdayList = new List<EmployeeBirthday_DTO>();
                birthdayList = DataTableVsListOfType.ConvertDataTableToList<EmployeeBirthday_DTO>(birthdayDT);
                return birthdayList;
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

        public async Task<DataTable> GetBirthdayComment(string employee_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", employee_id);

                DataTable commentDT = await Task.Run(() => _sqlCommand.Select_Table("emp_get_birthday_comment", CommandType.StoredProcedure));

                return commentDT;
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

        public async Task<DataResponse> Post(EmployeeBirthday_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();                
                _sqlCommand.Add_Parameter_WithValue("prm_comment", data.comment);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);

                if (data.birthday_comment_id == 0)
                {
                    _sqlCommand.Add_Parameter_WithValue("prm_employee_id", data.employee_id);
                    _sqlCommand.Add_Parameter_WithValue("prm_comment_employee_id", data.comment_employee_id);
                    var insert = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_birthday_comment", CommandType.StoredProcedure));
                    if (insert)
                        return new DataResponse("Comment Added Successfully", true);
                    else
                        return new DataResponse("Comment Already Exists", false);
                }
                else
                {
                    _sqlCommand.Add_Parameter_WithValue("prm_birthday_comment_id", data.birthday_comment_id);
                   
                    var update = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_birthday_comment", CommandType.StoredProcedure));
                    if (update)
                        return new DataResponse("Comment Updated Successfully", true);
                    else
                        return new DataResponse("Comment Already Exists", false);
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

        public async Task<DataResponse> Delete(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_birthday_comment_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_birthday_comment", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Comment Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Comment", false);
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


        public async Task<List<BirthdayListForRealTimeDTO>> GetBirthdayListForRealTimeChat()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable birthdayDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_employee_birthday_for_real_time_chat", CommandType.StoredProcedure);
                List<BirthdayListForRealTimeDTO> birthdayList = new List<BirthdayListForRealTimeDTO>();
                birthdayList = DataTableVsListOfType.ConvertDataTableToList<BirthdayListForRealTimeDTO>(birthdayDT);
                return birthdayList;
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

        public async Task<List<EmployeeBirthday_DTO>> GetBirthdayCommentByBirthdayPersonIdAndTimestamp(string employee_id, DateTime? timestamp)
        {

            try
            {

                BirthdayWishesDTO obj = new BirthdayWishesDTO {
                    employee_id = employee_id,
                    birthday_comment_id = null,
                    comment = null,
                    created_on = timestamp,
                    comment_employee_id = null
                };

                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable birthdayDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_employee_birthday_wishes_by_birthday_person_id_and_timestamp", obj, "prm_");
                List<EmployeeBirthday_DTO> birthdayList = new List<EmployeeBirthday_DTO>();


                birthdayList = DataTableVsListOfType.ConvertDataTableToList<EmployeeBirthday_DTO>(birthdayDT);
                return birthdayList;
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
