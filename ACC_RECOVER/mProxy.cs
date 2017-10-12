using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ACC_RECOVER
{
  public class mProxy
    {
        public static string proxyTYPE;
        public static StreamReader sr_proxy;
        public static List<string> listProxy = new List<string>();
        
        public static string currentProxy;
        public static void proxySettings()
        {


            if (proxyTYPE != "none")
            {

                    // GET PROXY BY LINK
                    WebClient WebClientForProxy = new WebClient();
               string uriString = "http://35.184.197.58/white_" + proxyTYPE + ".txt";
               // string uriString = "http://80.78.251.153/?controller=api&hwid=322b3925ea10af62322007f797645453&type=" + proxyTYPE;
                    Stream webProxyListStream = WebClientForProxy.OpenRead(uriString);
                    sr_proxy = new StreamReader(webProxyListStream);

                    //GET PROXY FROM FILE
                    //sr_proxy = new StreamReader(@"source/proxyHttp.txt");


                    using (sr_proxy)
                    {

                        string line;
                        string proxy;
                        string proxyPat = @"^(\d{1,3}).(\d{1,3}).(\d{1,3}).(\d{1,3}):(\d{2,5})$";
                        Match m;
                        while ((line = sr_proxy.ReadLine()) != null)
                        {

                            m = Regex.Match(line, proxyPat);

                            if (m.Success)
                            {
                                proxy = Regex.Replace(line, proxyPat, "$1.$2.$3.$4:$5");
                                listProxy.Add(proxy);
                                //Console.WriteLine(proxy);
                                pVar.countProxy++;

                            }

                        }
                        
                    
                    webProxyListStream.Close();

                }
               

            }
        }

        public static string nextProxy()
        {
            if (proxyTYPE != "none")
            {
                pVar.countProxy = listProxy.LongCount();
                if (pVar.countProxy == 0) { proxySettings(); }
                currentProxy = listProxy.First().ToString();
                listProxy.RemoveAt(0);
                Console.WriteLine(listProxy.Count()+" PROXIES");
                Console.WriteLine(currentProxy + " CURRENT PROXY");
                return currentProxy;
            }
            else
            {
                return "";
            }

        }

    }
}
