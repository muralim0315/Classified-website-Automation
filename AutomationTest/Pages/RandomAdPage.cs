using Automation.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
//using Protractor;
using System.Threading;
using OpenQA.Selenium.Interactions;
using SeleniumExtras;


namespace AutomationTest.Pages
{
  public class RandomAdPage : BasePage
    {
        //Objects for the Add page

        [FindsBy(How = How.XPath, Using = "//div[@class='vip-ad-image__legend']//button")]
        IWebElement imgButton { get; set; }


        public void WaitforControlfullyLoaded(string xpath)
        {
            var wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(60));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
        }

        public void TakeToControl(IWebElement element)
        {
            Actions actions = new Actions(DriverContext.Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        /// <summary>
        /// Clik on Images button of the Ad.
        /// </summary>
        /// <returns></returns>
        public ImagesPage NavigatingToImages()
        {
            WaitforControlfullyLoaded(@"//div[@class='vip-ad-image__legend']//button");
            TakeToControl(imgButton);
            imgButton.Click();

            return new ImagesPage();

        }

    }
}
