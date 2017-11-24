using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiUploadFiles.Controllers
{
    public class FileUploadController : ApiController
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }

        [HttpPost]
        [ValidateMimeMultipartContentFilter]
        public async Task<object> UploadFiles()
        {
            var streamProvider = new MultipartFormDataStreamProvider(@"C:\temp");
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var files = streamProvider.FileData.Select(x =>
            {
                dynamic fileObj = new ExpandoObject();

                fileObj.LocalFileName = x.LocalFileName;

                if (x.Headers.ContentDisposition != null)
                {
                    fileObj.FileName = x.Headers.ContentDisposition.FileName;
                    fileObj.Size = x.Headers.ContentDisposition.Size;
                    fileObj.CreationDate = x.Headers.ContentDisposition.CreationDate;
                }

                if (x.Headers.ContentType != null)
                {
                    fileObj.MediaType = x.Headers.ContentType.MediaType;
                }

                return fileObj;
            }).ToList();

            return ProcessFiles(files);
        }

        private object ProcessFiles(dynamic files)
        {
            // Just testing
            foreach (dynamic file in files)
            {
                string filePath = file.LocalFileName;
                File.Delete(filePath);
            }

            return new
            {
                NbOfFiles = files.Count
            };
        }
    }
}
