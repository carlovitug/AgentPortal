using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Models
{
    public class ABMSContext:DbContext
    {
        public ABMSContext(DbContextOptions<ABMSContext> options) : base(options)
        {

        }

        public DbSet<Agent> Agent { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<General> General { get; set; }
    }
}
