using System.Collections;
namespace iss.Models
{
    public interface IDocumentService
    {
        public bool Save(UploadDocument uploadDocument);
        public bool Delete(string id);
        public IEnumerable<Document> GetDocuments();
    }
}
