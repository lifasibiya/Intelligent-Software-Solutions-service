using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iss.Models;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;

namespace iss.Controllers
{
    [EnableCors("ISSPOLICY")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("get")]
        public dynamic GetDocument()
        {
            try
            {
                return _documentService.GetDocuments();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("create")]
        public dynamic Upload([FromForm] UploadDocument upload)
        {
            try
            {
                byte[] content = null;
                foreach (IFormFile f in upload.file)
                {
                    using (var ms = new MemoryStream())
                    {
                        f.CopyTo(ms);
                        content = ms.ToArray();

                    }

                    _documentService.Save(new UploadDocument{ user = upload.user, content = content, filename = upload.filename });
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public dynamic Delete(string id)
        {
            try
            {
                return _documentService.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
