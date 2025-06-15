using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Commons
{
    public abstract class BaseRequest
    {
        protected string language { get; set; } = "zh-TW";
        //protected string key { get; set; } = GoogleAPIContext.APIKey;

        public virtual string ToUri()
        {
            string url = "";
            url += GetPropsInfo(BindingFlags.NonPublic);
            url += "&" + GetPropsInfo(BindingFlags.Public);
            return url;
        }
        private string GetPropsInfo(BindingFlags bindingFlags)
        {
            string url = "";
            var infos = this.GetType().GetProperties(bindingFlags | BindingFlags.Instance);
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i].Name != "URL")
                {
                    string prop = infos[i].Name.ToLower() + "=" + infos[i].GetValue(this) + "&";
                    url += prop;
                }
            }
            url = url.TrimEnd('&');
            return url;
        }
        public abstract string URL { get; }
    }
}
