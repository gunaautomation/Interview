using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Interview
{
    class Program
    {
        public static IWebDriver _currentWebDriver;
        public static string URL = "https://www.programmableweb.com/category/open-data/api";
        static void Main()
        {
            ChromeOptions option = new ChromeOptions();
            var basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(basePath + @"\Driver");
            service.SuppressInitialDiagnosticInformation = true;            
            _currentWebDriver = new ChromeDriver(service, option);
            _currentWebDriver.Navigate().GoToUrl(URL);
          
            //a)Write an automation script to read and print that default text - ANSWER AS FOLLOWS
            IWebElement ele = _currentWebDriver.FindElement(By.Id("edit-term--2"));
            string ActualString = ele.GetAttribute("aria-label");
            Console.WriteLine("The default text is {0}",ActualString);

            //b)Also extract the number alone and print like this. ‘Programmableweb has XXXXX APIs’ - ANSWER AS FOLLOWS
            String[] ApiNumber = ActualString.Split(new Char[] { ' ' });           
            Console.WriteLine("Programmableweb has {0} APIs", ApiNumber[2]);

            //Getting total tab count and then proceeding to print 4 st step because if there will be a chance to tab to get changed dynamically 
            //4 You need to print the tab texts as follows: Answer as follows
            IList<IWebElement> listEle = _currentWebDriver.FindElements(By.XPath("//ul[@id='myTab']/li"));
            int count = listEle.Count;
            int totalResCount = 0;

            //i starts from 2 to remove summary tab
            for(int i=2;i<=count;i++)
            {
                string CategoryName = _currentWebDriver.FindElement(By.XPath("//ul[@id='myTab']/li["+i+"]/a")).Text;
                CategoryName = CategoryName.Replace("\r\n", ",");
                String[] subCategory = CategoryName.Split(new Char[] { ',' });
                string CategoryValue = subCategory[1].Replace("(","");
                CategoryValue = CategoryValue.Replace(")", "").Trim();
                Console.WriteLine("There are {0} {1} in this page", CategoryValue, subCategory[0]);
                // 5)	Print total number of resources by adding all these numbers you got in 4th step.
                totalResCount = totalResCount + int.Parse(CategoryValue);
            }
            //commented this line to show the command window with results
            //_currentWebDriver.Quit();
            //Prints total number of resources
            Console.WriteLine("Total number of resourses are {0}", totalResCount);
        }
    }
}
