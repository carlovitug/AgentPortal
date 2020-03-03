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
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Moa> Moa { get; set; }
        public DbSet<Terminal> Terminal { get; set; }
        public DbSet<UserAccounts> UserInformation { get; set; }
        public DbSet<AgentBranches> AgentBranches { get; set; }
        public DbSet<MasterAgentID> MasterAgentID { get; set; }
    }
}
