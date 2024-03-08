using Common.DbContext;
using DTO.Models;
using DTO.Models.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Land
{
    public class LandService : MyDbContext, ILandService
    {

        public async Task<DataResponse> Post(LandDTO landDTO)
        {
            System.Data.SqlClient.SqlTransaction Trans = null;
            try
            {
                OpenContext();
                Trans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
                _sqlCommand.Add_Transaction(Trans);
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_application_date", landDTO.application_date);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_from", landDTO.effective_from);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_to", landDTO.effective_to);
                _sqlCommand.Add_Parameter_WithValue("prm_parcha_no", landDTO.parcha_no);
                _sqlCommand.Add_Parameter_WithValue("prm_application_number", landDTO.application_number);
                _sqlCommand.Add_Parameter_WithValue("prm_land_address", landDTO.land_address);
                _sqlCommand.Add_Parameter_WithValue("prm_land_certificate", landDTO.certificatefilepath);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", landDTO.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", landDTO.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_land_registration", CommandType.StoredProcedure));
                bool final = false;
                foreach (var applicant in landDTO.applicants)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_application_number", landDTO.application_number);
                    _sqlCommand.Add_Parameter_WithValue("prm_applicant_name", applicant.applicant_name);
                    _sqlCommand.Add_Parameter_WithValue("prm_applicant_address", applicant.applicant_address);
                    _sqlCommand.Add_Parameter_WithValue("prm_identity_type", applicant.identity_type);
                    _sqlCommand.Add_Parameter_WithValue("prm_identity_number", applicant.identity_number);
                    _sqlCommand.Add_Parameter_WithValue("prm_identity_photo", applicant.filepath);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_by", landDTO.created_by);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_by", landDTO.updated_by);
                    final = await Task.Run(() => _sqlCommand.Execute_Query("post_land_registration_applicant", CommandType.StoredProcedure));
                   
                }



                Trans.Commit();
                if (final)
                    return new DataResponse("Registered Successfully", true);
                else
                    return new DataResponse("Application No Already Exists", false);
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<object> Get()
        {
            try
            {
                OpenContext();
                DataTable landRegistrationDetails = _sqlCommand.Select_Table("Select * from tbl_LandRegistration", CommandType.Text);
                DataTable landRegistrationApplicantDetails = _sqlCommand.Select_Table("Select * from tbl_LandRegistration_applicant", CommandType.Text);
                List<LandDTO> landDTO = new List<LandDTO>();
                for (int i = 0;i< landRegistrationDetails.Rows.Count;i++)
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
                        if(land.application_number == landRegistrationApplicantDetails.Rows[j]["application_number"].ToString())
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

        public async Task<DataResponse> Delete(long application_number)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_application_number", application_number);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_land_details", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Land Data Deleted Successfully", true);
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
