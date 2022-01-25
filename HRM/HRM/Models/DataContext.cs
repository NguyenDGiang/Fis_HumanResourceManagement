using System;using Thinktecture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<AcademicLevelDAO> AcademicLevel { get; set; }
        public virtual DbSet<CandidateDAO> Candidate { get; set; }
        public virtual DbSet<ChucVuDAO> ChucVu { get; set; }
        public virtual DbSet<DepartmentDAO> Department { get; set; }
        public virtual DbSet<DistrictDAO> District { get; set; }
        public virtual DbSet<EmployeeDAO> Employee { get; set; }
        public virtual DbSet<InterviewResultDAO> InterviewResult { get; set; }
        public virtual DbSet<JobPositionDAO> JobPosition { get; set; }
        public virtual DbSet<ProvinceDAO> Province { get; set; }
        public virtual DbSet<StatusDAO> Status { get; set; }
        public virtual DbSet<VillageDAO> Village { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=127.0.0.1\\\\\\\\LOCAL_SQL_SERVER,62192;initial catalog=HRM;persist security info=True;user id=sa2;password=123456;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicLevelDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(1);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AcademicLevels)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__AcademicL__Statu__4316F928");
            });

            modelBuilder.Entity<CandidateDAO>(entity =>
            {
                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Candidate__Distr__46E78A0C");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Candidate__Provi__47DBAE45");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Candidate__Statu__44FF419A");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Candidate__Villa__45F365D3");
            });

            modelBuilder.Entity<ChucVuDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ChucVus)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__ChucVu__StatusId__3E52440B");
            });

            modelBuilder.Entity<DepartmentDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Departmen__Distr__412EB0B6");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Departmen__Provi__4222D4EF");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Departmen__Statu__3F466844");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Departmen__Villa__403A8C7D");
            });

            modelBuilder.Entity<DistrictDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__District__Provin__4CA06362");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__District__Status__4D94879B");
            });

            modelBuilder.Entity<EmployeeDAO>(entity =>
            {
                entity.Property(e => e.BeginJobTime).HasColumnType("datetime");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.AcademicLevel)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AcademicLevelId)
                    .HasConstraintName("FK__Employee__Academ__398D8EEE");

                entity.HasOne(d => d.ChucVu)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ChucVuId)
                    .HasConstraintName("FK__Employee__ChucVu__38996AB5");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Employee__Distri__3C69FB99");

                entity.HasOne(d => d.JobPosition)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobPositionId)
                    .HasConstraintName("FK__Employee__JobPos__3A81B327");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Employee__Provin__3D5E1FD2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Employee__Status__37A5467C");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Employee__Villag__3B75D760");
            });

            modelBuilder.Entity<InterviewResultDAO>(entity =>
            {
                entity.Property(e => e.BeginJobTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.InterviewTime).HasColumnType("datetime");

                entity.Property(e => e.TrialTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.InterviewResults)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK__Interview__Candi__48CFD27E");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.InterviewResults)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Interview__Statu__49C3F6B7");
            });

            modelBuilder.Entity<JobPositionDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(1);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.JobPositions)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__JobPositi__Statu__440B1D61");
            });

            modelBuilder.Entity<ProvinceDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Province__Status__4E88ABD4");
            });

            modelBuilder.Entity<StatusDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<VillageDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Village__Distric__4AB81AF0");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Village__StatusI__4BAC3F29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
