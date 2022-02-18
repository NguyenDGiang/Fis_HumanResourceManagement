using System;using Thinktecture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

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

            // Tạo ILoggerFactory
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            optionsBuilder       // thiết lập làm việc với SqlServer
                        .UseLoggerFactory(loggerFactory);     // thiết lập logging
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureTempTable<long>();
            modelBuilder.Entity<AcademicLevelDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AcademicLevels)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__AcademicL__Statu__6FE99F9F");
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
                    .HasConstraintName("FK__Candidate__Distr__70DDC3D8");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Candidate__Provi__71D1E811");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Candidate__Statu__72C60C4A");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Candidate__Villa__73BA3083");
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
                    .HasConstraintName("FK__ChucVu__StatusId__74AE54BC");
            });

            modelBuilder.Entity<DepartmentDAO>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Departmen__Distr__75A278F5");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Departmen__Provi__76969D2E");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Departmen__Statu__778AC167");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Departmen__Villa__787EE5A0");
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
                    .HasConstraintName("FK__District__Provin__797309D9");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__District__Status__7A672E12");
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
                    .HasConstraintName("FK__Employee__Academ__7B5B524B");

                entity.HasOne(d => d.ChucVu)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ChucVuId)
                    .HasConstraintName("FK__Employee__ChucVu__7C4F7684");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Employee__Distri__7D439ABD");

                entity.HasOne(d => d.JobPosition)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobPositionId)
                    .HasConstraintName("FK__Employee__JobPos__7E37BEF6");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Employee__Provin__7F2BE32F");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Employee__Status__00200768");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK__Employee__Villag__01142BA1");
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
                    .HasConstraintName("FK__Interview__Candi__02084FDA");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.InterviewResults)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Interview__Statu__02FC7413");
            });

            modelBuilder.Entity<JobPositionDAO>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.JobPositions)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__JobPositi__Statu__03F0984C");
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
                    .HasConstraintName("FK__Province__Status__04E4BC85");
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
                    .HasConstraintName("FK__Village__Distric__05D8E0BE");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Village__StatusI__06CD04F7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
