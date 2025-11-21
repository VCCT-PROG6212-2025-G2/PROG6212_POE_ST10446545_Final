//*****************************************************************************
//PROG6212_POE_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.ComponentModel.DataAnnotations;

namespace CMCSProject.ViewModels
{
    public class HrReportRow
    {
        public string LecturerName { get; set; } = "";
        public decimal TotalHours { get; set; }
        public decimal TotalAmount { get; set; }
        public int ClaimCount { get; set; }
    }

    public class HrReportVm
    {
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        public List<HrReportRow> Rows { get; set; } = new();

        public decimal GrandTotal => Rows.Sum(r => r.TotalAmount);
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------