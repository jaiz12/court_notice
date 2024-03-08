using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.Marriage
{
    public class MarriageService : MyDbContext, IMarriageService
    {
        public async Task<DataResponse> Post(MarriageDTO marriage)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_application_date", marriage.application_date);
                _sqlCommand.Add_Parameter_WithValue("prm_application_no", marriage.application_no);
                _sqlCommand.Add_Parameter_WithValue("prm_husband_name", marriage.husband_name);
                _sqlCommand.Add_Parameter_WithValue("prm_husband_address", marriage.husband_address);
                _sqlCommand.Add_Parameter_WithValue("prm_wife_name", marriage.wife_name);
                _sqlCommand.Add_Parameter_WithValue("prm_wife_address", marriage.wife_address);
                _sqlCommand.Add_Parameter_WithValue("prm_husband_photo", marriage.husbandfilepath);
                _sqlCommand.Add_Parameter_WithValue("prm_wife_photo", marriage.wifefilepath);
                _sqlCommand.Add_Parameter_WithValue("prm_marriage_certificate_photo", marriage.marriage_certificate_path);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_from", marriage.effective_from);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_to", marriage.effective_to);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", marriage.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", marriage.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_marriage_registration", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Registered Successfully", true);
                else
                    return new DataResponse("Application No Already Exists", false);
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

        public async Task<DataTable> Get()
        {
            try
            {
                OpenContext();
                DataTable result = _sqlCommand.Select_Table("get_marriage_details", CommandType.StoredProcedure);
                return result;
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

        public async Task<DataResponse> Delete(long marriage_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_marriage_id", marriage_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_marriage_details", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Marriage Data Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Marriage Data", false);
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
