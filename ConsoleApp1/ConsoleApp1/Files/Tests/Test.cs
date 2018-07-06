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
            dv.FindElement(By.Id("UserName")).SendKeys(email);
            Thread.Sleep(10000);
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

        static void Main(string[] args)
        {
            TimeSpan time = TimeSpan.FromMinutes(1);
            IWebDriver dv = new FirefoxDriver();
            string UrlTo = "http://test.crm4you.pro:5775";
            new WebDriverWait(dv, time).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            //dv.AddArgument("no-sandbox");
            dv.Url = "http://test.crm4you.pro:5775";
            dv.Manage().Window.Maximize();

            CreateUserBO(dv);
            //LogOut(dv);
            //TestEmail(dv);
        }
    }
}
