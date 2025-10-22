//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using CMCSProject.Data;
using CMCSProject.Models;
using CMCSProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//------------------------------------------------------------------------------------------------------------------------
namespace CMCSProject.Controllers
{
    public class CMCSController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private static readonly string[] Allowed = { ".pdf", ".docx", ".xlsx" };
        private const long MaxFileBytes = 10 * 1024 * 1024; //10MB each

        public CMCSController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db; _env = env;
        }
        //------------------------------------------------------------------------------------------------------------------------
        // DASHBOARD
        public async Task<IActionResult> Dashboard()
        {
            var month = DateTime.Today.Month;
            var year = DateTime.Today.Year;

            // Sum as nullable, then coalesce to 0m so empty sets don't throw
            var totalThisMonth = await _db.Claims
                .Where(c => c.ClaimDate.Month == month && c.ClaimDate.Year == year)
                .SumAsync(c => (decimal?)(c.HoursWorked * c.HourlyRate)) ?? 0m;

            ViewBag.TotalThisMonth = totalThisMonth;

            var latest = await _db.Claims
                .OrderByDescending(c => c.ClaimDate)
                .Take(10)
                .AsNoTracking()
                .ToListAsync();

            return View(latest);
        }
        //------------------------------------------------------------------------------------------------------------------------
        // SUBMIT CLAIM (GET)
        [HttpGet]
        public IActionResult SubmitClaim() => View(new ClaimCreateVm());

        //------------------------------------------------------------------------------------------------------------------------
        // SUBMIT CLAIM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(ClaimCreateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var claim = new Claim
            {
                LecturerName = vm.LecturerName,
                HoursWorked = vm.HoursWorked,
                HourlyRate = vm.HourlyRate,
                ClaimDate = vm.ClaimDate,
                Notes = vm.Notes,
                Status = ClaimStatus.Pending
            };

            //------------------------------------------------------------------------------------------------------------------------
            // handle files
            var uploadRoot = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadRoot);

            foreach (var file in vm.Files ?? Enumerable.Empty<IFormFile>())
            {
                if (file.Length == 0) continue;
                if (file.Length > MaxFileBytes) { ModelState.AddModelError("", $"File {file.FileName} exceeds 10MB."); return View(vm); }

                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!Allowed.Contains(ext)) { ModelState.AddModelError("", $"File type {ext} not allowed."); return View(vm); }

                var savedName = $"{Guid.NewGuid()}{ext}";
                var fullPath = Path.Combine(uploadRoot, savedName);
                using (var stream = System.IO.File.Create(fullPath))
                    await file.CopyToAsync(stream);

                claim.Documents.Add(new SupportingDocument
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType ?? "application/octet-stream",
                    FilePath = $"/uploads/{savedName}"
                });
            }

            _db.Claims.Add(claim);
            await _db.SaveChangesAsync();
            TempData["msg"] = "Claim submitted successfully.";
            return RedirectToAction(nameof(Dashboard));
        }

        //------------------------------------------------------------------------------------------------------------------------
        //DOCUMENTS LIST
        public async Task<IActionResult> UploadedDocuments()
        {
            var docs = await _db.SupportingDocuments
                .Include(d => d.Claim)
                .OrderByDescending(d => d.UploadDate).ToListAsync();
            return View(docs);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //APPROVALS VIEW 
        public async Task<IActionResult> Approvals()
        {
            var items = await _db.Claims
                .Include(c => c.Documents)
                .OrderBy(c => c.Status)
                .ToListAsync();
            return View(items);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //VERIFY Coordinator
        [HttpPost]
        public async Task<IActionResult> Verify(int id, string approverName, string? comment)
        {
            var claim = await _db.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Verified;
            _db.Approvals.Add(new Approval
            {
                ClaimId = id,
                ApproverName = approverName,
                ApproverRole = ApproverRole.Coordinator,
                IsApproved = true,
                Comment = comment
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Approvals));
        }

        //------------------------------------------------------------------------------------------------------------------------
        // APPROVE (Manager)
        [HttpPost]
        public async Task<IActionResult> Approve(int id, string approverName, string? comment)
        {
            var claim = await _db.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Approved;
            _db.Approvals.Add(new Approval
            {
                ClaimId = id,
                ApproverName = approverName,
                ApproverRole = ApproverRole.Manager,
                IsApproved = true,
                Comment = comment
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Approvals));
        }

        //------------------------------------------------------------------------------------------------------------------------
        // REJECT (either role)
        [HttpPost]
        public async Task<IActionResult> Reject(int id, string approverName, ApproverRole role, string? comment)
        {
            var claim = await _db.Claims.FindAsync(id);
            if (claim == null) return NotFound();

            claim.Status = ClaimStatus.Rejected;
            _db.Approvals.Add(new Approval
            {
                ClaimId = id,
                ApproverName = approverName,
                ApproverRole = role,
                IsApproved = false,
                Comment = comment
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Approvals));
        }
        //------------------------------------------------------------------------------------------------------------------------
        // GET: /CMCS/Profile
        [HttpGet]
        public IActionResult Profile()
        {
            // Static placeholder for Part 2 
            var vm = new
            {
                FullName = "Test Lecturer",
                Email = "test.lecturer@example.com",
                Department = "Computer Science"
            };
            return View(vm);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Coordinator()
        {
            var items = await _db.Claims
                .Include(c => c.Documents)
                .Where(c => c.Status == ClaimStatus.Pending)
                .OrderBy(c => c.ClaimDate)
                .ToListAsync();
            return View(items);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Manager()
        {
            var items = await _db.Claims
                .Include(c => c.Documents)
                .Where(c => c.Status == ClaimStatus.Verified)
                .OrderBy(c => c.ClaimDate)
                .ToListAsync();
            return View(items);
        }
        //------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> MyClaims(string lecturer = "Test Lecturer")
        {
            var claims = await _db.Claims
                .Where(c => c.LecturerName == lecturer)
                .OrderByDescending(c => c.ClaimDate)
                .ToListAsync();
            ViewBag.Lecturer = lecturer;
            return View(claims);
        }
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------