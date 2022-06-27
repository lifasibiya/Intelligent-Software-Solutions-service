using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iss.Models
{
    [Keyless]
    public class UploadDocument
    {
        public int user { get; set; }
        public IFormFile[] file { get; set; }
        public byte[]? content { get; set; }
        public string? filename { get; set; }
    }
}
