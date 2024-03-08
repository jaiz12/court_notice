using BAL.Services.category;
using BAL.Services.Category;
using BAL.Services.Marriage;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
    // =============================================
    // -- Author:		Jaideep Roy
    // -- Create date: 26-Feb-2024
    // =============================================
    public class MarriageController : Controller
    {
        private readonly IMarriageService _marriageService;
        public MarriageController(IMarriageService marriageService)
        {
            _marriageService = marriageService;
        }

        [HttpPost("PostMarriage")]
        public async Task<IActionResult> PostMarriage([FromForm] MarriageDTO marriage,
            IFormFile husband_files = null, IFormFile wife_files = null, IFormFile marriage_certificate_files = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var husband_photo = husband_files;
            var wife_photo = wife_files;
            var marriage_certificate_photo = marriage_certificate_files;
            try
            {
                marriage.husbandfilepath = uploadFile("Husband",marriage.application_no, husband_photo, null);
                marriage.wifefilepath = uploadFile("Wife", marriage.application_no, wife_photo, null);
                marriage.marriage_certificate_path = uploadFile("MarriageCetrificate", marriage.application_no, marriage_certificate_photo, null);

                var result = await _marriageService.Post(marriage);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static string uploadFile(string type,string appointment_no, IFormFile fileData = null,
            ICollection<IFormFile> EmployeeFile = null)
        {
            var Image = fileData;
            string content = Image.ContentType;
            var ext = Path.GetExtension(Image.FileName) == "" ? ".webp" : Image.ContentType.Contains("image") ?
                            ".webp" : Path.GetExtension(Image.FileName);
            var filename = Image.FileName.Split(".")[0];
            var folderName = Path.Combine("assets", "Marriage", type);
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


        [HttpGet("GetMarriageRegisteredDetails")]
        public async Task<IActionResult> Get()
        {

            try
            {
                var result = await _marriageService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteMarriageDetail/{marriage_id}")]
        public async Task<IActionResult> Delete(long marriage_id)
        {

            try
            {
                var result = await _marriageService.Delete(marriage_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
