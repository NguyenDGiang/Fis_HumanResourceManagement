using HRM.Entities;
using HRM.Helpers;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using System.Linq;

namespace HRM.Tests.Repositories.jobposition
{
    public partial class JobPositionRepoTest:CommonTests
    {
        JobPosition Input;
        JobPosition InputCreate;
        public async Task When_CreateJobPosition_Success()
        {
            Input = new JobPosition()
            {
                Name = "Job1",
                Code = "J",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(Input);
        }

        public async Task When_UpdateJobPosition_Success()
        {
            InputCreate = new JobPosition()
            {
                StatusId = 1,
                Name = "Job1",
                Code= "1",
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            var jobPosition = DataContext.JobPosition.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Input = new JobPosition()
            {
                Id = jobPosition.Id,
                StatusId = 1,
                Name = "Job1w",
                Code = "1",
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            await repository.Update(Input);
        }

        public async Task When_DeleteEmployee_Success()
        {
            InputCreate = new JobPosition
            {
                Name = "Job1",
                Code = "J",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            await repository.Delete(InputCreate);
        }

        public async Task When_GetListByName_Success()
        {
            InputCreate = new JobPosition
            {
                Name = "Job1",
                Code = "J",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            // Create Instance
            await repository.Create(InputCreate);
        }
    }
}
