//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using CMCSProject.Data;
using CMCSProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CMCSProject.Tests;

internal static class TestDbHelper
{
    public static AppDbContext NewContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var ctx = new AppDbContext(options);
        ctx.Database.EnsureCreated();
        return ctx;
    }
    //------------------------------------------------------------------------------------------------------------------------
    public static int SeedPendingClaim(AppDbContext ctx, string lecturer = "Test Lecturer",
        decimal hours = 6m, decimal rate = 120m, DateTime? date = null)
    {
        var claim = new Claim
        {
            LecturerName = lecturer,
            HoursWorked = hours,
            HourlyRate = rate,
            ClaimDate = date ?? DateTime.Today,
            Status = ClaimStatus.Pending
        };
        ctx.Claims.Add(claim);
        ctx.SaveChanges();
        return claim.ClaimId;
    }
    //------------------------------------------------------------------------------------------------------------------------
    public static int SeedVerifiedClaim(AppDbContext ctx, string lecturer = "Test Lecturer 2",
        decimal hours = 8m, decimal rate = 350m, DateTime? date = null)
    {
        var claim = new Claim
        {
            LecturerName = lecturer,
            HoursWorked = hours,
            HourlyRate = rate,
            ClaimDate = date ?? DateTime.Today,
            Status = ClaimStatus.Verified
        };
        ctx.Claims.Add(claim);
        ctx.SaveChanges();

        ctx.Approvals.Add(new Approval
        {
            ClaimId = claim.ClaimId,
            ApproverName = "Coordinator Demo",
            ApproverRole = ApproverRole.Coordinator,
            ApprovalDate = DateTime.Now
        });
        ctx.SaveChanges();

        return claim.ClaimId;
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------