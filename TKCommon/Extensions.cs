using Newtonsoft.Json;

namespace DT.Common
{
    public static class MyExtensions
    {
        public static string ToJson(this object o)
        {

            var s = new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            //, MaxDepth = 2
                        };

            return JsonConvert.SerializeObject(o,
                
               s);
        }
    }
}