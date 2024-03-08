using BAL.Services.Land;
using BAL.Services.Marriage;
using DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
    public class LandController : Controller
    {
        private readonly ILandService _landService;
        public LandController(ILandService landService)
        {
            _landService = landService;
        }

        [HttpPost("PostLand")]
        public async Task<IActionResult> PostLand([FromForm] LandDTO landDTO, IFormFile certificate_files = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var certificate_file = certificate_files;

            try
            {


                landDTO.certificatefilepath = uploadFile("LandCertificate", landDTO.application_number, certificate_file, null);

                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        // Define the allowed file extensions
                        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".webp" };

                        // Get the file extension
                        var ext = Path.GetExtension(file.FileName).ToLower();

                        // Check if the file extension is allowed
                        if (allowedExtensions.Contains(ext))
                        {
                            var filename = Path.GetFileNameWithoutExtension(file.FileName);
                            var folderName = Path.Combine("assets", "Document_Identity");
                            var filePath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);

                            // Ensure the directory exists before saving the file
                            if (!Directory.Exists(folderName))
                            {
                                Directory.CreateDirectory(folderName);
                            }

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            foreach (var applicant in landDTO.applicants)
                            {
                                if (applicant.identity_photo == file.FileName)
                                {
                                    applicant.filepath = filePath;
                                }
                            }
                            
                        }
                       
                    }
                }

                var result = await _landService.Post(landDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static string uploadFile(string type, string appointment_no, IFormFile fileData = null,
            ICollection<IFormFile> EmployeeFile = null)
        {
            var Image = fileData;
            string content = Image.ContentType;
            var ext = Path.GetExtension(Image.FileName) == "" ? ".webp" : Image.ContentType.Contains("image") ?
                            ".webp" : Path.GetExtension(Image.FileName);
            var filename = Image.FileName.Split(".")[0];
            var folderName = Path.Combine("assets",type);
            var filepath = Path.Combine(folderName, appointment_no.Replace(" ", "") + "_" + filename.Replace(" ", "") + ext);

            // Ensure the directory exists before saving the file
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            return filepath;
        }

        [HttpGet("GetLandRegisteredDetails")]
        public async Task<object> Get()
        {

            try
            {
                var result = await _landService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteLandDetail/{application_number}")]
        public async Task<IActionResult> Delete(long application_number)
        {

            try
            {
                var result = await _landService.Delete(application_number);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
