using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.academic_level
{
    public class AcademicLevel_StatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public AcademicLevel_StatusDTO()
        {

        }
        public AcademicLevel_StatusDTO(Status Status)
        {
            Id = Status.Id;
            Name = Status.Name;
            Code = Status.Code;
        }
    }

    public class AcademicLevel_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StatusOrder OrderBy { get; set; }


    }
}