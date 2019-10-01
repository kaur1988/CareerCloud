using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext:DbContext

    {

        public CareerCloudContext(bool createProxy= true) :base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {
            Configuration.ProxyCreationEnabled = createProxy;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfilePoco>()
            .HasMany(c => c.CompanyDescriptions)
            .WithRequired(d => d.CompanyProfile)
            .HasForeignKey(d => d.Company)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
              .HasMany(c => c.CompanyJobs)
              .WithRequired(d => d.CompanyProfile)
              .HasForeignKey(d => d.Company)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>()
              .HasMany(c => c.CompanyLocations)
              .WithRequired(d => d.CompanyProfile)
              .HasForeignKey(d => d.Company)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>()
              .HasMany(c => c.ApplicantWorkHistories)
              .WithRequired(d => d.SystemCountryCode)
              .HasForeignKey(d => d.CountryCode)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>()
              .HasMany(c => c.ApplicantProfiles)
              .WithRequired(d => d.SystemCountryCode)
              .HasForeignKey(d => d.Country)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
              .HasMany(c => c.ApplicantJobApplications)
              .WithRequired(d => d.CompanyJobs)
              .HasForeignKey(d => d.Job)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
             .HasMany(c => c.CompanyJobSkills)
             .WithRequired(d => d.CompanyJob)
             .HasForeignKey(d => d.Job)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
             .HasMany(c => c.CompanyJobDescriptions)
             .WithRequired(d => d.CompanyJob)
             .HasForeignKey(d => d.Job)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>()
             .HasMany(c => c.CompanyJobEducations)
             .WithRequired(d => d.CompanyJob)
             .HasForeignKey(d => d.Job)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityRolePoco>()
             .HasMany(c => c.SecurityLoginsRoles)
             .WithRequired(d => d.SecurityRole)
             .HasForeignKey(d => d.Role)
             .WillCascadeOnDelete(true);


            modelBuilder.Entity<SecurityLoginPoco>()
             .HasMany(c => c.SecurityLoginsLogs)
             .WithRequired(d => d.SecurityLogin)
             .HasForeignKey(d => d.Login)
             .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .HasMany(c => c.ApplicantProfiles)
            .WithRequired(d => d.SecurityLogin)
            .HasForeignKey(d => d.Login)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>()
            .HasMany(c => c.SecurityLoginsRoles)
            .WithRequired(d => d.SecurityLogin)
            .HasForeignKey(d => d.Login)
            .WillCascadeOnDelete(true);


            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(c => c.ApplicantWorkHistories)
            .WithRequired(d => d.ApplicantProfile)
            .HasForeignKey(d => d.Applicant)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
           .HasMany(c => c.ApplicantEducations)
           .WithRequired(d => d.ApplicantProfiles)
           .HasForeignKey(d => d.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
           .HasMany(c => c.ApplicantSkills)
           .WithRequired(d => d.ApplicantProfile)
           .HasForeignKey(d => d.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
           .HasMany(c => c.ApplicantResumes)
           .WithRequired(d => d.ApplicantProfile)
           .HasForeignKey(d => d.Applicant)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(c => c.ApplicantJobApplications)
          .WithRequired(d => d.ApplicantProfile)
          .HasForeignKey(d => d.Applicant)
          .WillCascadeOnDelete(true);

           modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(c => c.ApplicantJobApplications)
          .WithRequired(d => d.ApplicantProfile)
          .HasForeignKey(d => d.Applicant)
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemLanguageCodePoco>()
           .HasMany(c => c.CompanyDescriptions)
           .WithRequired(d => d.SystemLanguage)
           .HasForeignKey(d => d.LanguageId)
           .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco>ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocationPocos { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

    }
}
