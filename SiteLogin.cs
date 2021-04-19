using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RMR.FinancialAllocation.Automation.Tools;

namespace RMR.FinancialAllocation.Automation.Layout
{
    public class SiteLogin
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration config;

        public SiteLogin(IWebDriver driver)
        {
            this.driver = driver;
            config = ConfigutationManager.InitConfiguration();
        }

        public void LoginOffice365()
        {
            driver.Manage().Window.Maximize();
            Console.WriteLine($"Initializing Main {this.GetType()}");
            driver.Navigate().GoToUrl(config["site"]);
            Thread.Sleep(7000);
            string windowsHandle = "";
            foreach (string handle in driver.WindowHandles) 
            {
                IWebDriver popup = driver.SwitchTo().Window(handle);

                if (popup.Title.Contains("Sign in to your account")) 
                {
                    windowsHandle = handle;
                    break;
                }
            }

            driver.FindElement(By.Id("i0116")).SendKeys(config["userLogin"]);
            var AcceptButton1 = driver.FindElement(By.Id("idSIButton9"));
            AcceptButton1.Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("i0118")).SendKeys(config["passwordLogin"]);
            Thread.Sleep(3000);
            var AcceptButton2 = driver.FindElement(By.Id("idSIButton9"));
            AcceptButton2.Click();
            Thread.Sleep(3000);

            var twoFactorCode = OTPCodeGenerator.GetCodeFromSecretKey();
            
            driver.FindElement(By.Id("idTxtBx_SAOTCC_OTC")).SendKeys(twoFactorCode);
            Thread.Sleep(3000);

            var AcceptButton3 = driver.FindElement(By.Id("idSubmit_SAOTCC_Continue"));
            AcceptButton3.Click();
            Thread.Sleep(3000);

            var AcceptButton4 = driver.FindElement(By.Id("idSIButton9"));
            AcceptButton4.Click();
            Thread.Sleep(4000);

            foreach (string handle in driver.WindowHandles) 
            {
                IWebDriver popup = driver.SwitchTo().Window(handle);
                if (popup.Title.Contains("Payroll")) break;
            }

            driver.Navigate().Refresh();
            Thread.Sleep(3000);
        }
    }
}