using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
 RULES:
 - All functions should get you back to the main page
 - All links should start by: "http://"
*/
namespace MyRandom
{
    class Test
    {
        public static void Wait(IWebDriver dv, int time) => dv.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        public static void LogIn (IWebDriver dv, String email, String PWD)
        {
            Thread.Sleep(5000);
            dv.FindElement(By.Id("UserName")).SendKeys(email);
            dv.FindElement(By.Id("Password")).SendKeys(PWD + Keys.Enter);
        }
        // Actions:
        // - Forget PWD.
        // - enter PWD.
        // - Check fail/success info box.
        public static void TestEmail (IWebDriver dv)
        {
            String email = "nicutestadmin@yopmail.com";

            try
            {
                dv.FindElement(By.Id("forget-password")).Click();
                dv.FindElement(By.Id("submit-pwd-reset-btn")).Click();
                if (dv.FindElement(By.Id("forgot-pwd-msg-success")).GetAttribute("class").Contains("hidden"))
                    Console.WriteLine("\nEmpty Email succeded");

                dv.FindElement(By.Name("login")).SendKeys(email);
                dv.FindElement(By.Id("submit-pwd-reset-btn")).Click();
                                if (!dv.FindElement(By.Id("forgot-pwd-msg-success")).GetAttribute("class").Contains("hidden"))
                    Console.WriteLine("Email Succeeded");
            }
            catch
            {
                Console.WriteLine("Fail");
            }
            finally
            {
                dv.Url = "http://test.crm4you.pro:5775";
            }
        }
        //Creates 220 vakid users in system
        private static void CreateEmployee(IWebDriver dv, String UrlTo)
        {
            LogIn(dv, "it@torrentfx.com", "Test123!");
            Thread.Sleep(10000);
            dv.Navigate().GoToUrl(UrlTo + "/en/Employee/CreateEmployee");

            dv.FindElement(By.Id("FirstName")).SendKeys(MyRandom.RandomString(10));
            dv.FindElement(By.Id("LastName")).SendKeys(MyRandom.RandomString(10));
            dv.FindElement(By.Id("Address")).SendKeys(MyRandom.RandomString(10));
            dv.FindElement(By.XPath("//*[@id='CountryKey']/option[3]")).Click();
            dv.FindElement(By.Id("Email")).SendKeys(MyRandom.RandomString(10) + "@yopmail.com");
            dv.FindElement(By.Id("Phone")).SendKeys(MyRandom.RandomInt(8));
            dv.FindElement(By.Id("formContinueButton")).Click();
            Thread.Sleep(5000);
            dv.FindElement(By.XPath("/html/body/div[3]/div[2]/div/div/div/div/div[2]/form/div/div[1]/div[2]/div[5]/div/div[1]/div/select/option[2]")).Click();
            dv.FindElement(By.XPath("/html/body/div[3]/div[2]/div/div/div/div/div[2]/form/div/div[1]/div[2]/div[5]/div/div[2]/div/select/option[2]")).Click();
            dv.FindElement(By.Id("createUserSubmit")).Click();
        }
        public static void CreateUserBO (IWebDriver dv, string UrlTo)
        {            
            LogIn(dv, "it@torrentfx.com", "Test123!");
            Thread.Sleep(10000);
            dv.Navigate().GoToUrl(UrlTo + "/en/CrmMain/CreateUser");
            int i = 2;
            try
            {
                while (i < 220)
                {
                    dv.Navigate().GoToUrl("http://test.crm4you.pro:5775/en/CrmMain/CreateUser");
                    Thread.Sleep(10000);
                    dv.FindElement(By.Id("TitleKey")).Click();
                    if (i % 5 != 0)
                        dv.FindElement(By.XPath("//*[@id='TitleKey']/option[" + (i % 5) + "]")).Click();
                    else
                        dv.FindElement(By.XPath("//*[@id='TitleKey']/option[" + ((i + 1) % 5) + "]")).Click();
                    dv.FindElement(By.Id("FirstName")).SendKeys(MyRandom.RandomString(10));
                    dv.FindElement(By.Id("Email")).SendKeys(MyRandom.RandomString(10) + "@yopmail.com");
                    dv.FindElement(By.Id("LastName")).SendKeys(MyRandom.RandomString(10));
                    dv.FindElement(By.Id("Address")).SendKeys(MyRandom.RandomString(10));
                    dv.FindElement(By.Id("City")).SendKeys(MyRandom.RandomString(10));
                    dv.FindElement(By.Id("ZipCode")).SendKeys(MyRandom.RandomString(10));
                    if (i % 185 != 0)
                        dv.FindElement(By.XPath("//*[@id='CountryKey']/option[" + (i % 185) + "]")).Click();
                    else
                        dv.FindElement(By.XPath("//*[@id='CountryKey']/option[" + ((i + 1) % 185) + "]")).Click();
                    dv.FindElement(By.XPath("//*[@id='NationalityKey']/option[" + i + "]")).Click();
                    dv.FindElement(By.Id("PhoneExtension")).SendKeys("373");
                    dv.FindElement(By.Id("Phone")).SendKeys(MyRandom.RandomInt(8));
                    if (i % 3 != 0)
                        dv.FindElement(By.XPath("//*[@id='Language']/option[" + (i % 3) + "]")).Click();
                    else
                        dv.FindElement(By.XPath("//*[@id='Language']/option[" + ((i + 1) % 3) + "]")).Click();
                    dv.FindElement(By.Id("Comment")).SendKeys("Created by AutoTest");
                    dv.FindElement(By.Id("formContinueButton")).Click();
                    //second page
                    dv.FindElement(By.XPath("//*[@id='EmploymentStatus']/option[" + 1 + "]")).Click();
                    dv.FindElement(By.XPath("//*[@id='EstimatedAnnualIncome']/option[" + 1 + "]")).Click();
                    dv.FindElement(By.XPath("//*[@id='EstimatedNetWorth']/option[" + 1 + "]")).Click();
                    dv.FindElement(By.Id("createUserSubmit")).Click();
                    Thread.Sleep(10000);
                }
            }
            catch
            {
                Console.WriteLine("BO Create User Fail");
            }
            finally
            {
                dv.Navigate().GoToUrl("http://test.crm4you.pro:5775");
            }
        }
        public static void LogOut(IWebDriver dv)
        {
            dv.Url = "http://test.crm4you.pro:5775/en/Account/LogOff";
        }

