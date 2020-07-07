using Automation.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace AutomationTest.Pages
{
    public class LandingPage : BasePage
    {

        //Objects for the landing page

        [FindsBy(How = How.XPath, Using = "//Input[@id='input-search-location-input']")]
        IWebElement InputText { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='SEARCH_DISTANCE_BUTTON']")]
        IWebElement drpButton { get; set; }
                
        //[FindsBy(How = How.XPath, Using = "//ul[@class='search-distance-menu']//li[8]//button")]
        //IWebElement dsli { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//span[@class='search-bar__submit-icon']//span[@class='icon-search']")]
        IWebElement lnkSearch { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@class='header-lower-deck__wrapper']")]
        IWebElement barTab { get; set; }

       
        [FindsBy(How = How.XPath, Using = "//div[@class='panel search-results-page__paginator']")]
        IWebElement PageTab { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='select select--clear']//select[@class='select__select']")]
        IWebElement DropDownList { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='page-number-navigation']//a[text()='1']")]
        IWebElement Page_1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='page-number-navigation']//a[text()='2']")]
        IWebElement Page_2 { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='page-number-navigation']//a[text()='3']")]
        IWebElement Page_3 { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='page-number-navigation']//a[text()='4']")]
        IWebElement Page_4 { get; set; }


        public void Enterlocation()
        {
            MoveToControl(InputText);
            WaitforControlfullyLoaded(@"//Input[@id='input-search-location-input']");
            //InputText.SendKeys("");
            InputText.SendKeys(Keys.Control + "a");//select all text in textbox
            InputText.SendKeys(Keys.Delete); //delete it
            InputText.SendKeys("Wollongong Region, NSW");
         
        }
       
        public void ClickOnSearch()
        {
            
            lnkSearch.Click();
        }
      


        public string GetTextFromDDL()
        {
            MoveToControl(Page_1);
            return new SelectElement(DriverContext.Driver.FindElement(By.XPath("//div[@class='select select--clear']//select[@class='select__select']"))).AllSelectedOptions.SingleOrDefault().Text;
        }


        /// <summary>
        /// Explicit wait condition for the specific control.
        /// </summary>
        /// <param name="xpath"></param>
        public void WaitforControlfullyLoaded(string xpath)
        {
            var wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(60));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
        }

        /// <summary>
        /// Waiting for till all the results page visible.
        /// Time set as 60 seconds.
        /// </summary>
        public void WaitforPageNumbersVisisble()
        {
            for (int second = 0; second <= 60; second++)
            {

                try
                {
                    var list = DriverContext.Driver.FindElements(By.XPath("/div[@class='page-number-navigation']"));
                    if (list.Count > 0)
                        break;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Asserting the Bestmatched results vs Pagination number.
        /// </summary>
        public void AssertAddsCount()
        {

            WaitforControlfullyLoaded(@"//div[@class='panel search-results-page__main-ads-wrapper user-ad-collection user-ad-collection--row']//div[@class='panel-body panel-body--flat-panel-shadow user-ad-collection__list-wrapper']/a");
            var list = DriverContext.Driver.FindElements(By.XPath("//div[@class='panel search-results-page__main-ads-wrapper user-ad-collection user-ad-collection--row']//div[@class='panel-body panel-body--flat-panel-shadow user-ad-collection__list-wrapper']/a"));                                
            string x = GetTextFromDDL();

            // Checking the number of total Best matched results witht he page per results (In the pagination)
            Assert.That(list.Count.ToString(), Is.EqualTo(x.Substring(0,2)));
        }

        /// <summary>
        /// Randomly selecting any add. Here selecting the first one.
        /// </summary>
        /// <returns></returns>
        public RandomAdPage SelectRandomAd()
        {
            WaitforControlfullyLoaded(@"//div[@class='panel search-results-page__main-ads-wrapper user-ad-collection user-ad-collection--row']//div[@class='panel-body panel-body--flat-panel-shadow user-ad-collection__list-wrapper']/a");
            var list = DriverContext.Driver.FindElements(By.XPath("//div[@class='panel search-results-page__main-ads-wrapper user-ad-collection user-ad-collection--row']//div[@class='panel-body panel-body--flat-panel-shadow user-ad-collection__list-wrapper']/a"));
            Random random = new Random();
            int randomAd= random.Next(0, list.Count-1);

            IWebElement adElement = (IWebElement) list[randomAd];
                MoveToControl(adElement);
                adElement.Click();
                return new RandomAdPage();
        }

        /// <summary>
        /// Moving control to the element screen position.
        /// </summary>
        /// <param name="element"></param>
        public void MoveToControl(IWebElement element)
        {           
            Actions actions = new Actions(DriverContext.Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
        /// <summary>
        /// Just to delay the next step (Not replcement of Explicit wait), to see the control engine steps.
        /// </summary>
        public void Delayperiod()
        {
            Thread.Sleep(1000);
        }
        
        /// <summary>
        /// Navigating to 4 pages using pagination.
        /// </summary>
        public void PageNavidation()
        {
            Delayperiod();
            WaitforControlfullyLoaded(@"//div[@class='page-number-navigation']//a[text()='1']");           
            MoveToControl(Page_1);
            Page_1.Click();
            Delayperiod();

            WaitforControlfullyLoaded(@"//div[@class='page-number-navigation']//a[text()='2']");
            MoveToControl(Page_2);
            Page_2.Click();
            Delayperiod();

            WaitforControlfullyLoaded(@"//div[@class='page-number-navigation']//a[text()='3']");
            MoveToControl(Page_3);
            Page_3.Click();
            Delayperiod();

            WaitforControlfullyLoaded(@"//div[@class='page-number-navigation']//a[text()='4']");
            MoveToControl(Page_4);
            Page_4.Click();
            Delayperiod();

        }


      


      


    }
}

