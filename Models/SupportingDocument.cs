//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.ComponentModel.DataAnnotations;

namespace CMCSProject.Models
{
    public class SupportingDocument
    {
        public int SupportingDocumentId { get; set; }
        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }

        [Required] public string FileName { get; set; } = "";
        [Required] public string ContentType { get; set; } = "";
        [Required] public string FilePath { get; set; } = "";   
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------