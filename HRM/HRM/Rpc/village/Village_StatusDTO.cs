using TrueSight.Common;
using HRM.Entities;

namespace HRM.Rpc.village
{
    public class Village_StatusDTO : DataDTO
    {

        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


        public Village_StatusDTO() { }
        public Village_StatusDTO(Status Status)
        {

            this.Id = Status.Id;

            this.Code = Status.Code;

            this.Name = Status.Name;

        }
    }

    public class Village_StatusFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public StatusOrder OrderBy { get; set; }
    }
}
