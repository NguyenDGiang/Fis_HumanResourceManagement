﻿using HRM.Entities;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MInterviewResult
{
    public interface IInterviewResultService :IServiceScoped
    {
        Task<int> Count(InterviewResultFilter InterviewResultFilter);
        Task<List<InterviewResult>> List(InterviewResultFilter InterviewResultFilter);
        Task<InterviewResult> Get(long Id);
        Task<InterviewResult> Create(InterviewResult InterviewResult);
        Task<InterviewResult> Update(InterviewResult InterviewResult);
        Task<InterviewResult> Delete(InterviewResult InterviewResult);
        InterviewResultFilter ToFilter(InterviewResultFilter EmployeeFilter);
    }
    public class InterviewResultService:IInterviewResultService
    {
        private IUOW UOW;
        private IInterviewResultValidator InterviewResultValidator;
        public InterviewResultService(IUOW UOW, IInterviewResultValidator InterviewResultValidator)
        {
            this.UOW = UOW;
            this.InterviewResultValidator = InterviewResultValidator;
        }
        public async Task<int> Count(InterviewResultFilter InterviewResultFilter)
        {
            int result = await UOW.InterviewResultRepository.Count(InterviewResultFilter);
            return result;
        }

        public async Task<InterviewResult> Create(InterviewResult InterviewResult)
        {    if (!await InterviewResultValidator.Create(InterviewResult))
                 return InterviewResult;
            try
            {
                
                await UOW.InterviewResultRepository.Create(InterviewResult);
                InterviewResult = await UOW.InterviewResultRepository.Get(InterviewResult.Id);
                return InterviewResult;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("IEmployeeService: " + ex.Message);
            }
            return null;
        }

        public async Task<InterviewResult> Delete(InterviewResult InterviewResult)
        {
            if (!await InterviewResultValidator.Delete(InterviewResult))
                return InterviewResult;
            await UOW.InterviewResultRepository.Delete(InterviewResult);
            List<InterviewResult> InterviewResults = await UOW.InterviewResultRepository.List(new List<long> { InterviewResult.Id });
            InterviewResult = InterviewResults.FirstOrDefault();
            return InterviewResult;
        }

        public async Task<InterviewResult> Get(long Id)
        {
            InterviewResult InterviewResult = await UOW.InterviewResultRepository.Get(Id);
            if (InterviewResult == null)
                return null;
            return InterviewResult;
        }

        public async Task<List<InterviewResult>> List(InterviewResultFilter InterviewResultFilter)
        {
            List<InterviewResult> InterviewResults = await UOW.InterviewResultRepository.List(InterviewResultFilter);
            return InterviewResults;
        }
        public InterviewResultFilter ToFilter(InterviewResultFilter filter)
        {

            return filter;
        }

        public async Task<InterviewResult> Update(InterviewResult InterviewResult)
        {
            if (!await InterviewResultValidator.Update(InterviewResult))
                return InterviewResult;
            try
            {
                var oldData = await UOW.InterviewResultRepository.Get(InterviewResult.Id);
                await UOW.InterviewResultRepository.Update(InterviewResult);
                List<InterviewResult> InterviewResults = await UOW.InterviewResultRepository.List(new List<long> { InterviewResult.Id });
                InterviewResult = InterviewResults.FirstOrDefault();
                return InterviewResult;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("IInterviewResulteService" + ex.Message);
            }
            return null;

        }

       
    }
}
