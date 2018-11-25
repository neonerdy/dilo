
using System;
using Microsoft.EntityFrameworkCore;
using Dilo.Models;

namespace Dilo
{
    public class AppDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }

        public static string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Team>(entity=>
             {
                entity.ToTable("teams");
                entity.Property(e=>e.ID).HasColumnName("id");
                entity.Property(e=>e.FullName).HasColumnName("full_name");
                entity.Property(e=>e.Role).HasColumnName("role");
                entity.Property(e=>e.Address).HasColumnName("address");
                entity.Property(e=>e.Phone).HasColumnName("phone");
                entity.Property(e=>e.Email).HasColumnName("email");
             });

            modelBuilder.Entity<Project>(entity=>
            {
                entity.ToTable("projects");
                entity.Property(e=>e.ID).HasColumnName("id");
                entity.Property(e=>e.ProjectName).HasColumnName("project_name");
                entity.Property(e=>e.Initial).HasColumnName("initial");
                entity.Property(e=>e.Description).HasColumnName("description");
                entity.Property(e=>e.CreatedDate).HasColumnName("created_date");
                entity.Property(e=>e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<WorkItem>(entity=>
            {
                entity.ToTable("workitems");
                entity.Property(e=>e.ID).HasColumnName("id");
                entity.Property(e=>e.ProjectId).HasColumnName("project_id");
                entity.Property(e=>e.Tracker).HasColumnName("tracker");
                entity.Property(e=>e.Title).HasColumnName("title");
                entity.Property(e=>e.Category).HasColumnName("category");
                entity.Property(e=>e.Priority).HasColumnName("priority");
                entity.Property(e=>e.AssigneeId).HasColumnName("assignee_id");
                entity.Property(e=>e.Description).HasColumnName("description");
                entity.Property(e=>e.Status).HasColumnName("status");
                entity.Property(e=>e.CreatedDate).HasColumnName("created_date");
                entity.Property(e=>e.ModifiedDate).HasColumnName("modified_date");
            });


        }


    }
}