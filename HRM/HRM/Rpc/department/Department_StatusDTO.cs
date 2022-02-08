using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class Department_StatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Department_StatusDTO()
        {

        }
        public Department_StatusDTO(Status Status)
        {
            Id = Status.Id;
            Code = Status.Code;
            Name = Status.Name;
        }
    }
    public class Department_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StatusOrder OrderBy { get; set; }

    }

}