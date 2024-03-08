using BAL.Services.Auth;
using Common.DbContext;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.View
{
    internal class ViewService : MyDbContext, IViewService
    {
        public async Task<DataTable> GetCountOfView()
        {

            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("get_count_of_registration", CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { CloseContext(); }
        }
        public async Task<DataTable> GetMarriageRegisteredDetails()
        {

            try
            {
                OpenContext();
                var result = _sqlCommand.Select_Table("get_marriage_registration_view", CommandType.StoredProcedure);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { CloseContext(); }
        }

        public async Task<object> GetLandRegisteredDetails()
        {
            try
            {
                OpenContext();
                DataTable landRegistrationDetails = _sqlCommand.Select_Table("Select * from tbl_LandRegistration", CommandType.Text);
                DataTable landRegistrationApplicantDetails = _sqlCommand.Select_Table("Select * from tbl_LandRegistration_applicant", CommandType.Text);
                List<LandDTO> landDTO = new List<LandDTO>();
                for (int i = 0; i < landRegistrationDetails.Rows.Count; i++)
                {
                    LandDTO land = new LandDTO();
                    land.application_date = Convert.ToDateTime(landRegistrationDetails.Rows[i]["application_date"].ToString());
                    land.effective_from = Convert.ToDateTime(landRegistrationDetails.Rows[i]["effective_from"].ToString());
                    land.effective_to = Convert.ToDateTime(landRegistrationDetails.Rows[i]["effective_to"].ToString());
                    land.parcha_no = landRegistrationDetails.Rows[i]["parcha_no"].ToString();
                    land.application_number = landRegistrationDetails.Rows[i]["application_number"].ToString();
                    land.land_address = landRegistrationDetails.Rows[i]["land_address"].ToString();
                    land.land_certificate = landRegistrationDetails.Rows[i]["land_certificate"].ToString();
                    land.created_on = Convert.ToDateTime(landRegistrationDetails.Rows[i]["created_on"].ToString());
                    land.updated_on = Convert.ToDateTime(landRegistrationDetails.Rows[i]["updated_on"].ToString());
                    List<ApplicantDTO> applicantDTO = new List<ApplicantDTO>();
                    for (int j = 0; j < landRegistrationApplicantDetails.Rows.Count; j++)
                    {
                        ApplicantDTO applicant = new ApplicantDTO();
                        if (land.application_number == landRegistrationApplicantDetails.Rows[j]["application_number"].ToString())
                        {
                            applicant.applicant_name = landRegistrationApplicantDetails.Rows[j]["applicant_name"].ToString();
                            applicant.applicant_address = landRegistrationApplicantDetails.Rows[j]["applicant_address"].ToString();
                            applicant.identity_type = landRegistrationApplicantDetails.Rows[j]["identity_type"].ToString();
                            applicant.identity_number = landRegistrationApplicantDetails.Rows[j]["identity_number"].ToString();
                            applicant.identity_photo = landRegistrationApplicantDetails.Rows[j]["identity_photo"].ToString();

                            applicantDTO.Add(applicant);
                        }

                    }
                    land.applicants = applicantDTO;
                    landDTO.Add(land);
                }
                return landDTO;
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
