using System;
using HRM.Models;
using TrueSight.Common;

namespace HRM.Repositories
{
    public interface IUOW : IServiceScoped, IDisposable
    {
        IStatusRepository StatusRepository { get; }
        IChucVuRepository ChucVuRepository { get; }
        
    }

    public class UOW : IUOW
    {
        private DataContext DataContext;

        public IStatusRepository StatusRepository { get; private set; }
        public IChucVuRepository ChucVuRepository { get;  private set; }

        public UOW(DataContext DataContext)
        {
            this.DataContext = DataContext;

            StatusRepository = new StatusRepository(DataContext);
            ChucVuRepository = new ChucVuRepository(DataContext);
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
