using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using DTO.Models;
using BAL.Services.Image;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] Image_DTO data)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    var ext = Path.GetExtension(file.FileName) == "" ? ".webp" : file.ContentType.Contains("image") ?
                        ".webp" : Path.GetExtension(file.FileName);
                    var filename = file.FileName.Split(".")[0];
                    var folderName = Path.Combine("assets", "images");
                    var filepath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    data.filepath = filepath;
                }

            }
            return Ok(await _imageService.PostAsync(data));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PutAsync(int? id, [FromForm] Image_DTO data)
        {
            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    var ext = Path.GetExtension(file.FileName) == "" ? ".webp" : file.ContentType.Contains("image") ?
                        ".webp" : Path.GetExtension(file.FileName);
                    var filename = file.FileName.Split(".")[0];
                    var folderName = Path.Combine("assets", "images");
                    var filepath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    data.filepath = filepath;
                }

            }
            return Ok(await _imageService.PutAsync(id, data));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _imageService.GetAsync());
        }
    }
}
