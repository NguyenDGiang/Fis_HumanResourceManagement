using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.academic_level
{
    public class AcademicLevel_AcademicLevelDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool? Used { get; set; }
        public AcademicLevel_StatusDTO Status { get; set; }


        public AcademicLevel_AcademicLevelDTO()
        {

        }
        public AcademicLevel_AcademicLevelDTO(AcademicLevel AcademicLevel)
        {
            Id = AcademicLevel.Id;
            Name = AcademicLevel.Name;
            Code = AcademicLevel.Code;
            StatusId = AcademicLevel.StatusId;
            Used = AcademicLevel.Used;
            Status = new AcademicLevel_StatusDTO(AcademicLevel.Status);
        }
    }

    public class AcademicLevel_AcademicLevelFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public AcademicLevelOrder OrderBy { get; set; }

    }
}