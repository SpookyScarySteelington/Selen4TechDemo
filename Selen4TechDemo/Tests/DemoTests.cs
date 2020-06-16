using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Selen4TechDemo.Tests
{
    [TestFixture]
    public class DemoTests : TestBaseClass
    {
        /// <summary>
        /// This test demonstrates the increased readability of code required to open new tabs/windows in the current browser session.
        /// 
        /// Selenium 4 Code:
        /// DriverGc.SwitchTo().NewWindow(WindowType.Tab);
        ///
        /// Selenium 3 Code:
        /// driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
        /// driver.SwitchTo().Window(driver.WindowHandles.Last());
        /// </summary>
        [Test]
        public void NewTabDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to page1, creating new tab, navigating to page2 in new tab");
            var originalTab = DriverGc.CurrentWindowHandle;
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            Wait();
            DriverGc.SwitchTo().NewWindow(WindowType.Tab);
            DriverGc.Navigate().GoToUrl("http://www.bing.com");
            Wait();
            var newTab = DriverGc.CurrentWindowHandle;
            //Act
            Console.WriteLine("Switch to first tab then back to new tab");
            DriverGc.SwitchTo().Window(originalTab);
            Wait();
            DriverGc.SwitchTo().Window(newTab);
            Wait();
            //Assert
            Console.WriteLine("Validating that the current tab has expected title");
            Assert.IsTrue(DriverGc.Title.Contains("Bing"));
        }

        /// <summary>
        /// This test demos the use of the new RelativeBy locators.
        /// It's helpful in situations where an element is difficult to locate.
        /// In those situations you can now select a nearby element that's easier to locate
        /// and then specify the difficult element's position in relation to it.
        /// 
        /// Available Relational Locators: Above, Below, RightOf, LeftOf, Near
        /// </summary>
        [Test]
        public void RelativeLocatorsDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to the test page");
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            Wait();
            DriverGc.FindElement(By.CssSelector("input[title*=\"Search\"]")).SendKeys("Parametric");
            var searchButton =
                DriverGc.FindElement(By.Name("btnK"));
            //Act
            var luckyButton = DriverGc.FindElement(RelativeBy.WithTagName("input").RightOf(searchButton));
            luckyButton.Click();
            Wait();
            //Assert
            Console.WriteLine("Validating that we ended up at the expected \"lucky\" page");
            Assert.IsTrue(DriverGc.Url.Contains("wikipedia"));
        }

        /// <summary>
        /// Selenium 4 supports taking a screen shot of a specific element on the page.
        /// In previous versions a helper method would need to be written to screen shot a specific element.
        /// That helper method could then perform an action like resizing and recentering the driver window.
        /// Or it could cropping the full size image down to the size and position of the desired element.
        /// </summary>
        [Test]
        public void ElementScreenShotDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to the test page");
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            Wait();
            var desiredElement = DriverGc.FindElement(By.Id("hplogo"));
            //Act
            ((ITakesScreenshot)desiredElement).GetScreenshot().SaveAsFile("elementScreenshot.png");
            Wait();
            //Assert
            Assert.That(File.Exists("elementScreenshot.png"));
        }

        //Helper Methods
        /// <summary>
        /// Explicit general wait
        /// </summary>
        private static void Wait()
        {
            Thread.Sleep(3000);
        }

        // <summary>
        // Basic test to verify sln is setup correctly.
        // </summary>
        //[Test]
        //public void BasicSearch()
        //{
        //    //Arrange
        //    DriverGc.Manage().Window.Maximize();
        //    DriverGc.Navigate().GoToUrl("http://www.google.com");
        //    DriverGc.FindElement(By.CssSelector("input[title*=\"Search\"]")).SendKeys("Parametric");
        //    //Act
        //    DriverGc.FindElement(By.XPath(@"/html/body/div/div[3]/form/div[2]/div[1]/div[3]/center/input[1]")).Click();
        //    //Assert
        //    Assert.IsTrue(DriverGc.Url.Contains("/search"));
        //}
    }
}