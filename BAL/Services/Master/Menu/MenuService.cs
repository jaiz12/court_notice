using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.Menu
{
    public class MenuService : MyDbContext, IMenuService
    {
        public async Task<DataResponse> PostMenu(Menu_DTO menu)
        {
            try
            {
                OpenContext();
                var menuData = $"Select * from menus_master where MenuName='{menu.MenuName}' and ParentId = '{menu.ParentId}'";
                DataTable menuExist = await Task.Run(() => _sqlCommand.Select_Table(menuData, CommandType.Text));
                if (menuExist.Rows.Count == 0)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_menuType", menu.MenuType);
                    _sqlCommand.Add_Parameter_WithValue("prm_menuName", menu.MenuName);
                    _sqlCommand.Add_Parameter_WithValue("prm_parentId", menu.ParentId);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_by", menu.CreatedBy);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_by", menu.UpdatedBy);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                    var item = await Task.Run(() => _sqlCommand.Execute_Query("post_menu_master", CommandType.StoredProcedure));

                    if (item)
                        return new DataResponse("Menu Added Successfully", true);
                    else
                        return new DataResponse("Menu Already Exists", false);
                }
                else
                {
                    return new DataResponse("Menu Already Exists", false);
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

        public async Task<DataResponse> Update(Menu_DTO menu)
        {
            try
            {
                OpenContext();
                var menuData = $"Select * from menus_master where MenuName='{menu.MenuName}' and ParentId = '{menu.ParentId}'";
                DataTable menuExist = await Task.Run(() => _sqlCommand.Select_Table(menuData, CommandType.Text));
                string message = "";
                bool status = false;
                if (menuExist.Rows.Count > 0)
                {
                    if (menuExist.Rows[0]["MenuName"].ToString() == menu.MenuName && Convert.ToInt32(menuExist.Rows[0]["ParentId"]) == menu.ParentId)
                    {
                        message = "Menu Already Exist";
                        status = false;
                    }
                }
                else
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_id", menu.Id);
                    _sqlCommand.Add_Parameter_WithValue("prm_menuType", menu.MenuType);
                    _sqlCommand.Add_Parameter_WithValue("prm_menuName", menu.MenuName);
                    _sqlCommand.Add_Parameter_WithValue("prm_parentId", menu.ParentId);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_by", menu.UpdatedBy);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                    var item = await Task.Run(() => _sqlCommand.Execute_Query("update_menu_master", CommandType.StoredProcedure));

                    if (item)
                    {

                        message = "Menu successfully Updated";
                        status = true;
                    }
                       
                    else
                    {
                        message = "Menu Update failed";
                        status = false;
                    }
                }
                return new DataResponse(message, status);
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

        public async Task<List<Menu_DTO>> GetMenus()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable menuDT = await _sqlCommand.ExecuteStoredProcedureAsync("get_menu_master", CommandType.StoredProcedure);
                List<Menu_DTO> menuList = new List<Menu_DTO>();
                menuList = DataTableVsListOfType.ConvertDataTableToList<Menu_DTO>(menuDT);
                return menuList;
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
                _sqlCommand.Add_Parameter_WithValue("@prm_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("delete_menu_master", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Menu Deleted Successfully", true);
                else
                    return new DataResponse("Menu Delete Failed", false);
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
