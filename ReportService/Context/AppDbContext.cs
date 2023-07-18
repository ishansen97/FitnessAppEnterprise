using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportService.Entity;

namespace ReportService.Context
{
  public class AppDbContext : DbContext
  {
    public DbSet<Report> Reports { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
  }
}
