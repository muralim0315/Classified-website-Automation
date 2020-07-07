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
  public class ImagesPage: BasePage
    {
        //Objects for the Images page
        [FindsBy(How = How.XPath, Using = "//span[@class='vip-ad-gallery__arrow vip-ad-gallery__arrow--next']")]
        IWebElement ArrowNext { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='vip-ad-gallery__carousel-legend']")]
        IWebElement MaxImages { get; set; }

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
        /// Traversing through each image.
        /// </summary>
        public void clikImageNext()
        {
            WaitforControlfullyLoaded(@"//span[@class='vip-ad-gallery__arrow vip-ad-gallery__arrow--next']");
            TakeToControl(ArrowNext);

            string imgCount = MaxImages.Text;

            for(int i=0; i<= Convert.ToInt16(imgCount.Substring(3,imgCount.Length-3).Trim()); i++)
            {
                ArrowNext.Click();
                TakeToControl(ArrowNext);
            }

           

        }

    }
}
