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

        private PersonInformation data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            data = new PersonInformation() { 
            };
        }

        [HttpGet]
        public IEnumerable<PersonInformation> Get()
        {
            List<PersonInformation> list = new List<PersonInformation>
            {
                new PersonInformation()
                {
                    Name = "王小明",
                    NickName = "Mike",
                    PhoneNumber = "0982335675",
                    //Interesting = "籃球",
                    JobTitle = "業務員"
                },
               new PersonInformation()
                {
                    Name = "孫小美",
                    NickName = "Jane",
                    PhoneNumber = "0922367885",
                    //Interesting = "瑜珈",
                    JobTitle = "行政人員"
                },
            };

            //List<personInformation> demo0 = 
            //    list.Where(w => !w.name.Equals("王小明")).ToList();
          
            return list;
        }

        [HttpPost]
        public IEnumerable<PersonInformation> Post(string data)
        {
            
            List<PersonInformation> list = new List<PersonInformation>
            {
                new PersonInformation()
                {
                    Name = "王小明",
                    NickName = "Mike",
                    PhoneNumber = "0982335675",
                    Address = "台北市新店區九龍街251號3樓之一號",
                    //Interesting = "籃球",
                    JobTitle = "業務員"
                },
               new PersonInformation()
                {
                    Name = "孫小美",
                    NickName = "Jane",
                    PhoneNumber = "0922367885",
                    //Interesting = "瑜珈",
                    JobTitle = "行政人員"
                },
            };
            
            return list;
        }
       
    }
}
