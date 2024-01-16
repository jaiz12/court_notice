using BAL.Services.Master.Common;
using Common.DbContext;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.CompanyService
{
    internal class CompanyService : MyDbContext, ICompanyService
    {
        private IMasterCommonService _commonService;
        public CompanyService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public async Task<DataTable> GetCompany()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_company_master", CommandType.StoredProcedure));
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

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public DataResponse AddCompany(Company_DTO model)
        {
            return _commonService.PostOrUpdate("emp_post_company_master", "company", model, false);

        }

        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public DataResponse EditCompany(Company_DTO model)
        {
            return _commonService.PostOrUpdate("emp_update_company_master", "company", model, true);

        }


        /// Author : Sandeep Chauhan
        /// Date : 02-11-2023
        public async Task<DataResponse> DeleteCompany(long id)
        {
            return await _commonService.Delete("emp_delete_company_master", "company", id);
        }

        public DataTable GetAllCompanyLogoUrl()
        {
            string query = @"
                            SELECT CM.[company_name]
                            ,CM.[company_logo_url]
	                        ,CDS.background_color
                            FROM [emp_company_master] CM
                            LEFT JOIN 
                            [customize_dashboard_skin] CDS
                            ON CM.company_name = CDS.company_name
                            WHERE CDS.section_name = 'Brand_logo' OR CDS.section_name IS NULL";

            return _commonService.GetDataTableByQuery(query);
        }

    }
}
