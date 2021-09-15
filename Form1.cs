using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaloExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ChromeDriver driver;
        private void button1_Click(object sender, EventArgs e)
        {
            string username = "số điện thoại zalo";
            string password = "mật khấu zalo";
            ChromeOptions options = new ChromeOptions();
            driver = new ChromeDriver(options);
            Dictionary<string, object> listURL = new Dictionary<string, object>();
            string[] durl = { "https://stc.sp.zdn.vn/zaloid/client/main-5.2.9.js" };
            listURL.Add("urls", durl);
            driver.ExecuteChromeCommand("Network.setBlockedURLs", listURL);
            Dictionary<string, object> listURL22 = new Dictionary<string, object>();
            driver.ExecuteChromeCommand("Network.enable", listURL22);
            driver.Navigate().GoToUrl("https://id.zalo.me");
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript(File.ReadAllText(@"js_zalocap.js"));
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByTagName('li')[1].childNodes[0].click();");
            Thread.Sleep(200);
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByTagName('input')[0].value=\"" + username + "\"");
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByTagName('input')[1].value=\"" + password + "\"");

            var anycaptchaResolve = new AnyCaptchaHelper.AnyCaptcha().Zalo("điền key anycaptcha vào đây");
            if (anycaptchaResolve.IsSuccess)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByTagName('textarea')[0].value=\"" + anycaptchaResolve.Result + "\"");
                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('anycapZLogin').click();");
            }
            else
            {
                MessageBox.Show(anycaptchaResolve.Message);
            }

        }
    }
}
