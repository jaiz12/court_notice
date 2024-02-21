using Common.Configuration;
using Common.Core;
using Common.DbContext;
using DTO.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Master.Common
{
    internal class MasterCommonService : MyDbContext, IMasterCommonService
    {
        private readonly DatabaseLockConfiguration _lockConfig;
        public MasterCommonService(DatabaseLockConfiguration databaseLockConfiguration)
        {
            _lockConfig = databaseLockConfiguration;
        }

        public DataTable Get(string spName)
        {
            try
            {
                OpenContext();
                return _sqlCommand.Select_Table(spName, CommandType.StoredProcedure);
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


        public DataTable GetDataTableByQuery(string query)
        {
            lock (_lockConfig.connectionStringLockObj)
            {
                using (SqlConnection connection = new SqlConnection(_lockConfig.GetConnectionString()))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable result = new DataTable();
                                adapter.Fill(result);
                                return result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it appropriately.
                        throw new Exception(ex.Message);
                    }
                }
            }
        }



        public DataResponse PostOrUpdate(string spName, string? columnNameInitial, dynamic item, bool isUpdate)
        {
            lock (_lockConfig.connectionStringLockObj)
            {
                using SqlConnection connection = new SqlConnection(_lockConfig.GetConnectionString());
                try
                {
                    connection.Open();
                    using SqlCommand command = new SqlCommand(spName, connection);
                    command.Parameters.Clear();
                    command.CommandType = CommandType.StoredProcedure;

                    // add id if its an update operation
                    if (isUpdate == true)
                    {
                        string colName = $"{columnNameInitial}_id";
                        command.Parameters.AddWithValue(RemoveWhiteSpace($"@prm_{colName}"), getPropertyValue(colName, item));
                        command.Parameters.AddWithValue("@prm_updated_by", item.updated_by);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@prm_created_by", item.updated_by);
                    }

                    command.Parameters.AddWithValue(RemoveWhiteSpace($"@prm_{columnNameInitial}_name"), getPropertyValue($"{columnNameInitial}_name", item));

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                        return new DataResponse($"{ConvertToPascalCase(columnNameInitial) + (isUpdate ? " Updated" : " Added") + " Successfully"}", true);
                    else
                        return new DataResponse($"{ConvertToPascalCase(columnNameInitial) + " Already Exists"}", false);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private object getPropertyValue(string initialName, dynamic item)
        {

            if (initialName.Length > 0)
            {
                // Get the property name
                string propertyName = RemoveWhiteSpace(initialName); // Replace "name" with the dynamic property name

                // Get the property value
                PropertyInfo propertyInfo = item.GetType().GetProperty(propertyName);
                object value = propertyInfo.GetValue(item);

                //Trimmed String Value (Added by Sandeep Chauhan)
                if (value is string stringValue)
                {
                    string trimmedValue = Regex.Replace(stringValue.Trim(), @"\s+", " ");
                    propertyInfo.SetValue(item, trimmedValue); // Update the property value with the trimmed string
                    return trimmedValue; // Return the trimmed value
                }

                return value;
            }
            else
            {
                return null;
            }
        }

        public DataResponse Archive(string spName, dynamic item)
        {
            lock (_lockConfig.connectionStringLockObj)
            {
                using SqlConnection connection = new SqlConnection(_lockConfig.GetConnectionString());
                try
                {
                    connection.Open();
                    using SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@prm_id", item.id);
                    command.Parameters.AddWithValue("@prm_updated_by", item.updated_by);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                        return new DataResponse("Successfully Archived!", true);
                    else
                        return new DataResponse("Failed to Archive!", true);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<DataResponse> Delete(string spName, string columnNameInitial, long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue($"prm_{columnNameInitial}_id", id);
                bool isDeleted = await Task.Run(() => _sqlCommand.Execute_Query($"{spName}", CommandType.StoredProcedure));
                if (isDeleted)
                    return new DataResponse("Successfully Deleted!", true);
                else
                    return new DataResponse($"Failed to Delete!", false);
            }
            catch (CustomException)
            {
                throw new CustomException($"Failed to Delete!.");
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

        public static string RemoveWhiteSpace(string input)
        {
            return input.Replace(" ", "");
        }

        static string ConvertToPascalCase(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(input).Replace("_", "");
        }

    }
}
