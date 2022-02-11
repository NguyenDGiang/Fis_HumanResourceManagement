using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.chucvu
{
    public class ChucVu_ChucVuDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }

        public ChucVu_StatusDTO Status { get; set; }

        public ChucVu_ChucVuDTO() { }
        public ChucVu_ChucVuDTO(ChucVu ChucVu)
        {
            this.Id = ChucVu.Id;
            this.Name = ChucVu.Name;
            this.Code = ChucVu.Code;
            this.StatusId = ChucVu.StatusId;
            this.Used = ChucVu.Used;
            this.Status = ChucVu.Status == null ? null : new ChucVu_StatusDTO(ChucVu.Status);
        }
    }

    public class ChucVu_ChucVuFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public ChucVuOrder OrderBy { get; set; }
        public ChucVuSelect Selects { get; set; }
        public string Search { get; set; }
    }
}