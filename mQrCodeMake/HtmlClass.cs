using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralClassLibrary
{
    public static class HtmlClass
    {
        public static Dictionary<string, string> ReadCfg(string PathToCfg = "conf.json")
        {
            Dictionary<string,string> confDic= new Dictionary<string,string>();
            string s;
            using (var f = new StreamReader(PathToCfg, Encoding.UTF8))            
                while ((s = f.ReadLine()) != null)
                {
                    string[] str = s.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    confDic[str[0]] = str[1];
                }
            
            return confDic;
        }

        public static Dictionary<string, string> ReadJSONcfg(string confPath)
        {
            var confDic = new Dictionary<string, string>();

            
            var json = File.ReadAllText(confPath, Encoding.UTF8);
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            
            foreach (var kvp in jsonObj)
            {
                confDic[kvp.Key] = kvp.Value["val"];
            }
            

            return confDic;
        }

        public static void WriteCfg(ref Dictionary<string, string> confDic, string PathToCfg, string key, string value)
        {
            using (var f = new StreamWriter(PathToCfg, append: true))
                f.WriteLine($"{key}={value}");
            confDic[key] = value;
        }
    }
}
