using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Automation.Base;
using AutomationTest.Pages;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;


namespace AutomationTest
{
    [TestFixture]
    class Test : Base
    {

        string url = "https://www.gumtree.com.au/s-cars-vans-utes/toyota/k0c18320?sort=rank";


        public void OpenBrowser(BrowserType browserType = BrowserType.FireFox)
        {
           

            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    DriverContext.Driver = new InternetExplorerDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.FireFox:
                    DriverContext.Driver = new FirefoxDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.Chrome:
                    DriverContext.Driver = new ChromeDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
            }


        }

        [SetUp]
        public void SetUp()
        {
            OpenBrowser(BrowserType.Chrome);
            DriverContext.Browser.GoToUrl(url);
            DriverContext.Driver.Manage().Window.Maximize();

        }

        [Test]
        public void TestMethod()
        {
            //Landing page
            CurrentPage = GetInstance<LandingPage>();
            
            CurrentPage.As<LandingPage>().Enterlocation();            
            CurrentPage.As<LandingPage>().ClickOnSearch();
            CurrentPage.As<LandingPage>().AssertAddsCount();
            CurrentPage.As<LandingPage>().PageNavidation();          

            CurrentPage = CurrentPage.As<LandingPage>().SelectRandomAd();
            CurrentPage= CurrentPage.As<RandomAdPage>().NavigatingToImages();
            CurrentPage.As<ImagesPage>().clikImageNext();

        }

        [TearDown]
        public void TearDown()
        {
            DriverContext.Driver.Quit();
            
        }

    }
}
