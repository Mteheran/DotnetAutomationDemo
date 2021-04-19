using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RMR.FinancialAllocation.Automation.Tools;


namespace RMR.FinancialAllocation.Automation.Layout
{
    public class MainMenu
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration config;

        private readonly string siteUrl;

        public MainMenu(IWebDriver driver)
        {
            this.driver = driver;
            config = ConfigutationManager.InitConfiguration();
            siteUrl = config["site"];
        }

        public void GoToMenu(string menu)
        {
            var MenuElements = driver.FindElements(By.CssSelector("#mainMenu > ul > li.nlightblue.fade-selection-animation.ng-scope > a"));

            foreach(var item in MenuElements)
            {
                if(item.GetAttribute("href").Replace(siteUrl, "").ToLower() == menu.ToString().ToLower())
                {
                    item.Click();
                    break;
                }
            }    
            
            Thread.Sleep(3000);
        
        }      
    }

}