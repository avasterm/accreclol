﻿using System;
using System.IO;
using xNet;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Text;

namespace ACC_RECOVER
{
    public class JsonS
    {
        public bool Success { get; set; }
        public string message { get; set; }
    }
    public class xNetRequest
    {
        
        public static string result { get; set; }
        public static string response { get; set; }
        public static ProxyClient proxyClient { get; set; }
        public static Uri uri { get; set; }
        public static string paramReq { get; set; }

        [STAThread]
        public static void sendReq(string action, string LoginEmail)
        {
            FORM_recover main = new FORM_recover();
            if (action == "LOGINS")
            {
                paramReq = "accountname";
                uri = new Uri("https://account.leagueoflegends.com/recover/password");
            }
            else if(action == "EMAILS")
            {
                paramReq = "email";
                uri = new Uri("https://account.leagueoflegends.com/recover/username");
            }



            try
            {
                Console.WriteLine("PROXY_TYPE: " + mProxy.proxyTYPE);
                Console.WriteLine("CURRENT_PROXY: " + mProxy.currentProxy);
                //Thread.Sleep(5000);
                using (var request = new HttpRequest())
                {
                    if (mProxy.proxyTYPE != "none")
                    {
                        string[] proxy = mProxy.currentProxy.Split(':');

                        if (mProxy.proxyTYPE == "https")
                            proxyClient = new HttpProxyClient(proxy[0], Convert.ToInt32(proxy[1]));
                        else if (mProxy.proxyTYPE == "socks4")
                            proxyClient = new Socks4ProxyClient(proxy[0], Convert.ToInt32(proxy[1]));
                        else if (mProxy.proxyTYPE == "socks5")
                            proxyClient = new Socks5ProxyClient(proxy[0], Convert.ToInt32(proxy[1]));

                        request.Proxy = proxyClient;
                    }
                    if(pVar.cf_clearance.Length==0)
                    {

                        mProxy.currentProxy = mProxy.nextProxy();
                        getCloudFlareCookies();
                    }
                    

                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                    request.KeepAlive = true;
                    request
                        .AddParam(paramReq, LoginEmail)


                        .AddHeader(HttpHeader.Referer, "https://account.leagueoflegends.com/pm.html?xdm_e=https%3A%2F%2Faccount.leagueoflegends.com%2Fna%2Fen%2Fforgot-password&xdm_c=default3177&xdm_p=4")
                        .AddHeader(HttpHeader.Accept, "application/json, text/javascript, */*; q=0.01")
                        .AddHeader("X-NewRelic-ID", "UA4OVVRUGwEDVllXDgA=")
                        .AddHeader("Origin", "https://account.leagueoflegends.com")
                        .AddHeader("X-Requested-With", "XMLHttpRequest")
                        //.AddHeader(HttpHeader.ContentType, "application/x-www-form-urlencoded")
                        .AddHeader(HttpHeader.ContentEncoding, "gzip, deflate, br")
                        .AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");

                    request.Cookies = new CookieDictionary()
                      {
                            {"__cfduid", pVar.__cfduid},
                            {"cf_clearance", pVar.cf_clearance},
                            {"PVPNET_LANG","en_US"},
                            {"PVPNET_REGION","euw"}
                        };

                    request.Cookies.IsLocked = true;
                    Console.WriteLine(request.Cookies);
                    result = request.Post(uri).ToString();
                    JsonS jsons = JsonConvert.DeserializeObject<JsonS>(result);

                    if (jsons.Success == true) { pVar.counterACCS++;  pVar.counterERRORS = 0; pVar.countGOOD++; main.showSuccess(); }
                    else if(jsons.Success == false) { pVar.counterERRORS++; main.showErrors(); }
                    

                    if (pVar.counterERRORS == 3)
                    {

                        pVar.__cfduid = string.Empty;
                        pVar.cf_clearance = string.Empty;
                        mProxy.currentProxy = mProxy.nextProxy();
                        // WebBrowser BROWSER = new WebBrowser();
                        // BROWSER.BrowserOpen();
                    }
                    Console.WriteLine("RESULT: " + jsons.Success);
                    Console.WriteLine("OTVET: " + jsons.message);
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); result = "lose"; }

            pVar.counterERRORS++;
            if (pVar.counterERRORS == 3)
            {
                pVar.__cfduid = string.Empty;
                pVar.cf_clearance = string.Empty;
                mProxy.currentProxy = mProxy.nextProxy();
                // WebBrowser BROWSER = new WebBrowser();
                // BROWSER.BrowserOpen();
            }
        }

        public static void getCloudFlareCookies()
        {
            using (var request = new HttpRequest())
            {
                if (mProxy.proxyTYPE != "none")
                {
                    request.Proxy = proxyClient;
                }
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                request.KeepAlive = true;
                Uri CFuri = new Uri("https://account.leagueoflegends.com/na/en/forgot-password");
                request.Cookies = new CookieDictionary();
                request.IgnoreProtocolErrors = true;
                string response = request.Get(CFuri).ToString();
                
                pVar.__cfduid = Regex.Replace(request.Cookies.ToString(), @"__cfduid=", "");
                CloudFlare.calculate(response);

                Thread.Sleep(3000);

                Uri CFuri2 = new Uri("https://account.leagueoflegends.com/cdn-cgi/l/chk_jschl?jschl_vc=" + CloudFlare.challenge + "&pass=" + CloudFlare.challenge_pass + "&jschl_answer=" + CloudFlare.solved + "");

                request
                        .AddHeader(HttpHeader.Referer, "https://account.leagueoflegends.com/na/en/forgot-password")
                        .AddHeader(HttpHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8")
                        .AddHeader(HttpHeader.ContentEncoding, "gzip, deflate, br")
                        .AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");

                var response2 = request.Get(CFuri2).ToString();
                //  Console.WriteLine(response2);

                pVar.__cfduid = Regex.Replace(request.Cookies.ToString(), @"__cfduid=(.*?)\; cf_clearance=(.*?)", "$1");
                pVar.cf_clearance = Regex.Replace(request.Cookies.ToString(), @"__cfduid=(.*?)\; cf_clearance=(.*?)", "$2");
                Console.WriteLine("COOK1:  " + pVar.__cfduid);
                Console.WriteLine("COOK2:  " + pVar.cf_clearance);

            }
        }
    }
}
