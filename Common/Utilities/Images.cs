/*
<summary>
Author: Pakaj Kishor Neupaney
Created date: 20-10-2020
Desc:Webp Format and Image Resize
</summary>
<returns></returns>
*/
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;


namespace Common.Utilities
{
    public class Images
    {

        public string convertImageToWebp(IFormFile files)
        {

            List<string> FileList = new List<string>();
            var folderNameWebP = Path.Combine("assets", "images", "orignal");
            var folderNameWebPmedium = Path.Combine("assets", "images", "medium");
            var folderNameWebPsmall = Path.Combine("assets", "images", "small");
            var pathToSaveWebP = Path.Combine(Directory.GetCurrentDirectory(), folderNameWebP);
            var pathToSaveWebPmedium = Path.Combine(Directory.GetCurrentDirectory(), folderNameWebPmedium);
            var pathToSaveWebPsmall = Path.Combine(Directory.GetCurrentDirectory(), folderNameWebPsmall);
            var guid = Guid.NewGuid();
            var fileNamewebconv = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            var extwebconv = ".webp";
            fileNamewebconv = Path.GetFileNameWithoutExtension(fileNamewebconv) + '_' + guid + extwebconv;
            var fullPathwebconv = Path.Combine(pathToSaveWebP, fileNamewebconv);
            var fullPathwebconvmedium = Path.Combine(pathToSaveWebPmedium, fileNamewebconv);
            var fullPathwebconvsmall = Path.Combine(pathToSaveWebPsmall, fileNamewebconv);

            using (var webPFileStream = new FileStream(fullPathwebconv, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(files.OpenReadStream())
                                .Format(new WebPFormat())
                                .Quality(70)
                                .Save(webPFileStream);
                }
            }
            using (var webPFileStream = new FileStream(fullPathwebconvmedium, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    Size size = new Size(250, 250);
                    imageFactory.Load(files.OpenReadStream())
                                .Format(new WebPFormat())
                                .Quality(100)
                                .Resize(size)
                                .Save(webPFileStream);
                }
            }
            using (var webPFileStream = new FileStream(fullPathwebconvsmall, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    Size size = new Size(95, 95);
                    imageFactory.Load(files.OpenReadStream())
                                .Format(new WebPFormat())
                                .Quality(100)
                                .Resize(size)
                                .Save(webPFileStream);
                }
            }


            return fileNamewebconv;
        }
    }
}
