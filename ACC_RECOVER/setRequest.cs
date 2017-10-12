using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace NitinJS
{
    public class SmsWebClient : WebClient
    {
        public SmsWebClient(CookieContainer container, Dictionary<string, string> Headers)
            : this(container)
        {
            foreach (var keyVal in Headers)
            {
                this.Headers[keyVal.Key] = keyVal.Value;
            }
        }
        public SmsWebClient(bool flgAddContentType = true)
            : this(new CookieContainer(), flgAddContentType)
        {

        }
        public SmsWebClient(CookieContainer container, bool flgAddContentType = true)
        {
            this.Encoding = Encoding.UTF8;
            System.Net.ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePointIdleTime = 2000;
            this.container = container;

            if (flgAddContentType)
            this.Headers["Host"] = "account.leagueoflegends.com";
            this.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            this.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            this.Headers["Accept"] = "application/json, text/javascript, */*; q=0.01";
            this.Headers["Origin"] = "https://account.leagueoflegends.com";
            this.Headers["X-Requested-With"] = "XMLHttpRequest";
            this.Headers["Referer"] = "https://account.leagueoflegends.com/pm.html?xdm_e=https%3A%2F%2Faccount.leagueoflegends.com%2Fna%2Fen%2Fforgot-password&xdm_c=default3177&xdm_p=4";
            this.Headers["Accept-Encoding"] = "gzip, deflate, br";
            this.Headers["Accept-Language"] = "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4";
        }

        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = container;
                request.Timeout = 3600000; //20 * 60 * 1000
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                CookieCollection cookies = response.Cookies;
                container.Add(cookies);
            }
        }
    }
}
