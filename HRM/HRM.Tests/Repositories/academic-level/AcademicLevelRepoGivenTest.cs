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

namespace HRM.Tests.Repositories.academic_level
{
    public partial class AcademicLevelRepoTest : CommonTests
    {
        private List<AcademicLevelDAO> AcademicLevelDAOs;
        private async Task LoadData()
        {
            string path = @"Repositories/academic-level/import_dev.xlsx";
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
                ExcelWorksheet wsAcademicLevel = excelPackage.Workbook.Worksheets.Where(x => x.Name == nameof(AcademicLevel)).FirstOrDefault();
                if (wsAcademicLevel != null)
                    await Given_AcademicLevel(wsAcademicLevel);
            }
        }

        private async Task Given_Status(ExcelWorksheet ExcelWorksheet)
        {
            StatusDAO StatusDAO_INACTIVE = new StatusDAO
            {
                Id = 0,
                Code = "INACTIVE",
                Name = "Dừng hoạt động",
            };
            DataContext.Status.Add(StatusDAO_INACTIVE);
            StatusDAO StatusDAO_ACTIVE = new StatusDAO
            {
                Id = 0,
                Code = "ACTIVE",
                Name = "Hoạt động",
            };
            DataContext.Status.Add(StatusDAO_ACTIVE);

            await DataContext.SaveChangesAsync();
        }

        private async Task Given_AcademicLevel(ExcelWorksheet ExcelWorksheet)
        {
            this.AcademicLevelDAOs = new List<AcademicLevelDAO>();
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
                AcademicLevelDAO AcademicLevelDAO = new AcademicLevelDAO();
                AcademicLevelDAO.Id = ExcelWorksheet.Cells[row, IdColumn].Value?.ParseLong() ?? 0;
                AcademicLevelDAO.Name = ExcelWorksheet.Cells[row, NameColumn].Value?.ParseString();
                AcademicLevelDAO.Code = ExcelWorksheet.Cells[row, CodeColumn].Value?.ParseString();
                AcademicLevelDAO.StatusId = ExcelWorksheet.Cells[row, StatusIdColumn].Value?.ParseLong();
                AcademicLevelDAO.Used = ExcelWorksheet.Cells[row, UsedColumn].Value.ParseBool();
                AcademicLevelDAO.CreatedAt = ExcelWorksheet.Cells[row, CreatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                AcademicLevelDAO.UpdatedAt = ExcelWorksheet.Cells[row, UpdatedAtColumn].Value?.ParseDateTime() ?? StaticParams.DateTimeNow;
                AcademicLevelDAO.DeletedAt = ExcelWorksheet.Cells[row, DeletedAtColumn].Value?.ParseNullDateTime();

                AcademicLevelDAOs.Add(AcademicLevelDAO);
                // Console.WriteLine(JsonConvert.SerializeObject(AcademicLevelDAO));
            }
            await DataContext.AcademicLevel.BulkMergeAsync(AcademicLevelDAOs, options =>
            {
                options.MergeKeepIdentity = true;
                options.ColumnPrimaryKeyExpression = c => c.Id;
            });
        }
    }

}