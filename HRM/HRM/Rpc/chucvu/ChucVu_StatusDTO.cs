using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.chucvu
{
    public class ChucVu_StatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ChucVu_StatusDTO()
        {

        }
        public ChucVu_StatusDTO(Status Status)
        {
            this.Id = Status.Id;
            this.Name = Status.Name;
            this.Code = Status.Code;
        }
    }

    public class ChucVu_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public StatusOrder OrderBy { get; set; }
    }
}