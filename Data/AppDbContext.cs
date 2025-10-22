//*****************************************************************************
//PROG6212_Part1_ST10446545_ST10446545@vcconnect.edu.za
//*****************************************************************************
using CMCSProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CMCSProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Claim> Claims => Set<Claim>();
        public DbSet<SupportingDocument> SupportingDocuments => Set<SupportingDocument>();
        public DbSet<Approval> Approvals => Set<Approval>();
    }
}
//---------------------------------------------------------- End Of File -----------------------------------------------------------------