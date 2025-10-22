//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMCSProject.ViewModels
{
    public class ClaimCreateVm
    {
        [Required] public string LecturerName { get; set; } = "";
        [Range(0, 1000)] public decimal HoursWorked { get; set; }
        [Range(0, 5000)] public decimal HourlyRate { get; set; }
        [DataType(DataType.Date)] public DateTime ClaimDate { get; set; } = DateTime.Today;
        [StringLength(500)] public string? Notes { get; set; }

        public List<IFormFile> Files { get; set; } = new(); //multiple uploads
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------