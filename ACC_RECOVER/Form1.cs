using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACC_RECOVER
{
    public partial class FORM_recover : Form
    {
        public int countGOOD = 0;
        public FORM_recover()
        {
            InitializeComponent();
            pVar.countProxy = 0;
            pVar.countGOOD = 0;
            pVar.countERROR = 0;
            pVar.countCURRENT = 0;
            pVar.mainAction = "LOGINS";
            chooseTypeOfList();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
        public void chooseTypeOfList()
        {
            Logins.Load();

                ALL.Text = pVar.countALL.ToString();
       
        }

        public void chooseProxy()
        {

            if (checkSOCKS5.Checked == true)
            {
                mProxy.proxyTYPE = "socks5";
            }
            else if (checkSOCKS4.Checked == true)
            {
                mProxy.proxyTYPE = "socks4";
            }
            else if (checkHTTPS.Checked == true)
            {
                mProxy.proxyTYPE = "https";
            }
            else if (checkNONE.Checked == true)
            {
                mProxy.proxyTYPE = "none";
                pVar.countProxy = 0;
            }

            mProxy.proxySettings();
            Invoke(new Action(() =>
            {
                PROXIES.Text = pVar.countProxy.ToString();
            }));
        }

        public void showSuccess()
        {


            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    GOODS.Text = "444";
                }));
            }
            string status = "GOOD";
            
            writeLineToFile(status, pVar.currentLogin);
        }

        public void showErrors()
        {
            pVar.counterERRORS++;
            ERRORS.Text = pVar.counterERRORS.ToString();
            string status = "ERRORS";

            writeLineToFile(status, pVar.currentLogin);
        }

        private void writeLineToFile(string status, string line)
        {
            GOODS.Text = "200";
            string file_src = status + ".txt";

            using (System.IO.StreamWriter file =
               new System.IO.StreamWriter(file_src, true))
            {
                file.WriteLine(line);
            }

        }

            private void btnStart_Click_1(object sender, EventArgs e)
        {
            chooseTypeOfList();
            chooseProxy();
            Recover.Run(Recover.DO);
           // xNetRequest.getCloudFlareCookies();
        }
    }
}
