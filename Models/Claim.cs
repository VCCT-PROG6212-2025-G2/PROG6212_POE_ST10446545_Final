//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCSProject.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required, StringLength(120)]
        public string LecturerName { get; set; } = "";

        [Range(0, 1000)]
        public decimal HoursWorked { get; set; }

        [Range(0, 5000)]
        public decimal HourlyRate { get; set; }

        [NotMapped]
        public decimal TotalAmount => HoursWorked * HourlyRate;

        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; } = DateTime.Today;

        [StringLength(500)]
        public string? Notes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public ICollection<SupportingDocument> Documents { get; set; } = new List<SupportingDocument>();
        public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------