using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Jint;
namespace ACC_RECOVER
{
    class CloudFlare
    {
        //public static string resPonse;
        public static string challenge;
        public static string challenge_pass;
        public static string builder;
        public static long solved;
        public static void calculate(string resPonse)
        {
            var JSEngine = new Jint.Engine();
            challenge = Regex.Match(resPonse, "name=\"jschl_vc\" value=\"(\\w+)\"").Groups[1].Value;
            challenge_pass = Regex.Match(resPonse, "name=\"pass\" value=\"(.+?)\"").Groups[1].Value;

            builder = Regex.Match(resPonse, "setTimeout\\(function\\(\\){\\s+(var s,t,o,p,b,r,e,a,k,i,n,g,f.+?\r?\n[\\s\\S]+?a\\.value =.+?)\r?\n").Groups[1].Value;
            

            builder = Regex.Replace(builder, @"a\.value =(.+?) \+ .+?;", "$1");
            builder = Regex.Replace(builder, @"\s{3,}[a-z](?: = |\.).+", "");
            builder = Regex.Replace(builder, @"[\n\\']", "");
            builder = Regex.Replace(builder, @"(parseInt\((.*?)\,\s\d{1,10}\)\s\;\s\d{1,10})", "");

            //Console.WriteLine(builder);
            var test = @"var s,t,o,p,b,r,e,a,k,i,n,g,f, tDZRQkc={""Gkre"":+((!+[]+!![]+[])+(!+[]+!![]))};        ;tDZRQkc.Gkre-=+((+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));tDZRQkc.Gkre+=+((!+[]+!![]+!![]+!![]+!![]+[])+(+[]));tDZRQkc.Gkre+=+!![];tDZRQkc.Gkre*=+((!+[]+!![]+!![]+!![]+[])+(+[]));tDZRQkc.Gkre+=+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]));tDZRQkc.Gkre-=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));tDZRQkc.Gkre+=+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]));tDZRQkc.Gkre+=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));tDZRQkc.Gkre+=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]));tDZRQkc.Gkre*=+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));";
            solved = long.Parse(JSEngine.Execute(builder).GetCompletionValue().ToObject().ToString());
            solved += ("account.leagueoflegends.com").Length;
            Console.WriteLine(solved);
        }
       

    }
}
