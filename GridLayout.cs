using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace RMR.FinancialAllocation.Automation.Layout
{
    public class GridLayout
    {
        private readonly IWebDriver driver;
        private readonly IConfiguration config;

        private readonly string tableName;
        public GridLayout(IWebDriver driver, string tableName)
        {
            this.driver = driver;
            this.tableName = tableName;
        }

        public string[] GetColumnsHeader()
        {
            // xpath of html table
            var elemTable = driver.FindElement(By.Id(tableName));
            var header = elemTable.FindElement(By.TagName("thead"));

            // Fetch all Row of the table
            IWebElement lstTrElem = header.FindElement(By.TagName("tr"));
            List<IWebElement> lstTdElem = new List<IWebElement>(lstTrElem.FindElements(By.TagName("th")));

            List<string> headerTextColumns = new List<string>();

            if (lstTdElem.Count > 0)
            {
                    // Traverse each column
                    foreach (var elemTd in lstTdElem)
                    {
                        headerTextColumns.Add(elemTd.Text);
                    }
            }

            return headerTextColumns.ToArray();
        }

        public List<List<string>> GetRows()
        {
            // xpath of html table
            var elemTable = driver.FindElement(By.Id(tableName));
            var header = elemTable.FindElement(By.TagName("tbody"));

            // Fetch all Row of the table
            List<IWebElement> lstTrElem =new List<IWebElement>(header.FindElements(By.TagName("tr")));
            List<List<string>> listElements = new List<List<string>>();
                     
            if (lstTrElem.Count > 0)
            {
                foreach(var trItem in lstTrElem)
                {
                    List<IWebElement> lstTdElem = new List<IWebElement>(trItem.FindElements(By.TagName("td")));

                    List<string> rowList = new List<string>();
                     // Traverse each column
                    foreach (var elemTd in lstTdElem)
                    {
                        rowList.Add(elemTd.Text);
                       
                    }

                    listElements.Add(rowList);
                    
                }
                   
            }

            return listElements;
        }
        
        
        public List<string> GetFirstRow()
        {
            // xpath of html table
            var elemTable = driver.FindElement(By.Id(tableName));
            var header = elemTable.FindElement(By.TagName("tbody"));

            // Fetch all Row of the table
            List<IWebElement> lstTrElem =new List<IWebElement>(header.FindElements(By.TagName("tr")));
             List<string> rowList = new List<string>();
            if (lstTrElem.Count > 0)
            {
                    List<IWebElement> lstTdElem = new List<IWebElement>(lstTrElem[0].FindElements(By.TagName("td")));

                   
                     // Traverse each column
                    foreach (var elemTd in lstTdElem)
                    {
                        rowList.Add(elemTd.Text);
                       
                    }
            }

            return rowList;
        }
        
    }
}