using HRM.Entities;
using HRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.interviewresult
{
    public partial class InterviewResultRepoTest:CommonTests
    {
        InterviewResult Input;
        InterviewResult InputCreate;
        public async Task When_CreateInterviewResult_Success()
        {
            Input = new InterviewResult()
            {
                CandidateId =1,
                InterviewTime = new DateTime(2022,03,03),
                PassInterview = true,
                BeginJobTime = new DateTime(2022,03,03),
                TrialTime = new DateTime(2022,03,03),
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(Input);
        }

        public async Task When_UpdateInterviewResult_Success()
        {
            InputCreate = new InterviewResult()
            {
                CandidateId = 1,
                InterviewTime = new DateTime(2022,03,03),
                PassInterview = true,
                BeginJobTime = new DateTime(2022,03,03),
                TrialTime = new DateTime(2022,03,03),
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            var interviewResult = DataContext.InterviewResult.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Input = new InterviewResult()
            {
                Id = interviewResult.Id,    
                CandidateId = interviewResult.CandidateId,
                InterviewTime = new DateTime(2022,05,03),
                PassInterview = true,
                BeginJobTime = new DateTime(2022,05,03),
                TrialTime = new DateTime(2022,05,03),
                Used = false,
                StatusId = interviewResult.StatusId,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            await repository.Update(Input);
        }

        public async Task When_DeleteEmployee_Success()
        {
            InputCreate = new InterviewResult
            {
                CandidateId = 1,
                InterviewTime = new DateTime(2022,03,03),
                PassInterview = true,
                BeginJobTime = new DateTime(2022,03, 03),
                TrialTime = new DateTime(2022,03,03),
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            await repository.Delete(InputCreate);
        }

        
    }
}
