using Newtonsoft.Json;
using QrCodeMakelib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        //переделай функцию так что бы возвращаемое значение было List<Options>
        public static List<Options> ReadJSONcfg(string confPath)
        {
            var optionsList = new List<Options>();

            var json = File.ReadAllText(confPath, Encoding.UTF8);
            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            foreach (var kvp in jsonObj)
            {
                var option = new Options
                {
                    name= kvp.Key,
                    val = kvp.Value["val"]
                };
                optionsList.Add(option);
            }

            return optionsList;
        }

        public static void ReplaceWordsInHtml<T>(string htmlPath, T confDic)
        {
            //foreach (KeyValuePair<string, string> conf in confDic)
            //{
            //    поставить условие если entry является цифрой, то продолжить цикл
            //    if (int.TryParse(conf.Key, out int result))
            //    {
            //        continue;
            //    }
            //    if (conf.Key.Contains("Img") && File.Exists(opt.val))
            //    {
            //        LinkedResource res = new LinkedResource(opt.val);
            //        res.ContentId = Guid.NewGuid().ToString();
            //        lLinkedResource.Add(res);

            //        string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";

            //        mMI.body = mMI.body.Replace(opt.name, htmlBody);
            //    }
            //    else
            //        mMI.body = mMI.body.Replace(opt.name, opt.val);//заменяем в нем все шаблонные места из файла конфига
            //}
        }

        public static void WriteCfg(ref Dictionary<string, string> confDic, string PathToCfg, string key, string value)
        {
            using (var f = new StreamWriter(PathToCfg, append: true))
                f.WriteLine($"{key}={value}");
            confDic[key] = value;
        }
    }
}
