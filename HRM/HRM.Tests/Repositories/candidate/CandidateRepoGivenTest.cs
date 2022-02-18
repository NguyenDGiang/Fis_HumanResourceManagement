using Bogus;
using HRM.Helpers;
using HRM.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using HRM.Entities;
using TrueSight.Common;

namespace HRM.Tests.Repositories.candidate
{

    public partial class CandidateRepoTest : CommonTests
    {
        List<CandidateDAO> CandidateDAOs;
        List<StatusDAO> StatusDAOs;
        List<ProvinceDAO> ProvinceDAOs;
        List<DistrictDAO> DistrictDAOs;
        List<VillageDAO> VillageDAOs;
        public async Task LoadData()
        {
            // string path = @"D:\FIS\laptrinh\HRM.BE\HRM\HRM.Tests\Repositories\candidate\import_dev.xlsx";
            string path = @"Repositories/candidate/import_dev.xlsx";

            await LoadExcel(path);

        }

        public async Task LoadExcel(string path)
        {



            byte[] array = File.ReadAllBytes(path);
            MemoryStream MemoryStream = new MemoryStream(array);
            using (ExcelPackage excelPackage = new ExcelPackage(MemoryStream))
            {
                ExcelWorksheet wsStatus = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(Status)).FirstOrDefault();
                if (wsStatus != null)
                    await Given_Status(wsStatus);
                ExcelWorksheet wsProvince = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(Province)).FirstOrDefault();
                if (wsProvince != null)
                    await Given_Provice(wsProvince);
                ExcelWorksheet wsDistrict = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(District)).FirstOrDefault();
                if (wsDistrict != null)
                    await Given_District(wsDistrict);
                ExcelWorksheet wsVillage = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(Village)).FirstOrDefault();
                if (wsVillage != null)
                    await Given_Village(wsVillage);

                ExcelWorksheet wsCandidate = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(Candidate)).FirstOrDefault();
                if (wsCandidate != null)
                    await Given_Candidate(wsCandidate);
            }
        }

        private async Task Given_Status(ExcelWorksheet ExcelWorksheet)
        {
            // StatusDAO StatusDAO_INACTIVE = new StatusDAO
            // {
            //     Id = 0,
            //     Code = "INACTIVE",
            //     Name = "Dừng hoạt động",
            // };
            // DataContext.Status.Add(StatusDAO_INACTIVE);
            // StatusDAO StatusDAO_ACTIVE = new StatusDAO
            // {
            //     Id = 0,
            //     Code = "ACTIVE",
            //     Name = "Hoạt động",
            // };
            // DataContext.Status.Add(StatusDAO_ACTIVE);

            // await DataContext.SaveChangesAsync();

            this.StatusDAOs = new List<StatusDAO>();
            int StartColumn = 1;
            int StartRow = 1;
            List<string> columns = new List<string>();
            for (int column = StartColumn; column <= ExcelWorksheet.Dimension.End.Column; column++)
            {
                string columnName = ExcelWorksheet.Cells[StartRow, column].Value?.ToString() ?? "";
                columns.Add(columnName);
            }
            int IdColumn = StartColumn + columns.IndexOf("Id");
            int NameColumn = StartColumn + columns.IndexOf("Name");
            int CodeColumn = StartColumn + columns.IndexOf("Code");

            for (int row = StartRow + 1; row <= ExcelWorksheet.Dimension.End.Row; row++)
            {
                if (ExcelWorksheet.Cells[row, StartColumn].Value == null)
                    continue;
                if (ExcelWorksheet.Cells[row, StartColumn].Value.ToString() == "END")
                    break;
                StatusDAO StatusDAO = new StatusDAO();
                StatusDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                StatusDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                StatusDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();


                StatusDAOs.Add(StatusDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(StatusDAO));
            }
            await DataContext.Status.BulkMergeAsync(StatusDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }
        private async Task Given_Provice(ExcelWorksheet ExcelWorksheet)
        {
            this.ProvinceDAOs = new List<ProvinceDAO>();
            int StartColumn = 1;
            int StartRow = 1;
            List<string> columns = new List<string>();
            for (int column = StartColumn; column <= ExcelWorksheet.Dimension.End.Column; column++)
            {
                string columnName = ExcelWorksheet.Cells[StartRow, column].Value?.ToString() ?? "";
                columns.Add(columnName);
            }
            int IdColumn = StartColumn + columns.IndexOf("Id");
            int NameColumn = StartColumn + columns.IndexOf("Name");
            int CodeColumn = StartColumn + columns.IndexOf("Code");
            int StatusIdColumn = StartColumn + columns.IndexOf("StatusId");
            int UsedColumn = StartColumn + columns.IndexOf("Used");
            int CreatedAtColumn = StartColumn + columns.IndexOf("CreatedAt");
            int UpdatedAtColumn = StartColumn + columns.IndexOf("UpdatedAt");
            int DeletedAtColumn = StartColumn + columns.IndexOf("DeletedAt");


            for (int row = StartRow + 1; row <= ExcelWorksheet.Dimension.End.Row; row++)
            {
                if (ExcelWorksheet.Cells[row, StartColumn].Value == null)
                    continue;
                if (ExcelWorksheet.Cells[row, StartColumn].Value.ToString() == "END")
                    break;
                ProvinceDAO ProvinceDAO = new ProvinceDAO();
                ProvinceDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                ProvinceDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                ProvinceDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();
                ProvinceDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                ProvinceDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                ProvinceDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                ProvinceDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                ProvinceDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                ProvinceDAOs.Add(ProvinceDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(ProvinceDAO));
            }
            await DataContext.Province.BulkMergeAsync(ProvinceDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }
        private async Task Given_District(ExcelWorksheet ExcelWorksheet)
        {
            this.DistrictDAOs = new List<DistrictDAO>();
            int StartColumn = 1;
            int StartRow = 1;
            List<string> columns = new List<string>();
            for (int column = StartColumn; column <= ExcelWorksheet.Dimension.End.Column; column++)
            {
                string columnName = ExcelWorksheet.Cells[StartRow, column].Value?.ToString() ?? "";
                columns.Add(columnName);
            }
            int IdColumn = StartColumn + columns.IndexOf("Id");
            int NameColumn = StartColumn + columns.IndexOf("Name");
            int CodeColumn = StartColumn + columns.IndexOf("Code");
            int StatusIdColumn = StartColumn + columns.IndexOf("StatusId");
            int UsedColumn = StartColumn + columns.IndexOf("Used");
            int ProvinceIdColumn = StartColumn + columns.IndexOf("ProvinceId");
            int CreatedAtColumn = StartColumn + columns.IndexOf("CreatedAt");
            int UpdatedAtColumn = StartColumn + columns.IndexOf("UpdatedAt");
            int DeletedAtColumn = StartColumn + columns.IndexOf("DeletedAt");


            for (int row = StartRow + 1; row <= ExcelWorksheet.Dimension.End.Row; row++)
            {
                if (ExcelWorksheet.Cells[row, StartColumn].Value == null)
                    continue;
                if (ExcelWorksheet.Cells[row, StartColumn].Value.ToString() == "END")
                    break;
                DistrictDAO DistrictDAO = new DistrictDAO();
                DistrictDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                DistrictDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                DistrictDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();
                DistrictDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                DistrictDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                DistrictDAO.ProvinceId = ExcelWorksheet.Cells[row, ProvinceIdColumn].Value.ParseLong();
                DistrictDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                DistrictDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                DistrictDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                DistrictDAOs.Add(DistrictDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(DistrictDAO));
            }
            await DataContext.District.BulkMergeAsync(DistrictDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }

        private async Task Given_Village(ExcelWorksheet ExcelWorksheet)
        {
            this.VillageDAOs = new List<VillageDAO>();
            int StartColumn = 1;
            int StartRow = 1;
            List<string> columns = new List<string>();
            for (int column = StartColumn; column <= ExcelWorksheet.Dimension.End.Column; column++)
            {
                string columnName = ExcelWorksheet.Cells[StartRow, column].Value?.ToString() ?? "";
                columns.Add(columnName);
            }
            int IdColumn = StartColumn + columns.IndexOf("Id");
            int NameColumn = StartColumn + columns.IndexOf("Name");
            int CodeColumn = StartColumn + columns.IndexOf("Code");
            int StatusIdColumn = StartColumn + columns.IndexOf("StatusId");
            int UsedColumn = StartColumn + columns.IndexOf("Used");
            int DistrictIdColumn = StartColumn + columns.IndexOf("DistrictId");
            int CreatedAtColumn = StartColumn + columns.IndexOf("CreatedAt");
            int UpdatedAtColumn = StartColumn + columns.IndexOf("UpdatedAt");
            int DeletedAtColumn = StartColumn + columns.IndexOf("DeletedAt");


            for (int row = StartRow + 1; row <= ExcelWorksheet.Dimension.End.Row; row++)
            {
                if (ExcelWorksheet.Cells[row, StartColumn].Value == null)
                    continue;
                if (ExcelWorksheet.Cells[row, StartColumn].Value.ToString() == "END")
                    break;
                VillageDAO VillageDAO = new VillageDAO();
                VillageDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                VillageDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                VillageDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();
                VillageDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                VillageDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                VillageDAO.DistrictId = ExcelWorksheet.Cells[row, DistrictIdColumn].Value.ParseLong();
                VillageDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                VillageDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                VillageDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                VillageDAOs.Add(VillageDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(VillageDAO));
            }
            await DataContext.Village.BulkMergeAsync(VillageDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }

        private async Task Given_Candidate(ExcelWorksheet ExcelWorksheet)
        {

            this.CandidateDAOs = new List<CandidateDAO>();
            int StartColumn = 1;
            int StartRow = 1;
            List<string> columns = new List<string>();
            for (int column = StartColumn; column <= ExcelWorksheet.Dimension.End.Column; column++)
            {
                string columnName = ExcelWorksheet.Cells[StartRow, column].Value?.ToString() ?? "";
                columns.Add(columnName);
            }
            int IdColumn = StartColumn + columns.IndexOf("Id");
            int FullNameColumn = StartColumn + columns.IndexOf("FullName");
            int EmailColumn = StartColumn + columns.IndexOf("Email");
            int PhoneNumberColumn = StartColumn + columns.IndexOf("PhoneNumber");
            int BirthdayColumn = StartColumn + columns.IndexOf("Birthday");
            int StatusIdColumn = StartColumn + columns.IndexOf("StatusId");
            int UsedColumn = StartColumn + columns.IndexOf("Used");
            int VillageIdColumn = StartColumn + columns.IndexOf("VillageId");
            int DistrictIdColumn = StartColumn + columns.IndexOf("DistrictId");
            int ProvinceIdColumn = StartColumn + columns.IndexOf("ProvinceId");
            int CreatedAtColumn = StartColumn + columns.IndexOf("CreatedAt");
            int UpdatedAtColumn = StartColumn + columns.IndexOf("UpdatedAt");
            int DeletedAtColumn = StartColumn + columns.IndexOf("DeletedAt");
            for (int row = StartRow + 1; row <= ExcelWorksheet.Dimension.End.Row; row++)
            {
                if (ExcelWorksheet.Cells[row, StartColumn].Value == null)
                    continue;
                if (ExcelWorksheet.Cells[row, StartColumn].Value.ToString() == "END")
                    break;
                CandidateDAO CandidateDAO = new CandidateDAO();
                CandidateDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                CandidateDAO.FullName = ExcelWorksheet.Cells[row, FullNameColumn].Value?.ParseString();
                CandidateDAO.Email = ExcelWorksheet.Cells[row, EmailColumn].Value?.ParseString();
                CandidateDAO.PhoneNumber = ExcelWorksheet.Cells[row, PhoneNumberColumn].Value?.ParseString();
                CandidateDAO.Birthday = ExcelWorksheet.Cells[row, BirthdayColumn].Value.ParseDateTime();
                CandidateDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                CandidateDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                CandidateDAO.VillageId = ExcelWorksheet.Cells[row, VillageIdColumn].Value?.ParseLong();
                CandidateDAO.DistrictId = ExcelWorksheet.Cells[row, DistrictIdColumn].Value?.ParseLong();
                CandidateDAO.ProvinceId = ExcelWorksheet.Cells[row, ProvinceIdColumn].Value?.ParseLong();
                CandidateDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                CandidateDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                CandidateDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                CandidateDAOs.Add(CandidateDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(CandidateDAO));
            }
            await DataContext.Candidate.BulkMergeAsync(CandidateDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }
    }
}
