using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ACC_RECOVER
{
    public class Logins
    {
        public static void Load()
        {

             pVar.countALL = 0;
             if(pVar.listLogins!=null) pVar.listLogins.Clear();


            pVar.sr_logins = new StreamReader(@""+pVar.mainAction+".txt");
            using (pVar.sr_logins)
            {

                string line;
                string login;
                while ((line = pVar.sr_logins.ReadLine()) != null)
                {

                        login = line;
                        pVar.listLogins.Add(login);
                       // Console.WriteLine(login);
                        pVar.countALL++;
                }
            }

        }

        public static string nextLogin(int id)
        {
                pVar.currentLogin = pVar.listLogins.ElementAt(id).ToString();
                //pVar.listLogins.RemoveAt(0);
               Console.WriteLine(pVar.listLogins.Count() + " LOGINS");
                Console.WriteLine(pVar.currentLogin + " CURRENT LOGIN");
                return pVar.currentLogin;
        }
    }
}
