//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.ComponentModel.DataAnnotations;

namespace CMCSProject.Models
{
    public class Approval
    {
        public int ApprovalId { get; set; }
        public int ClaimId { get; set; }
        public Claim? Claim { get; set; }

        public ApproverRole ApproverRole { get; set; }
        [Required, StringLength(120)]
        public string ApproverName { get; set; } = "";

        public DateTime ApprovalDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsApproved { get; set; }  

        [StringLength(500)]
        public string? Comment { get; set; }
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------