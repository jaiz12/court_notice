using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.BoardsService
{
    public class BoardsService : MyDbContext, IBoardsService
    {
        public async Task<DataResponse> Post(Boards_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_boards_name", data.boards_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", data.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_boards_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Boards Added Successfully", true);
                else
                    return new DataResponse("Board Already Exists", false);
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

        public async Task<List<Boards_DTO>> GetBoards()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable boardDT = await _sqlCommand.ExecuteStoredProcedureAsync("emp_get_boards_master", CommandType.StoredProcedure);
                List<Boards_DTO> boardList = new List<Boards_DTO>();
                boardList = DataTableVsListOfType.ConvertDataTableToList<Boards_DTO>(boardDT);
                return boardList;
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

        public async Task<DataResponse> Update(Boards_DTO data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_boards_id", data.boards_id);
                _sqlCommand.Add_Parameter_WithValue("prm_boards_name", data.boards_name.Trim());
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", data.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_boards_master", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Boards Updated Successfully", true);
                else
                    return new DataResponse("Board Already Exists", false);
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
                _sqlCommand.Add_Parameter_WithValue("@prm_boards_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_boards_master", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Boards Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Board", false);
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

