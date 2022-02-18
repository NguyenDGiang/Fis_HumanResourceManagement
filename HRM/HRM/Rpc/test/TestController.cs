using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HRM.Helpers;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRM.Rpc.test
{
    public class TestController : RpcController
    {
        private DataContext DataContext;
        public TestController(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        private static readonly HttpClient client = new HttpClient();

        [Route("api/test/get")]

        public async Task<int> Get()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;


            var response = await client.GetAsync("https://provinces.open-api.vn/api/?depth=3");

            var responseString = await response.Content.ReadAsStringAsync();
            JArray json1 = JsonConvert.DeserializeObject<JArray>(responseString);

            // System.Console.WriteLine(responseString);
            foreach (var provice in json1)
            {
                System.Console.WriteLine("tên tỉnh: " + provice["name"]);

                // System.Console.WriteLine("codename: " + provice["codename"]);
                ProvinceDAO provinceDAO = new ProvinceDAO();
                provinceDAO.Name = provice["name"].ToString();
                provinceDAO.Code = provice["codename"].ToString();
                provinceDAO.StatusId = 1;
                provinceDAO.Used = true;
                provinceDAO.CreatedAt = StaticParams.DateTimeNow;
                provinceDAO.UpdatedAt = StaticParams.DateTimeNow;
                provinceDAO.Used = true;

                DataContext.Province.Add(provinceDAO);
                DataContext.SaveChanges();
                foreach (var district in provice["districts"])
                {
                    // System.Console.WriteLine("--tên huyen: " + district["name"]);
                    // System.Console.WriteLine("--codename: " + district["codename"]);
                    DistrictDAO DistrictDAO = new DistrictDAO();
                    DistrictDAO.Name = district["name"].ToString();
                    DistrictDAO.Code = district["codename"].ToString();
                    DistrictDAO.StatusId = 1;
                    DistrictDAO.Used = true;
                    DistrictDAO.CreatedAt = StaticParams.DateTimeNow;
                    DistrictDAO.UpdatedAt = StaticParams.DateTimeNow;
                    DistrictDAO.Used = true;
                    DistrictDAO.ProvinceId = provinceDAO.Id;
                    DataContext.District.Add(DistrictDAO);
                    DataContext.SaveChanges();

                    List<VillageDAO> villageDAOs = new List<VillageDAO>();

                    foreach (var village in district["wards"])
                    {
                        VillageDAO VillageDAO = new VillageDAO();
                        VillageDAO.Name = village["name"].ToString();
                        VillageDAO.Code = village["codename"].ToString();
                        VillageDAO.StatusId = 1;
                        VillageDAO.Used = true;
                        VillageDAO.CreatedAt = StaticParams.DateTimeNow;
                        VillageDAO.UpdatedAt = StaticParams.DateTimeNow;
                        VillageDAO.Used = true;
                        VillageDAO.DistrictId = DistrictDAO.Id;

                        villageDAOs.Add(VillageDAO);
                    }
                    DataContext.Village.AddRange(villageDAOs);
                    await DataContext.SaveChangesAsync();

                }
                System.Console.WriteLine();
            }
            return 1;

        }

    }

}