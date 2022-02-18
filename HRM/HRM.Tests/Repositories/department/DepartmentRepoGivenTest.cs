using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using TrueSight.Common;

namespace HRM.Tests.Repositories.department
{
    public partial class DepartmentRepoTest : CommonTests
    {
        private List<DepartmentDAO> DepartmentDAOs;
        private List<VillageDAO> VillageDAOs;
        private List<DistrictDAO> DistrictDAOs;
        private List<ProvinceDAO> ProvinceDAOs;
        private List<StatusDAO> StatusDAOs;


        private async Task LoadData()
        {
            string path = @"Repositories/department/import_dev.xlsx";
            await LoadExcel(path);
        }

        private async Task LoadExcel(string path)
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

                ExcelWorksheet wsDepartment = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(Department)).FirstOrDefault();
                if (wsDepartment != null)
                    await Given_Department(wsDepartment);
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
                Console.WriteLine(JsonConvert.SerializeObject(ProvinceDAO));
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

        private async Task Given_Department(ExcelWorksheet ExcelWorksheet)
        {
            this.DepartmentDAOs = new List<DepartmentDAO>();
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
            int PhoneNumberColumn = StartColumn + columns.IndexOf("PhoneNumber");
            int VillageIdColumn = StartColumn + columns.IndexOf("VillageId");
            int DistrictIdColumn = StartColumn + columns.IndexOf("DistrictId");
            int ProvinceIdColumn = StartColumn + columns.IndexOf("ProvinceId");
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
                DepartmentDAO DepartmentDAO = new DepartmentDAO();
                DepartmentDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                DepartmentDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                DepartmentDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();
                DepartmentDAO.PhoneNumber = ExcelWorksheet.Cells[row, PhoneNumberColumn].Value?.ParseString();
                DepartmentDAO.VillageId = ExcelWorksheet.Cells[row, VillageIdColumn].Value?.ParseLong();
                DepartmentDAO.DistrictId = ExcelWorksheet.Cells[row, DistrictIdColumn].Value?.ParseLong();
                DepartmentDAO.ProvinceId = ExcelWorksheet.Cells[row, ProvinceIdColumn].Value?.ParseLong();
                DepartmentDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                DepartmentDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                DepartmentDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                DepartmentDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                DepartmentDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                DepartmentDAOs.Add(DepartmentDAO);
                Console.WriteLine(JsonConvert.SerializeObject(DepartmentDAO));
            }
            await DataContext.Department.BulkMergeAsync(DepartmentDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }
    }

}