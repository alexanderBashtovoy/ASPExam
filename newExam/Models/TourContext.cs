using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newExam.Models
{
    public class TourContext : DbContext
    {
        public TourContext() : base("DataContext")
        { }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
