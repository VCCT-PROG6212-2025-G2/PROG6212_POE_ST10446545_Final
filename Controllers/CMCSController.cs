//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using Microsoft.AspNetCore.Mvc;

namespace CMCSProject.Controllers
{
    public class CMCSController : Controller
    {
        public IActionResult Dashboard() => View();
        public IActionResult SubmitClaim() => View();
        public IActionResult UploadedDocuments() => View();
        public IActionResult Approvals() => View();
        public IActionResult Profile() => View();
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------