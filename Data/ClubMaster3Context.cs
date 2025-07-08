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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamA)
                .WithMany()
                .HasForeignKey(m => m.TeamAId)
                .OnDelete(DeleteBehavior.Restrict); // منع الحذف التلقائي

            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamB)
                .WithMany()
                .HasForeignKey(m => m.TeamBId)
                .OnDelete(DeleteBehavior.Restrict); // نفس الشي
        }


        public DbSet<ClubMaster3.Models.Coach> Coach { get; set; } = default!;
        public DbSet<ClubMaster3.Models.Player> Player { get; set; } = default!;
        public DbSet<ClubMaster3.Models.Team> Team { get; set; } = default!;
        public DbSet<ClubMaster3.Models.Match> Matches { get; set; }
        public DbSet<ClubMaster3.Models.User> Users { get; set; }
    }
}
