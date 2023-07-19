using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HrSystemLastOne.Models
{
    public class ITIContext : IdentityDbContext<ApplicationUser>
    {

        public ITIContext()
        {
            
        }
        public ITIContext(DbContextOptions options):base(options)
        {
            
        }
        public  DbSet<Department> Departments { get; set; }
        public  DbSet<Employee> Employees { get; set; }

        public  DbSet<General_Settings> General_Settings { get; set; }
        public  DbSet<LeaveAttend> LeaveAttends { get; set; }
        public  DbSet<OffDays> OffDays { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HrSystem1;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
