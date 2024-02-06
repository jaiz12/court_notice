using DTO.Models.EmployeeOperation.PrintCV.Resume;
using System.Collections.Generic;
using System.Text;

namespace BAL.Services.EmployeeOperations.EmployeePrintCV.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(EmployeeCVPersonalInformationDTO personalInformation, List<EmployeeCVQualificationDTO> qualificationList)
        {
            var sb = new StringBuilder();


            // PERSONAL INFORMATION
            sb.AppendFormat(@$"<html lang='en'>
									<head>
										<meta charset='UTF-8'>
									</head>

									<body>
										<table cellspacing='0'>
											<colgroup width='51'></colgroup>
											<colgroup width='255'></colgroup>
											<colgroup width='221'></colgroup>
											<colgroup width='255'></colgroup>
											<colgroup width='160'></colgroup>
											<colgroup width='188'></colgroup>
											<tr>
												<td class='header'><b></b></td>
												<td class='header' colspan='5'><b>RESUME</b></td>
											</tr>
											<tr>
												<td class='subheader'><b>I.</b></td>
												<td class='subheader' colspan='5'><b>Personal Information</font></b></td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Name</b></td>
												<td class='content' colspan='4'>{personalInformation.employee_name}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Date of Birth</b></td>
												<td class='content' colspan='4'>{personalInformation.date_of_birth}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Gender</b></td>
												<td class='content' colspan='4'>{personalInformation.gender_name}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Mobile number</b></td>
												<td class='content' colspan='4'>{personalInformation.mobile_number}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Current address</b></td>
												<td class='content' colspan='4'>{personalInformation.correspondence_address}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>Permanent address</b></td>
												<td class='content' colspan='4'>{personalInformation.permanent_address}</td>
											</tr>
											<tr>
												<td class='content'></td>
												<td class='content'><b>E-mail ID</b></td>
												<td class='content' colspan='4'>{personalInformation.personal_email_id}</td>
											</tr>
											<tr>
												<td class='subheader'><b>II.</b></td>
												<td class='subheader' colspan='5'><b>Educational Qualification</b></td>
											</tr>
											<td></td>
											<td class='content'><b>Qualification</b></td>
											<td class='content'><b>Course/Subject</b></td>
											<td class='content'><b>School/College</b></td>
											<td class='content'><b>Year of Passing</b></td>
											<td class='content'><b>Percentage / CGPA</b></td>"
            );

            // EDUCATIONAL QUALIFICATION
            // loopable
            int qualificationIndex = 1;
            foreach (EmployeeCVQualificationDTO qualification in qualificationList)
            {
                sb.AppendFormat(@$"<tr>
								<td class='content center'><b>{qualificationIndex}</b></td>
								<td class='content'><b>{qualification.qualification_name}</b></td>
								<td class='content'>{qualification.stream_name}</font></td>
								<td class='content'>{qualification.school_name}</font></td>
								<td class='content'>{qualification.year_of_passing}</font></td>
								<td class='content'>{qualification.percentage}</font></td>
							</tr>"
                );
                qualificationIndex++;
            }

            // ADDITIONAL QUALIFICATION
            sb.AppendFormat(@$"<tr>
									<td class='content'><br></td>
									<td class='content'><b>Any other qualifications (please specify)</b></td>
									<td class='content' colspan='5'>{"  N/A "}</td>
							  </tr>"
            );

            // PROFESSIONAL SUMMARY
            sb.Append(@"<tr>
							<td class='subheader'><b>III.</b></td>
							<td class='subheader'  colspan='5'><b>Professional Summary</b></td>
						</tr>

						<tr>
							<td class='content'><b><br></b></td>
							<td class='content'><b>Name of the employer/company/organization</b></td>
							<td class='content'><b>Position/Title</b></td>
							<td class='content'><b>No of years</b></td>
							<td class='content'><b>Job responsibilites</b></td>
							<td class='content'><b>City/state</b></td>
						</tr>
			");
            // loopable
            sb.AppendFormat(@$"<tr>
									<td class='content center'><b>1</b></td>
									<td class='content'>{"  N/A "}</td>
									<td class='content'>{"  N/A "}</td>
									<td class='content'>{"  N/A "}</td>
									<td class='content'>{"  N/A "}</td>
									<td class='content'>{"  N/A "}</td>
								</tr>
			");

            // HOBBIES and SKILLS
            sb.AppendFormat(@$"<tr>
							<td class='subheader'><b>IV.</b></td>
							<td class='subheader' colspan='5'><b>Hobbies/Interests</b></td>
							</tr>
							<tr>
								<td class='content'><b><br></b></td>
								<td class='content center' colspan='5'>{" N/A "}</td>
							</tr>
							<tr>
								<td class='subheader'><b>V.</b></td>
								<td class='subheader' colspan='5'><b>Skills</b></td>
								</tr>
							<tr>
								<td class='content'><b><br></b></td>
								<td class='content center'  colspan='5'>{" N/A "}</td>
								</tr>
							<tr>
								<td class='subheader'><b>VI.</b></td>
								<td class='subheader' colspan='5'><b>Past employment references</b></td>
								</tr>
							<tr>
								<td class='content'><b><br></b></td>
								<td class='content'><b>Name</b></td>
								<td class='content'><b>Designation </b></td>
								<td class='content'><b>Organisation</b></td>
								<td class='content'><b>Phone number</b></td>
								<td class='content'><b>E-mail id</b></td>
							</tr>
			 ");

            //PAST EMPLOYMENT REFERENCES - loopable
            sb.AppendFormat(@$"<tr>
								<td class='content center'><b>1</b></td>
								<td class='content'>{"  N/A "}</td>
								<td class='content'>{"  N/A "} </td>
								<td class='content'>{"  N/A "}</td>
								<td class='content'>{"  N/A "}</td>
								<td class='content'><u><a href='mailto:loremone@mail.com'>{"  N/A "}</a></u></td>
							</tr>");




            sb.Append(@"		</table>
							</body>
						</html>"
            );

            return sb.ToString();
        }
    }
}
