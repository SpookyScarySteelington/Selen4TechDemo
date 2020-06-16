using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selen4TechDemo.Tests
{
    [TestFixture]
    public class DemoTests : TestBaseClass
    {
        // <summary>
        // Basic test to verify sln is setup correctly.
        // </summary>
        //[Test]
        //public void BasicSearch()
        //{
        //    //Arrange
        //    DriverGc.Navigate().GoToUrl("http://www.google.com");
        //    DriverGc.FindElement(By.CssSelector("input[title*=\"Search\"]")).SendKeys("Parametric");
        //    //Act
        //    DriverGc.FindElement(By.XPath(@"/html/body/div/div[3]/form/div[2]/div[1]/div[3]/center/input[1]")).Click();
        //    //Assert
        //    Assert.IsTrue(DriverGc.Url.Contains("/search"));
        //}

        /// <summary>
        /// This test demonstrates the increased readability of code required to open new tabs/windows in the current browser session.
        /// 
        /// Selenium 4 Code:
        /// DriverGc.SwitchTo().NewWindow(WindowType.Tab);
        ///
        /// Selenium 3 Code:
        /// driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
        /// driver.SwitchTo().Window(driver.WindowHandles.Last());
        /// driver.Navigate().GoToUrl("http://www.google.com")
        /// </summary>
        [Test]
        public void DemoNewTab()
        {
            //Arrange
            var originalTab = DriverGc.CurrentWindowHandle;
            DriverGc.Manage().Window.Maximize();
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            DriverGc.SwitchTo().NewWindow(WindowType.Tab);
            DriverGc.Navigate().GoToUrl("http://www.bing.com");
            var newTab = DriverGc.CurrentWindowHandle;
            //Act
            DriverGc.SwitchTo().Window(originalTab);
            DriverGc.SwitchTo().Window(newTab);
            //Assert
            Assert.IsTrue(DriverGc.Title.Contains("Bing"));
        }
    }
}