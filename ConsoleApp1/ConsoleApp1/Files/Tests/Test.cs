using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 RULES:
 - All functions should get you back to the main page
 - 
     */
namespace MyRandom
{
    class Test
    {
        public static void Wait(IWebDriver dv, int time) => dv.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        public static void LogIn (IWebDriver dv)
        {
            dv.FindElement(By.Id("UserName")).SendKeys("it@torrentfx.com");
            dv.FindElement(By.Id("Password")).SendKeys(text: "Test123!" + Keys.Enter);
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
                dv.FindElement(By.Name("login")).SendKeys(email);
                dv.FindElement(By.Id("submit-pwd-reset-btn")).Click();

                Wait(dv, 20);
                if (!dv.FindElement(By.Id("forgot-pwd-msg-success")).GetAttribute("class").Contains("hide"))
                    Console.WriteLine("Email Succeeded");
            }
            catch
            {
                Console.WriteLine("Fail");
            }
            finally
            {
                dv.Url = "test.crm4you.pro:5775";
            }
        }

        public static void LogOut(IWebDriver dv)
        {
            dv.Url = "test.crm4you.pro:5775/en/Account/LogOff";
        }

        static void Main(string[] args)
        {
            IWebDriver dv = new ChromeDriver
            {
                Url = "test.crm4you.pro:5775"
            };

            dv.Manage().Window.Maximize();

            /*Wait(20);
            LogIn(dv);

            Wait(20);
            LogOut(dv);*/

            Wait(dv, 20);
            TestEmail(dv);
        }
    }
}
