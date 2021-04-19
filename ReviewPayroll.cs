using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RMR.FinancialAllocation.Automation.Layout;
using Xunit;

namespace RMR.FinancialAllocation.Automation.Application
{
    public class ReviewPayroll: IDisposable
    {
        public IWebDriver driver;
        
        
        public ReviewPayroll()
        {
            driver = new ChromeDriver();
            SiteLogin siteLogin = new SiteLogin(driver);
            siteLogin.LoginOffice365();
            MainMenu menu = new MainMenu(driver);
            menu.GoToMenu("review-payroll");
        }

        [Fact]
        public void ReviewPayroll_ValidateTitle()
        {
            ModuleLayout moduleLayout = new ModuleLayout(driver);
           
            string title = moduleLayout.GetModuleTitle();

            Assert.Equal("Review Payroll", title);
        }

        [Fact]
        public void ReviewPayroll_LoadGrossPayGrid_Columns()
        {
            var grosspayDropdownList = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/select[1]"));
            var selectElement = new SelectElement(grosspayDropdownList);
            System.Threading.Thread.Sleep(1000);
            selectElement.SelectByText("05/15/2020", true);

            var AcceptButton1 = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/button[1]"));
            AcceptButton1.Click();

            System.Threading.Thread.Sleep(500);

            GridLayout grid = new GridLayout(driver, "tblGross");
            string[] columns  = grid.GetColumnsHeader();

            Assert.Equal(columns[0].ToUpper(), "EMP NO");
            Assert.Equal(columns[1].ToUpper(), "FIRST NAME");
            Assert.Equal(columns[columns.Length-1].ToUpper(), "MERIT BONUS?");
        }

        [Fact]
        public void ReviewPayroll_LoadGrossPayGrid_Rows()
        {
            var grosspayDropdownList = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/select[1]"));
            var selectElement = new SelectElement(grosspayDropdownList);
            System.Threading.Thread.Sleep(1000);
            selectElement.SelectByText("05/15/2020", true);

            var AcceptButton1 = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/button[1]"));
            AcceptButton1.Click();

            System.Threading.Thread.Sleep(500);

            GridLayout grid = new GridLayout(driver, "tblGross");
            List<List<string>> gridRows  = grid.GetRows();

            Assert.Equal(gridRows.Count, 588);
        }

        [Fact]
        public void ReviewPayroll_LoadGrossPayGrid_FirstRow()
        {
            var grosspayDropdownList = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/select[1]"));
            var selectElement = new SelectElement(grosspayDropdownList);
            System.Threading.Thread.Sleep(1000);
            selectElement.SelectByText("05/15/2020", true);

            var AcceptButton1 = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/button[1]"));
            AcceptButton1.Click();

            System.Threading.Thread.Sleep(500);

            GridLayout grid = new GridLayout(driver, "tblGross");
            List<string> gridFirstRow  = grid.GetFirstRow();

            Assert.Equal(gridFirstRow[0], "1004");
            Assert.Equal(gridFirstRow[1], "Armando A");
            Assert.Equal(gridFirstRow[2], "Baires");
        }

        [Fact]
        public void ReviewPayroll_LoadGrossPayGrid_SearchBar()
        {
            var grosspayDropdownList = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/select[1]"));
            var selectElement = new SelectElement(grosspayDropdownList);
            System.Threading.Thread.Sleep(1000);
            selectElement.SelectByText("05/15/2020", true);

            var AcceptButton1 = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[3]/div/div[2]/button[1]"));
            AcceptButton1.Click();

            System.Threading.Thread.Sleep(500);

            var searchBarText = driver.FindElement(By.XPath("//*[@id=\"home-view\"]/section/div/div[4]/div/div[2]/input"));
            searchBarText.SendKeys("Jill");
            searchBarText.SendKeys(Environment.NewLine);
            
            System.Threading.Thread.Sleep(500);

            GridLayout grid = new GridLayout(driver, "tblGross");
            List<string> gridFirstRow  = grid.GetFirstRow();

            Assert.Equal(gridFirstRow[0], "1017");
            Assert.Equal(gridFirstRow[1], "Jill");
            Assert.Equal(gridFirstRow[2], "Cassidy");
        }


        public void Dispose()
        {
            driver.Close();
            driver.Quit();
        }
    }
}