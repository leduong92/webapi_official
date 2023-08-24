using Newtonsoft.Json;
using System.Dynamic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace Infrastructure.HelperService
{
    public static class MapListHelpers
    {
        public static string MapListObjectToString<T>(List<T> listInputValue)
        {
            string selectString = "new(";
            var properties = listInputValue.GetType().GetGenericArguments()[0].GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Equals(properties.Last()))
                {
                    selectString += prop.Name;
                }
                else
                {
                    selectString += prop.Name + ",";
                }
            }
            selectString += ")";
            var dynamicResult = listInputValue.AsQueryable().Select(selectString).ToDynamicList();
            var jsonSetting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(dynamicResult, Formatting.None, jsonSetting);
        }
    }
}
