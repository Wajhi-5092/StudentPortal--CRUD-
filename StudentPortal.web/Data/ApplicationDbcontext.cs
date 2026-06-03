using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Data
{
    public class ApplicationDbcontext: DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext>options):base(options)   //constructor
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
