//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using System.Linq;
using System.Threading.Tasks;
using CMCSProject.Data;
using CMCSProject.Models;
using CMCSProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CMCSProject.Tests.Controllers
{
    public class CMCSControllerTests
    {
        private static CMCSController MakeController(AppDbContext ctx)
        {
            var env = new FakeWebHostEnvironment();
            return new CMCSController(ctx, env);
        }
        //------------------------------------------------------------------------------------------------------------------------
        // 1) VERIFY a Pending claim => changes to Verified + adds approval
        [Fact]
        public async Task Verify_PendingClaim_ChangesStatusToVerified_AndAddsApproval()
        {
            using var ctx = TestDbHelper.NewContext();
            var id = TestDbHelper.SeedPendingClaim(ctx, "Lecturer X");

            var controller = MakeController(ctx);

            var result = await controller.Verify(id, "Coordinator Demo", "Coordinator");

            var updated = ctx.Claims.Single(c => c.ClaimId == id);
            Assert.Equal(ClaimStatus.Verified, updated.Status);

            var approval = ctx.Approvals.Single(a => a.ClaimId == id);
            Assert.Equal(ApproverRole.Coordinator, approval.ApproverRole);
            Assert.Equal("Coordinator Demo", approval.ApproverName);

            Assert.IsType<RedirectToActionResult>(result);
            var redirect = (RedirectToActionResult)result;
            Assert.Equal("Approvals", redirect.ActionName);   
        }
        //------------------------------------------------------------------------------------------------------------------------
        // 2) APPROVE a Verified claim => changes to Approved + adds Manager approval
        [Fact]
        public async Task Approve_VerifiedClaim_ChangesStatusToApproved()
        {
            using var ctx = TestDbHelper.NewContext();
            var id = TestDbHelper.SeedVerifiedClaim(ctx); 

            var controller = MakeController(ctx);
            
            var result = await controller.Approve(id, "Manager Demo", "Manager");
           
            var updated = ctx.Claims.Single(c => c.ClaimId == id);
            Assert.Equal(ClaimStatus.Approved, updated.Status);

            var lastApproval = ctx.Approvals
                .Where(a => a.ClaimId == id)
                .OrderByDescending(a => a.ApprovalId)
                .First();

            Assert.Equal(ApproverRole.Manager, lastApproval.ApproverRole);
            Assert.Equal("Manager Demo", lastApproval.ApproverName);

            Assert.IsType<RedirectToActionResult>(result);
            var redirect = (RedirectToActionResult)result;
            Assert.Equal("Approvals", redirect.ActionName);      
        }
        //------------------------------------------------------------------------------------------------------------------------
        // 3) REJECT from Pending => changes to Rejected (Coordinator)
        [Fact]
        public async Task Reject_FromPending_SetsStatusRejected()
        {
            using var ctx = TestDbHelper.NewContext();
            var id = TestDbHelper.SeedPendingClaim(ctx, "Lecturer Y");

            var controller = MakeController(ctx);

            var result = await controller.Reject(id, "Coordinator Demo", ApproverRole.Coordinator, "Insufficient info");

            var updated = ctx.Claims.Single(c => c.ClaimId == id);
            Assert.Equal(ClaimStatus.Rejected, updated.Status);

            var lastApproval = ctx.Approvals
                .Where(a => a.ClaimId == id)
                .OrderByDescending(a => a.ApprovalId)
                .First();
            Assert.Equal(ApproverRole.Coordinator, lastApproval.ApproverRole);

            Assert.IsType<RedirectToActionResult>(result);
            var redirect = (RedirectToActionResult)result;
            Assert.Equal("Approvals", redirect.ActionName);
        }
        //------------------------------------------------------------------------------------------------------------------------
        // 4) REJECT from Verified => changes to Rejected (Manager)
        [Fact]
        public async Task Reject_FromVerified_SetsStatusRejected()
        {
            using var ctx = TestDbHelper.NewContext();
            var id = TestDbHelper.SeedVerifiedClaim(ctx); 

            var controller = MakeController(ctx);
 
            var result = await controller.Reject(id, "Manager Demo", ApproverRole.Manager, "Budget constraints");

            var updated = ctx.Claims.Single(c => c.ClaimId == id);
            Assert.Equal(ClaimStatus.Rejected, updated.Status);

            var lastApproval = ctx.Approvals
                .Where(a => a.ClaimId == id)
                .OrderByDescending(a => a.ApprovalId)
                .First();
            Assert.Equal(ApproverRole.Manager, lastApproval.ApproverRole);

            Assert.IsType<RedirectToActionResult>(result);
            var redirect = (RedirectToActionResult)result;
            Assert.Equal("Approvals", redirect.ActionName);
        }
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------