using Common.DbContext;
using Common.Utilities;
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
    }
}
