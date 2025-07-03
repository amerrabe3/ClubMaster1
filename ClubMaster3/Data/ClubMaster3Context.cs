using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubMaster3.Models;

namespace ClubMaster3.Data
{
    public class ClubMaster3Context : DbContext
    {
        public ClubMaster3Context (DbContextOptions<ClubMaster3Context> options)
            : base(options)
        {
        }

        public DbSet<ClubMaster3.Models.Coach> Coach { get; set; } = default!;
        public DbSet<ClubMaster3.Models.Player> Player { get; set; } = default!;
        public DbSet<ClubMaster3.Models.Team> Team { get; set; } = default!;
     
    }
}
