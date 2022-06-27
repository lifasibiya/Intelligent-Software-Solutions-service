using iss.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Dapper;

namespace iss.Models
{
    public class DocumentManagement : IDocumentService
    {
        private readonly ISSDBContext _context;
        public DocumentManagement(ISSDBContext dbContext)
        {
            _context = dbContext;
        }
        bool IDocumentService.Delete(string id)
        {
            var param = new DynamicParameters();
            param.Add("id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            return ISSDBContext.ExcuteProcs<bool>("[dbo].[sp_DeleteFile]", param).FirstOrDefault();
        }

        IEnumerable<Document> IDocumentService.GetDocuments()
        {
            return ISSDBContext.ExcuteProcs<Document>("[dbo].[sp_GetAllFiles]", null);
        }

        bool IDocumentService.Save(UploadDocument upload)
        {
            var param = new DynamicParameters();
            param.Add("filename", upload.filename, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("user", upload.user, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("content", upload.content, System.Data.DbType.Binary, System.Data.ParameterDirection.Input);
            return ISSDBContext.ExcuteProcs<bool>("[dbo].[sp_UploadFile]", param).FirstOrDefault();
        }
    }
}
