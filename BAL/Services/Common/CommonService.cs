using Common.Configuration;
using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using System.Collections.Generic;
using DTO.Models.Common;
using DTO.Models.EmployeeOperation;

namespace BAL.Services.Common
{
    internal class CommonService : MyDbContext, ICommonService
    {
        private readonly DatabaseLockConfiguration _lockConfig;
        public CommonService(DatabaseLockConfiguration databaseLockConfiguration)
        {
            _lockConfig = databaseLockConfiguration;
        }

        public dynamic GetListById(string spName, string prmInitial, dynamic id)
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }

                DataTable res = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<dynamic> GetDatasetByIdAsync(string spName, string prmInitial, dynamic id)
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }

                DataSet dt = await Task.Run(() => _sqlCommand.Select_TableSet(spName, CommandType.StoredProcedure));
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }


        public T GetById<T>(string spName, string prmInitial, dynamic id) where T : class
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }

                DataTable dt = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);

                if(dt.Rows.Count > 0)
                {
                    T model = DataTableVsListOfType.ConvertDataTableToModel<T>(dt.Rows[0]);
                    return model;
                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }

        public List<T> GetListById<T>(string spName, string prmInitial, dynamic id) where T : class
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }

                DataTable dt = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    List<T> list = DataTableVsListOfType.ConvertDataTableToList<T>(dt);
                    return list;
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }


        public async Task<List<T>> GetListByIdAsync<T>(string spName, string prmInitial, dynamic id, PaginationEntityDTO pagination = null) where T : class
        {
            try
            {
                OpenContext();
                if (id != null && prmInitial != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }
                else if (pagination != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValues("", pagination);
                }

                DataTable dt = await Task.Run(() => _sqlCommand.Select_Table(spName, CommandType.StoredProcedure));
                if (dt.Rows.Count > 0)
                {
                    List<T> list = DataTableVsListOfType.ConvertDataTableToList<T>(dt);
                    return list;
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }


        public async Task<List<T>> GetListByIdUsingSearchFilterAsync<T>(string spName, string prmInitial, dynamic id, SearchFilterDTO searchFilter = null) where T : class
        {
            try
            {
                OpenContext();
                if (id != null && prmInitial != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }
                else if (searchFilter != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    //_sqlCommand.Add_Parameter_WithValues("", searchFilter);

                    if (searchFilter is object)
                    {
                        foreach (var property in searchFilter.GetType().GetProperties())
                        {
                            var val = property.GetValue(searchFilter, null);

                            if (property.GetValue(searchFilter) != null && val.ToString() != "")
                            {
                                _sqlCommand.Add_Parameter_WithValue(property.Name, property.GetValue(searchFilter));
                            }
                        }
                    }
                }

                DataTable dt = await Task.Run(() => _sqlCommand.Select_Table(spName, CommandType.StoredProcedure));
                if (dt.Rows.Count > 0)
                {
                    List<T> list = DataTableVsListOfType.ConvertDataTableToList<T>(dt);
                    return list;
                }

                return new List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataSet> GetDataSetByIdAsync(string spName, string prmInitial, dynamic id, PaginationEntityDTO pagination = null)
        {
            try
            {
                OpenContext();
                if (id != null && prmInitial != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }
                else if (pagination != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValues("", pagination);
                }

                DataSet ds = await Task.Run(() => _sqlCommand.Select_TableSet(spName, CommandType.StoredProcedure));
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }

                return new DataSet();
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }



        public DataResponse PostOrUpdateEmployeeProfile(string spName, dynamic item, bool isUpdate)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("employee_personal_id", item.employee_personal_id);
                _sqlCommand.Add_Parameter_WithValue("employee_id", item.employee_id);
                _sqlCommand.Add_Parameter_WithValue("first_name", item.first_name);
                _sqlCommand.Add_Parameter_WithValue("middle_name", item.middle_name);
                _sqlCommand.Add_Parameter_WithValue("last_name", item.last_name);
                _sqlCommand.Add_Parameter_WithValue("date_of_birth", item.date_of_birth);
                _sqlCommand.Add_Parameter_WithValue("gender_id", item.gender_id);
                _sqlCommand.Add_Parameter_WithValue("created_by", item.created_by);
                _sqlCommand.Add_Parameter_WithValue("created_on", item.created_on);
                _sqlCommand.Add_Parameter_WithValue("updated_by", item.updated_by);
                _sqlCommand.Add_Parameter_WithValue("updated_on", item.updated_on);
                DataTable dt = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
                ErrorMessageModel error = CheckForError(dt);

                if (error != null)
                {
                    return new DataResponse(error.shortError, false, error.longError);
                }
                else
                {
                    return new DataResponse($"{(isUpdate ? "Update" : "Insert") + " Successfull!"}", true);
                }
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


        public async Task<object> GetByIdAsync<T>(string spName, string prmInitial, dynamic id) where T : class
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }

                DataTable dt = await Task.Run(() => _sqlCommand.Select_Table(spName, CommandType.StoredProcedure));

                if (dt.Rows.Count > 0)
                {
                    T model = DataTableVsListOfType.ConvertDataTableToModel<T>(dt.Rows[0]);
                    return model;
                }

                return new object();
            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }

        //NOTE : Use Specific Styled Stored Procedure to Use Below Code
        public DataResponse PostOrUpdate(string spName, dynamic item, bool isUpdate)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValues("", item);
                DataTable dt = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
                ErrorMessageModel error = CheckForError(dt);

                if (error != null)
                {
                    return new DataResponse(error.shortError, false, error.longError);
                }
                else
                {
                    return new DataResponse($"{(isUpdate ? "Update" : "Insert") + " Successfull!"}", true);
                }
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


        public async Task<DataResponse> PostOrUpdateAsync(string spName, dynamic item, bool isUpdate)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();

                if(item is object)
                {
                    foreach (var property in item.GetType().GetProperties())
                    {
                        if(property.GetValue(item) != null)
                        {
                            _sqlCommand.Add_Parameter_WithValue(property.Name, property.GetValue(item));
                        }
                    }
                } 

                //_sqlCommand.Add_Parameter_WithValues("", item);

                DataTable dt = await Task.Run(() => _sqlCommand.Select_Table(spName, CommandType.StoredProcedure));
                ErrorMessageModel error = CheckForError(dt);

                if (error != null)
                {
                    return new DataResponse(error.shortError, false, error.longError);
                }
                else
                {
                    return new DataResponse($"{(isUpdate ? "Update" : "Insert") + " Successfull!"}", true);
                }
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

        //NOTE : Use Specific Styled Stored Procedure to Use Below Code
        public DataResponse DeleteById(string spName, string prmInitial, dynamic id)
        {
            try
            {
                OpenContext();
                if (id != null)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue(prmInitial, id);
                }
                DataTable dt = _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
                string ErrorMessage = dt.Rows[0]["ErrorMessage"].ToString();

                if (!String.IsNullOrEmpty(ErrorMessage))
                {
                    return new DataResponse(dt.Rows[0]["ErrorMessage"].ToString(), false);
                }
                else
                {
                    return new DataResponse("Deleted Successfully!", true);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error : GET from Database Error, " + ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }


        public class ErrorMessageModel
        {
            public string shortError { get; set; }
            public string longError { get; set; }
        }
        public ErrorMessageModel CheckForError(DataTable dt)
        {
            int errorNumber;
            if (dt.Rows.Count > 0 && dt.Rows[0].Table.Columns.Contains("ErrorNumber") &&
                int.TryParse(dt.Rows[0]["ErrorNumber"].ToString(), out errorNumber))
            {
                if (errorNumber != 0)
                {
                    return new ErrorMessageModel
                    {
                        shortError = ErrorCodes.SqlServerExceptionMsg(errorNumber),
                        longError = dt.Rows[0]["ErrorMessage"].ToString(),
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
