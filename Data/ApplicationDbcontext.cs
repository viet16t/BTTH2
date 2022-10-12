using DoanQuocVietBTTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace DoanQuocVietBTTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Student> Students {get; set;}
    }
}