using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RMR.FinancialAllocation.Automation.Tools;


namespace RMR.FinancialAllocation.Automation.Application
{
    public class ModuleLayout
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration config;
        public ModuleLayout(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetModuleTitle()
        {
            var TitleElement = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[1]/div/div[1]/h1"));
            return TitleElement.Text;
        }      
    }

}