using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechnicalTestApi.Models;

namespace TechnicalTestApi.Controllers
{

    [ApiController]
    [Route("funtions")]
    public class FunctionsControllers : ControllerBase
    {
       [HttpPost]
       [Route("remove-duplicates")]
       
       public dynamic RemoveDuplicates(Object jsonData)
        {


            Funtions data = JsonConvert.DeserializeObject<Funtions>(jsonData.ToString());
             
            List<string> Duplicates = new List<string>();

            foreach (var item in data.repeatedValues.ToArray()) { 
                
                bool check = Array.Exists(Duplicates.ToArray(), element => element == item);
                if (!check)
                {
                    Duplicates.Add(item);
                }
            } 

            return new { uniques = Duplicates};
        }






        
        [HttpPost]
        [Route("organize")]
        public dynamic Organize([FromBody] Object jsonData)
        {
            Week week = JsonConvert.DeserializeObject<Week>(jsonData.ToString());

            List<string> names = new List<string>();
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            names = GetNames(week.monday, names);
            names = GetNames(week.thursday, names);
            names = GetNames(week.wednesday, names);
            names = GetNames(week.tuesday, names);
            names = GetNames(week.friday, names);

            foreach (string item in names)
            {
                result.Add(item, GetListPerson(week, item));
            }

            return result;

            


           
        }
        
        public static List<string> GetListPerson(Week week, string name)
        {
            List<string> people = new List<string>();
            List<bool> contains = new List<bool>();
            contains.Add(week.monday.Contains(name));
            contains.Add(week.tuesday.Contains(name));
            contains.Add(week.wednesday.Contains(name));
            contains.Add(week.thursday.Contains(name));
            contains.Add(week.friday.Contains(name));
            

            List<string> weekList = new List<string>()
            {
                "monday",
                "thursday",
                "wednesday",
                "tuesday",
                "friday"
            };

            for (int i = 0; i < contains.ToArray().Length; i++)
            {
                if (contains.ToArray()[i])
                {
                    people.Add(weekList.ToArray()[i]);
                }
            }



            return people;
        }

        public static List<string> GetNames(List<string> day, List<string> listNames)
        {
            List<string> names = listNames;

            foreach (string item in day)
            {
                bool check = Array.Exists(names.ToArray(), element => element == item);
                if (!check)
                {
                    names.Add(item);
                }
            }


            return names;
        }


      

    }

    


}









