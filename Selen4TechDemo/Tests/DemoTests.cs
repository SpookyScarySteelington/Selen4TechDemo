using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        //    DriverGc.Manage().Window.Maximize();
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
        /// </summary>
        [Test]
        [Repeat(10)]
        public void NewTabDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to page1, creating new tab, navigating to page2 in new tab");
            var originalTab = DriverGc.CurrentWindowHandle;
            DriverGc.Manage().Window.Maximize();
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(2000);
            DriverGc.SwitchTo().NewWindow(WindowType.Tab);
            DriverGc.Navigate().GoToUrl("http://www.bing.com");
            Thread.Sleep(2000);
            var newTab = DriverGc.CurrentWindowHandle;
            //Act
            Console.WriteLine("Switch to first tab then back to new tab");
            DriverGc.SwitchTo().Window(originalTab);
            Thread.Sleep(2000);
            DriverGc.SwitchTo().Window(newTab);
            Thread.Sleep(2000);
            //Assert
            Console.WriteLine("Validating that the current tab has expected title");
            Assert.IsTrue(DriverGc.Title.Contains("Bing"));
        }

        [Test]
        public void RelativeLocatorsDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to page we want to locate an element on");
            DriverGc.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(3000);
            DriverGc.FindElement(By.CssSelector("input[title*=\"Search\"]")).SendKeys("Parametric");
            var searchButton =
                DriverGc.FindElement(By.Name("btnK"));
            var luckyButton = DriverGc.FindElement(RelativeBy.WithTagName("input").RightOf(searchButton));
            luckyButton.Click();
            //Act
            Console.WriteLine("Navigating to page we want to locate an element on");
            Thread.Sleep(3000);
            //Assert
            //Assert.IsTrue(DriverGc.Url.Contains("wikipedia"));
        }
    }
}