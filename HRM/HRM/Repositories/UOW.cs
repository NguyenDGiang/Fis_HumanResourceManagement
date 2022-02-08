using System;
using HRM.Models;
using TrueSight.Common;

namespace HRM.Repositories
{
    public interface IUOW : IServiceScoped, IDisposable
    {
        IStatusRepository StatusRepository { get; }
        IChucVuRepository ChucVuRepository { get; }
        IAcademicLevelRepository AcademicLevelRepository { get; }
        ICandidateRepository CandidateRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }

        // nhom 1
        IDistrictRepository DistrictRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        IVillageRepository VillageRepository { get; }

        // nhom 3
        IEmployeeRepository EmployeeRepository { get; }
        IInterviewResultRepository InterviewResultRepository { get; }
        IJobPositionRepository JobPositionRepository { get; }


    }

    public class UOW : IUOW
    {
        private DataContext DataContext;

        public IStatusRepository StatusRepository { get; private set; }
        public IChucVuRepository ChucVuRepository { get; private set; }
        public IAcademicLevelRepository AcademicLevelRepository { get; private set; }
        public ICandidateRepository CandidateRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IDistrictRepository DistrictRepository { get; private set; }

        public IProvinceRepository ProvinceRepository { get; private set; }

        public IVillageRepository VillageRepository { get; private set; }

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public IInterviewResultRepository InterviewResultRepository { get; private set; }

        public IJobPositionRepository JobPositionRepository { get; private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;

            StatusRepository = new StatusRepository(DataContext);
            ChucVuRepository = new ChucVuRepository(DataContext);
            AcademicLevelRepository = new AcademicLevelRepository(DataContext);
            CandidateRepository = new CandidateRepository(DataContext);
            ChucVuRepository = new ChucVuRepository(DataContext);
            DepartmentRepository = new DepartmentRepository(DataContext);
            DistrictRepository = new DistrictRepository(DataContext);
            ProvinceRepository = new ProvinceRepository(DataContext);
            VillageRepository = new VillageRepository(DataContext);
            EmployeeRepository = new EmployeeRepository(DataContext);
            InterviewResultRepository = new InterviewResultRepository(DataContext);
            JobPositionRepository = new JobPositionRepository(DataContext);

        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (this.DataContext == null)
            {
                return;
            }

            this.DataContext.Dispose();
            this.DataContext = null;
        }
    }
}
