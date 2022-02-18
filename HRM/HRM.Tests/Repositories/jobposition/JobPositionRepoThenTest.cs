using HRM.Entities;
using HRM.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.jobposition
{
    public partial class JobPositionRepoTest:CommonTests
    {
        public async Task Then_CreateJobPosition_Success()
        {
            var Output = DataContext.JobPosition.Where(x => x.Id == Input.Id).FirstOrDefault();
            Assert.AreEqual(Input.Name, Output.Name);
            Assert.AreEqual(Input.Code, Output.Code);
            Assert.AreEqual(Input.Used, Output.Used);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

        }

        public async Task Then_UpdateJobPosition_Success()
        {
            var Output = DataContext.JobPosition.Where(x => x.Id == Input.Id).FirstOrDefault();

            Assert.AreEqual(Input.Name, Output.Name);
            Assert.AreEqual(Input.Code, Output.Code);
            Assert.AreEqual(Input.Used, Output.Used);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(1, 1);
        }

        public async Task Then_DeleteJobPosition_Success()
        {
            var Output = DataContext.JobPosition.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Assert.AreEqual(InputCreate.DeletedAt.ToString(), Output.DeletedAt.ToString());
        }
        public async Task Then_GetListByName_Success()
        {
            string Name = "Job1";
            JobPositionFilter JobPositionFilter = new JobPositionFilter
            {
                Skip = 0,
                Take = 10,
                Name = new StringFilter { Equal = Name },
                Selects = JobPositionSelect.Name
            };

            int count = await repository.Count(JobPositionFilter);

            // Assert
            Assert.AreEqual(1, count);
        }
    }
}
