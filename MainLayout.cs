using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using OtpNet;
using RMR.FinancialAllocation.Automation.Tools;
using RMR.FinancialAllocation.Automation.Layout;

namespace RMR.FinancialAllocation.Automation
{
    public class MainLayout : IDisposable
    {
        public IWebDriver driver;
        
        public MainLayout()
        {
            driver = new ChromeDriver();
            SiteLogin siteLogin = new SiteLogin(driver);
            siteLogin.LoginOffice365();
        }

        [Fact]
        public void  MainLayout_ValidateTitle()
        {
            var titleMenu = driver.FindElement(By.XPath("/html/body/app/div/header/nav/a/span"));
            Assert.Equal("Payroll Allocation Application".ToLower(), driver.Title.ToLower());
            Assert.Equal("Payroll Allocation App".ToLower(), titleMenu.Text.ToLower());
        }

        [Fact]
        public void  MainLayout_ValidateUserMenuName()
        {
            var navabarElement = driver.FindElement(By.XPath("//*[@id=\"navbarDropdown\"]"));
            Assert.Contains("Miguel".ToLower(), navabarElement.Text.ToLower());
        }

        [Fact]
        public void  MainLayout_ValidateHomeMenu()
        {
            var navabarHomeMenuElement = driver.FindElement(By.XPath("//*[@id=\"mainMenu\"]/ul/li[1]/a"));
            Assert.Contains("Home".ToLower(), navabarHomeMenuElement.Text.ToLower());
        }

        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
