﻿using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Customization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Customization.DashboardSkinService
{
    public class DashboardSkinService : MyDbContext, IDashboardSkinService
    {
        public async Task<DataResponse> PostOrUpdateDashboardSkin(List<DashboardSkin_DTO> _dashboardSkin)
        {
            try
            {
                OpenContext();
                List<DashboardSkin_DTO> noOfinsertSuccess = new List<DashboardSkin_DTO>();
                var item = false;
                foreach (DashboardSkin_DTO skin in _dashboardSkin)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_section_name", skin.section_name);
                    _sqlCommand.Add_Parameter_WithValue("prm_company_name", skin.company_name);
                    _sqlCommand.Add_Parameter_WithValue("prm_background_color", skin.background_color);
                    _sqlCommand.Add_Parameter_WithValue("prm_font_color", skin.font_color);
                    _sqlCommand.Add_Parameter_WithValue("prm_font_size", skin.font_size);
                    _sqlCommand.Add_Parameter_WithValue("prm_image_height", skin.image_height);
                    _sqlCommand.Add_Parameter_WithValue("prm_image_width", skin.image_width);
                    _sqlCommand.Add_Parameter_WithValue("prm_text_alignment", skin.text_alignment);
                    _sqlCommand.Add_Parameter_WithValue("prm_font_scheme", skin.font_scheme);

                    _sqlCommand.Add_Parameter_WithValue("prm_created_on", skin.created_on = DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_by", skin.created_by);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_on", skin.updated_on = DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_by", skin.updated_by);

                    item = await Task.Run(() => _sqlCommand.Execute_Query("skin_post_or_update_customize_dashboard", CommandType.StoredProcedure));

                    noOfinsertSuccess.Add(skin);
                }
                if (item)
                {
                    return new DataResponse("Theme Updated Successfully", true);
                }
                else
                {
                    return new DataResponse($"No Changes Found", false);
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
        public async Task<List<DashboardSkin_DTO>> GetDashboardSkin()
        {
            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("skin_get_customize_dashboard", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<DashboardSkin_DTO>(result);
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
