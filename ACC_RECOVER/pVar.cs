using System;
using System.Collections.Generic;
using System.IO;
using xNet;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ACC_RECOVER
{
   public static class pVar
    {
        public static string mainAction { get; set; }
        public static ProxyType proxTyp { get; set; }
        public static long countProxy { get; set; }

        public static long countALL;
        public static int countCURRENT;
        public static int countGOOD;
        public static int countERROR;

        public static int counterACCS;
        public static int counterERRORS;

        public static string __cfduid;
        public static string cf_clearance;
        public static StreamReader sr_logins;
        public static List<string> listLogins = new List<string>();
        
        public static string currentLogin;
    }
}
