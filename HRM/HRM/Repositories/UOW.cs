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
    }

    public class UOW : IUOW
    {
        private DataContext DataContext;

        public IStatusRepository StatusRepository { get; private set; }
        public IChucVuRepository ChucVuRepository { get; private set; }
        public IAcademicLevelRepository AcademicLevelRepository { get; private set; }
        public ICandidateRepository CandidateRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;

            StatusRepository = new StatusRepository(DataContext);
            ChucVuRepository = new ChucVuRepository(DataContext);
            AcademicLevelRepository = new AcademicLevelRepository(DataContext);
            CandidateRepository = new CandidateRepository(DataContext);
            ChucVuRepository = new ChucVuRepository(DataContext);
            DepartmentRepository = new DepartmentRepository(DataContext);

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
