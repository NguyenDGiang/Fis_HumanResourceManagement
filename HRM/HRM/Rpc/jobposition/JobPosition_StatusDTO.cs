﻿using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.jobposition
{
    public class JobPosition_StatusDTO:DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public JobPosition_StatusDTO() { }
        public JobPosition_StatusDTO(Status Status)
        {
            this.Id = Status.Id;
            this.Code = Status.Code;
            this.Name = Status.Name;
        }
    }
    public class JobPosition_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StatusOrder OrderBy { get; set; }
        public StatusSelect Selects { get; set; }
    }
}
