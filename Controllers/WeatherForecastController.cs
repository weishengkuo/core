using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;

namespace core.Controllers
{
    // UNBOXING 
    // BONING
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private personInformation data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            data = new personInformation() { 
            };
        }

        [HttpGet]
        public IEnumerable<personInformation> Get()
        {
            List<personInformation> list = new List<personInformation>
            {
                new personInformation()
                {
                    name = "王小明",
                    nickName = "Mike",
                    phoneNumber = "0982335675",
                    interesting = "籃球",
                    jobTitle = "業務員"
                },
               new personInformation()
                {
                    name = "孫小美",
                    nickName = "Jane",
                    phoneNumber = "0922367885",
                    interesting = "瑜珈",
                    jobTitle = "行政人員"
                },
            };

            //List<personInformation> demo0 = 
            //    list.Where(w => !w.name.Equals("王小明")).ToList();
          
            return list;
        }

        [HttpPost]
        public IEnumerable<personInformation> Post(string data)
        {
            Console.WriteLine(data);



            List<personInformation> list = new List<personInformation>
            {
                new personInformation()
                {
                    name = "王小明",
                    nickName = "Mike",
                    phoneNumber = "0982335675",
                    interesting = "籃球",
                    jobTitle = "業務員"
                },
               new personInformation()
                {
                    name = "孫小美",
                    nickName = "Jane",
                    phoneNumber = "0922367885",
                    interesting = "瑜珈",
                    jobTitle = "行政人員"
                },
            };

           

            return list;
        }

        public static personInformation GetData(string name = null ,string nickname = null, string phonenumber = null, string interest = null, string job = null)
        {
            var list = new List<personInformation>();
            var result = new personInformation();

            result.name = name;
            result.nickName = nickname;
            result.phoneNumber = phonenumber;
            result.interesting = interest;
            result.jobTitle = job;
            
            return result;
        }

        //[HttpPost]
        //public HttpResponseMessage Post([FromBody] string DATA)
        //{
        //    string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

        //    JObject jo = JObject.Parse(DATA);

        //    string l_type = jo["TYPE"].ToString();
        //    string param1 = jo["PARAM1"].ToString();

        //    var result = new
        //    {
        //        STATUS = true,
        //        MSG = "成功",
        //    };

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}
    }
}
