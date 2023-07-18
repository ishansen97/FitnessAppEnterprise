using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PredictionsService.Entity;

namespace PredictionsService.Context
{
  public class PredictionDbContext : DbContext
  {
    public DbSet<PredictionConstant> PredictionConstants { get; set; }

    public DbSet<Prediction> Predictions { get; set; }

    public PredictionDbContext(DbContextOptions<PredictionDbContext> options) : base(options)
    {
    }
  }
}
