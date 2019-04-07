using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Entities.System;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.DataAccess.Context
{
    internal sealed class TgprContext : DbContext
    {
        public TgprContext(DbContextOptions<TgprContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership
                             && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            //SetUserEntities(modelBuilder);
        }

        private void SetUserEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleSecurityActivity>()
                .HasOne(x => x.SecurityActivity)
                .WithMany()
                .HasForeignKey(x => x.SecurityActivityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .HasMany(x => x.SecurityActivities)
                .WithOne()
                .HasForeignKey(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        // System
        public DbSet<Version> Version { get; set; }

        // Users
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SecurityActivity> SecurityActivity { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoleSecurityActivity> RoleSecurityActivity { get; set; }

        // Applications
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<ApplicationTemplate> ApplicationTemplate { get; set; }
        public DbSet<ApplicationCategory> ApplicationCategory { get; set; }
        public DbSet<ApplicationCategoryReview> ApplicationCategoryReview { get; set; }
        public DbSet<ApplicationQuestion> ApplicationQuestion { get; set; }
        public DbSet<ApplicationQuestionType> ApplicationQuestionType { get; set; }
        public DbSet<ApplicationOption> ApplicationOption { get; set; }
        public DbSet<ApplicationOptionStatus> ApplicationOptionStatus { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<ApplicationCategoryReviewText> ApplicationCategoryReviewText { get; set; }
        public DbSet<ApplicationCategoryReviewStatus> ApplicationCategoryReviewStatus { get; set; }
        public DbSet<ApplicationQuestionAnswer> ApplicationQuestionAnswer { get; set; }
    }
}
