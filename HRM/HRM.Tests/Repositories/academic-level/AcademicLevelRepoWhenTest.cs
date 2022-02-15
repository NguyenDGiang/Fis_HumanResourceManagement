using HRM.Entities;
using HRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.academic_level
{
    public partial class AcademicLevelRepoTest : CommonTests
    {
        AcademicLevelFilter filter;
        AcademicLevel AcademicLevel;
        List<long> Ids;
        List<AcademicLevel> AcademicLevels;
        int count;
        private async Task When_CountAcademicLevel_Success()
        {
            filter = new AcademicLevelFilter()
            {
                Name = new StringFilter()
                {
                    Contain = "a"
                }
            };
            count = await repository.Count(filter);
        }

        private async Task When_CreateAcademicLevel_Success()
        {
            AcademicLevel = new AcademicLevel
            {
                Id = 0,
                Name = "Test create â",
                StatusId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
            };

            await repository.Create(AcademicLevel);
        }
        private async Task When_GetAcademicLevel_Success()
        {
            Ids = new List<long>() { 1 };
            long id = Ids.FirstOrDefault();
            AcademicLevel = await repository.Get(id);
        }
        private async Task When_DeleteAcademicLevel_Success()
        {
            AcademicLevel = new AcademicLevel
            {
                Id = 2
            };
            await repository.Delete(AcademicLevel);
        }
        private async Task When_BulkDeleteAcademicLevel_Success()
        {
            List<long> Ids = new List<long>() { 1, 2, 3 };
            AcademicLevels = Ids.Select(id => new AcademicLevel
            {
                Id = id
            }
            ).ToList();

            await repository.BulkDelete(AcademicLevels);
        }

        private async Task When_ListOrderByAcademicLevel_Success()
        {
            filter = new AcademicLevelFilter
            {
                OrderBy = AcademicLevelOrder.Name,
                OrderType = OrderType.DESC,
                Take = int.MaxValue,
                Skip = 0,
                Selects = AcademicLevelSelect.ALL
            };
            AcademicLevels = await repository.List(filter);
        }

        private async Task When_ListFilterAcademicLevel_Success()
        {
            filter = new AcademicLevelFilter
            {
                Name = new StringFilter
                {
                    Contain = "x"
                },
                Take = int.MaxValue,
                Skip = 0,
                Selects = AcademicLevelSelect.ALL
            };
            AcademicLevels = await repository.List(filter);
        }
    }
}
