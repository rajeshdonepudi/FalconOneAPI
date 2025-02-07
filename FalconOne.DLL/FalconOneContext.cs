﻿using FalconOne.Models.Entities;
using FalconOne.Models.Entities.Common;
using FalconOne.Models.Entities.ExpenseManagement;
using FalconOne.Models.Entities.Mails;
using FalconOne.Models.Entities.QuestionAndAnswer;
using FalconOne.Models.Entities.Security;
using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FalconOne.DAL
{
    public class FalconOneContext : IdentityDbContext<User, ApplicationRole, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        #region Security
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
        public DbSet<TenantUserSecurityGroup> UserSecurityGroups { get; set; }
        #endregion

        #region Tenant
        public DbSet<TenantPermission> TenantPermissions { get; set; }
        #endregion

        #region Common
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        #endregion

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        #endregion

        #region Mail
        public DbSet<Mail> Mails { get; set; }
        public DbSet<UserMail> UserMail { get; set; }
        #endregion

        #region Tenant
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> TenantUsers { get; set; }
        #endregion

        #region Themes
        public DbSet<AppTheme> AppThemes { get; set; }
        #endregion

        #region System
        #endregion

        #region Question

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        #endregion

        #region Expense Managment
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        #endregion

        #region Constructor
        public FalconOneContext(DbContextOptions<FalconOneContext> options) : base(options)
        {
            
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            EntityConfiguration(modelBuilder);
        }

        private static void EntityConfiguration(ModelBuilder builder)
        {
            #region Tenants

            builder.Entity<Tenant>()
                   .Property(a => a.AccountAlias)
                   .HasColumnType("nvarchar(max)")
                   .HasComputedColumnSql($"CONVERT(NVARCHAR(max), {GenerateTimeBasedId()}) + '-FALO_TEN' + CAST([AccountId] AS NVARCHAR(max))");

            #endregion

            #region User

            builder.Entity<User>()
                   .HasAlternateKey(x => x.ResourceId);

            builder.Entity<User>()
                   .Property(x => x.ResourceAlias)
                   .HasColumnType("nvarchar(max)")
                   .HasComputedColumnSql($"CONVERT(NVARCHAR(max), {GenerateTimeBasedId()}) + '-FALO_USR' + CAST([ResourceId] AS NVARCHAR(max))");
            #endregion
        }

        public static string GenerateTimeBasedId()
        {
            DateTime now = DateTime.UtcNow;

            string id = now.ToString("yyyyMMddHHmmssfff");

            return id;
        }
    }
}