        public static void TryToFind(IWebDriver dv, string symb)
        {
            try
            {
                dv.FindElement(By.Id("webkit-xml-viewer-source-xml"));
                Console.WriteLine(symb + " Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(symb + " Fail");
            }
        }
        public static void CheckSymbolWraper(IWebDriver dv)
        {
            string[] allSymb = new[] {"EURUSD", "GBPUSD", "USDJPY", "USDCHF", "AUDUSD", "NZDUSD", "USDCAD", "AUDJPY", "AUDNZD", "CADCHF", "EURAUD.", "CHFJPY", "EURAUD", "EURCAD",
                "EURCHF", "EURGBP", "EURJPY", "EURNZD", "GBPAUD", "AUDCHF", "AUDCAD", "GBPCAD", "CADJPY", "GBPCHF", "GBPJPY", "GBPNZD", "NZDCAD", "NZDCHF", "NZDJPY", "EURCZK",
                "EURDKK", "EURHKD", "EURHUF", "EURNOK", "EURPLN", "EURSEK", "EURSGD", "EURTRY", "GBPHKD", "GBPHUF", "GBPSEK", "USDCNH", "USDCZK", "USDDKK", "USDHKD", "USDHUF",
                "USDMXN", "USDNOK", "USDPLN", "USDRUB", "USDSEK", "USDSGD", "USDTRY", "USDZAR", "XAUUSD", "XAGUSD", "WTI", "USDUSC", "EURUSD.", "GBPUSD.", "USDJPY.", "AUDUSD.",
                "USDCHF.", "USDCAD.", "NZDUSD.", "AUDJPY.", "EURJPY.", "GBPJPY.", "AUDNZD.", "AUDCAD.", "AUDCHF.", "CHFJPY.", "EURGBP.", "EURCHF.", "EURNZD.", "EURCAD.", "GBPCHF.",
                "GBPAUD.", "GBPNZD.", "NZDCAD.", "NZDCHF.", "GBPCAD.", "CADCHF.", "CADJPY.", "NZDJPY.", "USDRUB.", "USDSEK.", "EURSEK.", "EURCZK.", "EURDKK.", "EURHKD.", "EURHUF.",
                "EURNOK.", "EURPLN.", "EURSGD.", "EURTRY.", "GBPHKD.", "GBPHUF.", "GBPSEK.", "USDCNH.", "USDCZK.", "USDDKK.", "USDHKD.", "USDHUF.", "USDMXN.", "USDNOK.", "USDPLN.",
                "USDSGD.", "USDTRY.", "USDZAR.", "XAUUSD.", "XAUUSDz", "XAGUSD.", "COPPER.MAY6", "OILUSDz", "COPPER.JUL6", "UKOIL.SEP6", "UKOIL.OCT6", "USOIL.AUG6", "USOIL.SEP6",
                "CHINA$.JUL6", "HSENG$.JUL6", "CSI300.JUL6", "JAPAN$.SEP6", "US30.CASH", "US500.CASH", "USNDX.CASH", "UK100.CASH", "GERMAN.CASH", "ESTOX.CASH", "WTI.", "WTI2",
                "ANGLO-AMERI", "ANTOFAGASTA", "ASTRAZENECA", "AVIVA", "BARCLAYS", "BHP-BILITON", "BP", "BT", "GLENCORE", "GRAINGER", "HSBC", "IMP-TOBACCO", "LLOYDS", "M&S", "RBS",
                "RIO-TINTO", "RSA", "STAN-CHARTD", "TESCO", "VEDANTA", "VODAFONE", "3M", "58.COM", "ADIDAS", "AIG", "ALIBABA", "ALLIANZ", "AMAZON", "AMEX", "APPLE", "AT&T", "BAIDU",
                "BASF", "BAYER", "BITAUTO", "BMW", "BNK-AMER", "BOEING", "BOSTON", "CACC", "CAT", "CHEVRON", "CHI-LIFE", "CHI-PETS", "CHINAMOB", "CHIPOTLE", "CISCO", "CITI", "CNOOC",
                "COCA-COLA", "CORNERSTONE", "CTRIP", "DAIMLER", "DEUT-TELEKM", "DEUTSCHE-BK", "DISNEY", "DUPONT", "E.ON", "EBAY", "ELI-LILLY", "EXXON", "FACEBOOK", "FEDEX", "FITBIT",
                "FORD", "GE", "GM", "GOLDMANS", "GOPRO", "HOME-DEP", "HP", "HUAN-POW", "IBM", "INTEL", "JD.COM", "JOHNSON", "JPMORGAN", "LINKEDIN", "MERCK", "MICROSOFT", "MORGAN-S",
                "MSTRCARD", "McD", "NETFLIX", "NIKE", "OCI", "ORACLE", "P&G", "PAYPAL", "PEPSI", "PETRO-CN", "PFIZER", "PHIL-MOR", "PORSCHE", "QIHOO", "SAP", "SIEMENS", "SKYWORKS",
                "STARBUX", "STJUDE", "STRYKER", "TESLA", "TEXTRON", "TIME-WRN", "TOMTOM", "TRAVELRS", "TRINSEO", "TWITTER", "UTD-HLTH", "UTD-TECH", "VERIZON", "VIACOM", "VIPSHOP",
                "VISA", "VOLKSWAGEN", "WALMART", "WELLS-FG", "YAHOO", "YY-INC", "ZIMMER", "EURUSD#", "GBPUSD#", "USDJPY#", "USDCHF#", "USDCAD#", "NZDUSD#", "AUDUSD#", "AUDNZD#",
                "AUDCAD#", "AUDCHF#", "AUDJPY#", "CHFJPY#", "EURGBP#", "EURAUD#", "EURCHF#", "EURJPY#", "EURNZD#", "EURCAD#", "GBPCHF#", "GBPAUD#", "GBPCAD#", "GBPNZD#", "GBPJPY#",
                "CADCHF#", "CADJPY#", "NZDCAD#", "NZDJPY#", "NZDCHF#", "USDRUB#", "USDSEK#", "EURSEK#", "EURCZK#", "EURDKK#", "EURHKD#", "EURHUF#", "EURNOK#", "EURPLN#", "EURSGD#",
                "EURTRY#", "GBPHKD#", "GBPHUF#", "GBPSEK#", "USDCNH#", "USDCZK#", "USDDKK#", "USDHKD#", "USDHUF#", "USDMXN#", "USDNOK#", "USDPLN#", "USDSGD#", "USDTRY#", "USDZAR#",
                "XAUUSD#", "XAGUSD#", "BrentFut", "EURUSD.f", "CrudeFut", "USOIL.T", "EURUSD.p", "GBPUSD.p", "USDJPY.p", "USDCHF.p", "USDCAD.p", "NZDUSD.p", "AUDUSD.p", "AUDNZD.p",
                "AUDCAD.p", "AUDCHF.p", "AUDJPY.p", "CHFJPY.p", "EURGBP.p", "EURAUD.p", "EURCHF.p", "EURJPY.p", "EURNZD.p", "EURCAD.p", "GBPCHF.p", "GBPAUD.p", "GBPCAD.p", "GBPNZD.p",
                "GBPJPY.p", "CADCHF.p", "CADJPY.p", "NZDCAD.p", "NZDJPY.p", "NZDCHF.p", "USDRUB.p", "USDSEK.p", "EURSEK.p", "EURHKD.p", "EURNOK.p", "EURPLN.p", "EURSGD.p", "EURTRY.p",
                "GBPSEK.p", "USDCNH.p", "USDCZK.p", "USDDKK.p", "USDHUF.p", "USDNOK.p", "USDPLN.p", "USDSGD.p", "USDMXN.p", "USDTRY.p", "USDZAR.p", "XAUUSD.p", "XAGUSD.p", "USOIL.p",
                "CL_BRENT.p", "WTI_OIL.p", "NGAS.p", "FERRARI", "KRAFT", "MATCH", "SNAP", "USOIL", "GBPUSD.d", "UKOIL", "XTIUSD#", "XAUUSD.d", "XAGUSD.d", "EURUSD.d", "USDJPY.d", "AUDUSD.d",
                "EURCHF.d", "GBPUSD.f", "USDJPY.f", "USDCHF.f", "USDCAD.f", "NZDUSD.f", "AUDUSD.f", "AUDNZD.f", "AUDCAD.f", "AUDCHF.f", "AUDJPY.f", "CHFJPY.f", "CADCHF.f", "CADJPY.f",
                "EURAUD.f", "EURGBP.f", "EURJPY.f", "EURNZD.f", "EURCAD.f", "EURCHF.f", "GBPCHF.f", "GBPNZD.f", "GBPAUD.f", "GBPJPY.f", "GBPCAD.f", "NZDJPY.f", "NZDCHF.f", "NZDCAD.f",
                "XAUUSD.f", "XAGUSD.f", "XTIUSD.f", "XAUUSD-", "EURUSD-", "GBPUSD-", "USDJPY-", "USDCHF-", "USDCAD-", "NZDUSD-", "AUDUSD-", "GBPCHF-", "EURGBP-", "EURCHF-", "USOIL.f",
                "CHINA50.f", "US30", "UT100", "US500", "DE30", "EU50", "UK100", "JP225", "HK50", "BITCOIN", "LTCUSD", "DSHUSD", "ETHUSD", "XRPUSD", "USDUSC.p", "CSI300.AUG8", "CSI300.JUL8",

                "HSENG$.JUN8", "CHINA$.JUN8", "JAPAN$.SEP8", "XPDUSD", "XPTUSD", "XPDUSD.", "XPTUSD.", "HSENG$.JUL8", "CHINA$.JUL8", "JAPAN$.DEC8", "USOIL.d", "WTI-chTest", "WTI=test", "WTI1", "EURCHY"};
            string[] failed = new[] { "BrentFut", "EURUSD.f", "CrudeFut", "USOIL.T", "FERRARI", "KRAFT", "MATCH", "SNAP", "USOIL", "GBPUSD.d", "UKOIL", "XTIUSD#", "XAUUSD.d", "XAGUSD.d", "EURUSD.d",
                "USDJPY.d", "AUDUSD.d", "EURCHF.d", "GBPUSD.f", "USDJPY.f", "USDCHF.f", "USDCAD.f", "NZDUSD.f", "AUDUSD.f", "AUDNZD.f", "AUDCAD.f", "AUDCHF.f", "AUDJPY.f", "CHFJPY.f", "CADCHF.f",
                "CADJPY.f", "EURAUD.f", "EURGBP.f", "EURJPY.f", "EURNZD.f", "EURCAD.f", "EURCHF.f", "GBPCHF.f", "GBPNZD.f", "GBPAUD.f", "GBPJPY.f", "GBPCAD.f", "NZDJPY.f", "NZDCHF.f", "NZDCAD.f", "XAUUSD.f",
                "XAGUSD.f", "XTIUSD.f", "XAUUSD-", "EURUSD-", "GBPUSD-", "USDJPY-", "USDCHF-", "USDCAD-", "NZDUSD-", "AUDUSD-", "GBPCHF-", "EURGBP-", "EURCHF-", "USOIL.f", "CHINA50.f", "UT100", "EU50", "HK50",
                "BITCOIN", "LTCUSD", "DSHUSD", "ETHUSD", "XRPUSD", "USDUSC.p", "CSI300.AUG8", "CSI300.JUL8", "HSENG$.JUN8", "CHINA$.JUN8", "JAPAN$.SEP8", "XPDUSD", "XPTUSD", "XPDUSD.", "XPTUSD.", "HSENG$.JUL8",
                "CHINA$.JUL8", "JAPAN$.DEC8", "WTI-chTest", "WTI=test", "WTI1", "EURCHY" };
            int i = 0;

            //while (i < 452)
            //{
            //    dv.Navigate().GoToUrl("http://94.130.121.134:8092/api/Charts/GetChartInfoRequest?Symbol=" + list[i] +
            //                          "&Start=1483228800&End=1534755600&Mode=CHART_RANGE_IN&Period=PERIOD_M1&Timesign=0");
            //    TryToFind(dv, allSymb[i]);
            //    i++;
            //}
            while (i < 86)
            {
                dv.Navigate().GoToUrl("http://94.130.121.134:8092/api/Charts/GetChartInfoRequest?Symbol=" + failed[i] +
                                      "&Start=1483228800&End=1534755600&Mode=CHART_RANGE_IN&Period=PERIOD_M1&Timesign=0");
                TryToFind(dv, failed[i]);
                i++;
            }
        }

        static void Main(string[] args)
        {
            TimeSpan time = TimeSpan.FromMinutes(1);
            IWebDriver dv = new ChromeDriver();
            //string UrlTo = "http://test.crm4you.pro:5775";
            //new WebDriverWait(dv, time).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            ////dv.AddArgument("no-sandbox");
            //dv.Url = "http://test.crm4you.pro:5775";
            //dv.Manage().Window.Maximize();

            CheckSymbolWraper(dv);
            //LogOut(dv);
            //TestEmail(dv);
        }
    }
}
