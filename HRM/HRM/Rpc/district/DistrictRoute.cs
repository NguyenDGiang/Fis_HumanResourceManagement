﻿using TrueSight.Common;
using HRM.Entities;
using HRM.Enums;
using HRM.Services.MDistrict;
using HRM.Services.MProvince;
using HRM.Services.MStatus;
using HRM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HRM.Rpc.district
{
    [DisplayName("Quận huyện")]
    public class DistrictRoute : Root
    {
        public const string Parent = Module + "/official";
        public const string Master = Module + "/official/district/district-master";
        public const string Detail = Module + "/official/district/district-detail/*";
        private const string Default = Rpc + Module + "/district";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string BulkDelete = Default + "/bulk-delete";

        public const string FilterListProvince = Default + "/filter-list-province";
        public const string FilterListStatus = Default + "/filter-list-status";

        public const string SingleListProvince = Default + "/single-list-province";
        public const string SingleListStatus = Default + "/single-list-status";

        private static List<string> FilterList = new List<string> {
            FilterListProvince, FilterListStatus,
        };
        private static List<string> SingleList = new List<string> {
            SingleListProvince, SingleListStatus,
        };
        private static List<string> CountList = new List<string>
        {

        };
        public static Dictionary<string, IEnumerable<string>> Action = new Dictionary<string, IEnumerable<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                Get,
                }.Concat(FilterList)
            },
            { "Thêm mới", new List<string> {
                Parent,
                Master, Count, List,
                Get,
                Detail, Create,
                }.Concat(FilterList).Concat(SingleList)
            },
            { "Sửa", new List<string> {
                Parent,
                Master, Count, List,
                Get,
                Detail, Update,
                }.Concat(FilterList).Concat(SingleList)
            },
            { "Xoá", new List<string> {
                Parent,
                Master, Count, List,
                Get,
                Delete
                }.Concat(FilterList)
            },
             { "Xoá nhiều", new List<string> {
                Parent,
                Master, Count, List,
                Get,
                BulkDelete
                }.Concat(FilterList)
            },
        };

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
