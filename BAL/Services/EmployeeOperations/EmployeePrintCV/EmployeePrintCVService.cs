using BAL.Services.Common;
using BAL.Services.EmployeeOperations.EmployeePrintCV.Utility;
using Common.Utilities;
using DinkToPdf;
using DinkToPdf.Contracts;
using DTO.Models.Common;
using DTO.Models.EmployeeOperation.PrintCV;
using DTO.Models.EmployeeOperation.PrintCV.Resume;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeePrintCV
{
    internal class EmployeePrintCVService : IEmployeePrintCVService
    {
        private readonly IConverter _converter;


        //declaring gloabl settings for generating pdfs
        readonly GlobalSettings globalSettings = new GlobalSettings
        {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Portrait,
            PaperSize = PaperKind.A4,
            Margins = new MarginSettings { Top = 5, Left = 5, Right = 5 },
            DocumentTitle = "Employee Resume",
            // Use "Out" if you want to store the PDF file in Physical Storage.
        };



        ICommonService _commonService;

        public EmployeePrintCVService(ICommonService commonService, IConverter converter)
        {
            _commonService = commonService;
            _converter = converter;
        }

        public async Task<dynamic> GetEmployeeListForPrintCV(PaginationEntityDTO pagination = null)
        {
            DataSet ds = await _commonService.GetDataSetByIdAsync("emp_get_employee_list_for_print_cv_v2", null, null, pagination);

            //int TotalRows = (int)ds.Tables[0].Rows[0]["TotalRows"];
            List<EmployeeDetailsForPrintCVDTO> EmployeeList = DataTableVsListOfType.ConvertDataTableToList<EmployeeDetailsForPrintCVDTO>(ds.Tables[0]);

            var item = new
            {
                //totalRows = TotalRows,
                employeeList = EmployeeList
            };

            return item;

        }


        public async Task<byte[]> GenerateEmployeeResumePdf(EmployeeDetailsForPrintCVDTO employee)
        {
            try
            {

                //get personal information
                EmployeeCVPersonalInformationDTO personalInformation = (EmployeeCVPersonalInformationDTO)await _commonService.GetByIdAsync<EmployeeCVPersonalInformationDTO>("emp_get_resume_personal_information_by_employeeid", "employee_id", employee.employee_id);

                //get qualification list
                List<EmployeeCVQualificationDTO> qualificationInformation = await _commonService.GetListByIdAsync<EmployeeCVQualificationDTO>("emp_get_resume_qualification_list_by_employeeid", "employee_id", employee.employee_id);



                //Object settins for generating particular pdf with consuemr bill details
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    //generating HTML content from here
                    HtmlContent = TemplateGenerator.GetHTMLString(personalInformation, qualificationInformation),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "printcv", "css", "defaultFormat.css") },
                    //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" },
                };
                //here we are making HTML to PDF document
                var pdf = new HtmlToPdfDocument
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                //Converting generated pdf to a byte array which will sent to UI for downlaoding
                var file = _converter.Convert(pdf);
                return file;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> GenerateEmployeesResumeZip(List<EmployeeDetailsForPrintCVDTO> employeeList)
        {
            Stream memoryStreamForZipFile = null;
            try
            {
                ZipFile zipFile = new ZipFile();
                //looping on grouped bill details 
                foreach (var employee in employeeList)
                {
                    //calling generate bills pdf function to create pdf
                    var file = await GenerateEmployeeResumePdf(employee);

                    //adding a memory stream with the byte array 
                    memoryStreamForZipFile = new MemoryStream(file);
                    memoryStreamForZipFile.Seek(0, SeekOrigin.Begin);
                    zipFile.AddEntry($"{employee.employee_name}_CV_{DateTime.Now:dd_MM_yyyy}_{employee.employee_service_id}.pdf", memoryStreamForZipFile);
                }
                var zipMemoryStream = new MemoryStream();
                //saving the zip file into a zip memory stream 
                zipFile.Save(zipMemoryStream);
                //converting the zip into a byte array and retrning to caller
                return zipMemoryStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
