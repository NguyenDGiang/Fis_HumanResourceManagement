using HRM.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.interviewresult
{
    public partial class InterviewResultRepoTest:CommonTests
    {
        public async Task Then_CreateInterviewResult_Success()
        {
            var Output = DataContext.InterviewResult.Where(x => x.Id == Input.Id).FirstOrDefault();
            Assert.AreEqual(Input.CandidateId, Output.CandidateId);
            Assert.AreEqual(Input.InterviewTime, Output.InterviewTime);
            Assert.AreEqual(Input.Used, Output.Used);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.PassInterview, Output.PassInterview);
            Assert.AreEqual(Input.BeginJobTime, Output.BeginJobTime);
            Assert.AreEqual(Input.TrialTime, Output.TrialTime);
            Assert.AreEqual(Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

        }

        public async Task Then_UpdateInterviewResult_Success()
        {
            var Output = DataContext.InterviewResult.Where(x => x.Id == Input.Id).FirstOrDefault();

            Assert.AreEqual(Input.CandidateId, Output.CandidateId);
            Assert.AreEqual(Input.InterviewTime, Output.InterviewTime);
            Assert.AreEqual(Input.Used, Output.Used);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.PassInterview, Output.PassInterview);
            Assert.AreEqual(Input.BeginJobTime, Output.BeginJobTime);
            Assert.AreEqual(Input.TrialTime, Output.TrialTime);
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(1, 1);
        }

        public async Task Then_DeleteInterviewResult_Success()
        {
            var Output = DataContext.InterviewResult.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Assert.AreEqual(InputCreate.DeletedAt.ToString(), Output.DeletedAt.ToString());
        }
       
    }
}
