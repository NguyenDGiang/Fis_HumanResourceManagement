using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MStatus
{
    public interface IStatusService : IServiceScoped
    {
        Task<int> Count(StatusFilter StatusFilter);
        Task<List<Status>> List(StatusFilter StatusFilter);
    }
    public class StatusService : IStatusService
    {
        private IUOW UOW;

        public StatusService(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<int> Count(StatusFilter StatusFilter)
        {
            try
            {
                int result = await UOW.StatusRepository.Count(StatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return 0;
        }

        public async Task<List<Status>> List(StatusFilter StatusFilter)
        {
            try
            {
                List<Status> Statuss = await UOW.StatusRepository.List(StatusFilter);
                return Statuss;
            }
            catch (Exception ex)
            {
               System.Console.WriteLine(ex);
            }
            return null;
        }
    }
}