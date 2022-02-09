using System;
using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.candidate
{
    public class Candidate_CandidateDTO : DataDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public long? StatusId { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }

        public Candidate_DistrictDTO District { get; set; }
        public Candidate_ProvinceDTO Province { get; set; }
        public Candidate_VillageDTO Village { get; set; }
        public Candidate_StatusDTO Status { get; set; }
        public Candidate_CandidateDTO()
        {

        }
        public Candidate_CandidateDTO(Candidate Candidate)
        {
            Id = Candidate.Id;
            FullName = Candidate.FullName;
            Email = Candidate.Email;
            PhoneNumber = Candidate.PhoneNumber;
            Birthday = Candidate.Birthday;
            StatusId = Candidate.StatusId;
            DistrictId = Candidate.DistrictId;
            VillageId = Candidate.VillageId;
            ProvinceId = Candidate.ProvinceId;
            Status = new Candidate_StatusDTO(Candidate.Status);
            Village = new Candidate_VillageDTO(Candidate.Village);
            District = new Candidate_DistrictDTO(Candidate.District);
            Province = new Candidate_ProvinceDTO(Candidate.Province);
        }
    }

    public class Candidate_CandidateFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public DateFilter Birthday { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public CandidateOrder OrderBy { get; set; }
        public CandidateSelect Selects { get; set; }
    }
}