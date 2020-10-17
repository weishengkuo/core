using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  
    public class DemoController : ControllerBase
    {
        // GET: api/Demo
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Demo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Demo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Demo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
       
        public IEnumerable<PersonInformation> PersonData()
        {
            PersonInformation[] list =
            {
                new PersonInformation()
                {
                    PersonID = 1,
                    Name = "王小明",
                    NickName = "Mike",
                    PhoneNumber = "0982335675",
                    Address = "台北市新店區九龍街251號3樓之一號",                    
                    JobTitle = "業務員"
                },

                new PersonInformation()
                {
                    PersonID = 2,
                    Name = "孫小美",
                    NickName = "Jane",
                    PhoneNumber = "0922367885",
                    Address = "台北市山重區中正路581號5樓之一號",                   
                    JobTitle = "行政人員"
                },

                new PersonInformation()
                {
                    PersonID = 3,
                    Name = "陳名奇",
                    NickName = "Bob",
                    PhoneNumber = "0926335897",
                    Address = "台北市蘆洲區光明路2841號5樓之一號",                   
                    JobTitle = "電器銷售人員"
                },

            };

            return list;
        }

        public IEnumerable<Interest> InterestData()
        {
            Interest[] list =
            {
                new Interest()
                {
                   PersonID = 1,
                   InterestItem ="籃球"
                },

                new Interest()
                {
                   PersonID = 2,
                   InterestItem ="瑜珈"
                },

                new Interest()
                {
                   PersonID = 2,
                   InterestItem ="桌球"
                },

            };

            return list;
        }

        [HttpGet]
        public IEnumerable<PersonInforDetail> Get()
        {
            List<PersonInformation> personInfo = PersonData().ToList();

            List<Interest> interest = InterestData().ToList();

            var result = from p in personInfo
                         join i in interest
                         on p.PersonID equals i.PersonID
                         select new PersonInforDetail
                         {
                             Name = p.Name,
                             NickName = p.NickName,
                             PhoneNumber = p.PhoneNumber,
                             Address = p.Address,
                             Interesting = i.InterestItem,
                             JobTitle = p.JobTitle
                         };

            return result;

        }

        [HttpGet]
        public Dictionary<int, Nationality> List()
        {
            Dictionary<int, Nationality> dictionary = new Dictionary<int, Nationality>();

            dictionary.Add(2, new Nationality { Continent = "亞洲", Country = "中國" });
            dictionary.Add(3, new Nationality { Continent = "亞洲", Country = "台灣" });
            dictionary.Add(5, new Nationality { Continent = "亞洲", Country = "日本" });
            dictionary.Add(1, new Nationality { Continent = "亞洲", Country = "韓國" });
            dictionary.Add(4, new Nationality { Continent = "歐洲", Country = "瑞士" });
            dictionary.Add(7, new Nationality { Continent = "歐洲", Country = "荷蘭" });
            dictionary.Add(6, new Nationality { Continent = "歐洲", Country = "法國" });
            dictionary.Add(8, new Nationality { Continent = "北美洲", Country = "美國" });
            dictionary.Add(9, new Nationality { Continent = "南美洲", Country = "智利" });

            return dictionary;
        }

    }
}
