using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace Selen4TechDemo.Tests
{
    [TestFixture]
    public class DemoTests : TestBaseClass
    {
        //Constants
        private const string GoogleAddress = "http://www.google.com";
        private const string BingAddress = "http://www.bing.com";
        private const string Parametric = "Parametric";

        //Selectors
        private const string GoogleSearchField = "input[title*=\"Search\"]";
        private const string GoogleSearchButton = "btnK";
        private const string Input = "input";
        private const string GoogleLogo = "hplogo";

        /// <summary>
        /// This test demonstrates the increased write/readability of code required to open new tabs/windows in the current browser/driver session.
        /// 
        /// Selenium 4 Code:
        /// DriverGc.SwitchTo().NewWindow(WindowType.Tab);
        ///
        /// Selenium 3 Code:
        /// driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
        /// driver.SwitchTo().Window(driver.WindowHandles.Last());
        ///
        /// Another alternative in Selenium 3 was creating multiple driver instances.
        /// </summary>
        [TestCase(WindowType.Tab)]
        [TestCase(WindowType.Window)]
        public void NewTabDemo(WindowType windowType)
        {
            //Arrange
            Console.WriteLine("Navigating to first page");
            DriverGc.Navigate().GoToUrl(GoogleAddress);
            var googleTab = DriverGc.CurrentWindowHandle;
            Wait();

            Console.WriteLine("Opening a new tab/window and navigating to second page");
            DriverGc.SwitchTo().NewWindow(windowType);
            DriverGc.Navigate().GoToUrl(BingAddress);
            var bingTab = DriverGc.CurrentWindowHandle;
            Wait();

            //Act
            Console.WriteLine("Switch to first tab/window and then switch back");
            DriverGc.SwitchTo().Window(googleTab);
            Wait();
            DriverGc.SwitchTo().Window(bingTab);
            Wait();

            //Assert
            Console.WriteLine("Validating that the current tab has expected title");
            Assert.IsTrue(DriverGc.Title.ToLower().Contains(BingAddress.Split('.')[^2]));
        }

        /// <summary>
        /// This test demos the use of the new RelativeBy locators.
        /// It's helpful in situations where an element is difficult to locate.
        /// In those situations you can now select a nearby element that's easier to locate.
        /// After selecting the "easy element specify the difficult to locate element's position in relation to it.
        /// 
        /// Available Relational Locators: Above, Below, RightOf, LeftOf, Near
        /// </summary>
        [Test]
        public void RelativeLocatorsDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to the test page");
            DriverGc.Navigate().GoToUrl(GoogleAddress);
            Wait();

            Console.WriteLine("Input query into the search box.");
            DriverGc.FindElement(By.CssSelector(GoogleSearchField)).SendKeys(Parametric);

            //Act
            Console.WriteLine("Locate and click the \"Feeling Lucky\" button based off of the \"Search\" button's position.");
            var searchButton =
                DriverGc.FindElement(By.Name(GoogleSearchButton));
            var luckyButton = DriverGc.FindElement(RelativeBy.WithTagName(Input).RightOf(searchButton));
            luckyButton.Click();
            Wait();

            //Assert
            Console.WriteLine("Validating that we have left Google");
            Assert.IsFalse(DriverGc.Url.ToLower().Contains(GoogleAddress.Split('.')[^2]));
        }

        /// <summary>
        /// Selenium 4 supports taking a screen shot of a specific element on the page.
        /// In previous versions a helper method would need to be written to screen shot a specific element.
        /// That helper method would perform an action like resize and recenter
        /// the browser window to the element before taking the screen shot.
        /// Another option was cropping down the full screen shot to the elements size and position.
        /// </summary>
        [Test]
        public void ElementScreenShotDemo()
        {
            //Arrange
            Console.WriteLine("Navigating to the test page");
            DriverGc.Navigate().GoToUrl(GoogleAddress);
            Wait();
            var desiredElement = DriverGc.FindElement(By.Id(GoogleLogo));

            //Act
            Console.WriteLine("Take a screen shot of the Google logo element");
            ((ITakesScreenshot)desiredElement).GetScreenshot().SaveAsFile(ScreenShotFileName);
            Wait();

            //Assert
            Console.WriteLine("Verify an image file was created. Ideally compare against expected image file");
            Assert.That(File.Exists(ScreenShotFileName));
        }

        /// <summary>
        /// This is a quick demo showing that Chrome dev tools are now available to us within the Selenium Framework.
        /// This example only blocks certain element types from loading but we also have access to categories such as:
        /// AppCache, Network, Performance, Profiling, Security, DOM, etc.
        /// </summary>
        [Test]
        public void ChromeDevToolsDemo()
        {
            //Arrange
            Console.WriteLine("Navigate to page to show images and css have loaded");
            DriverGc.Navigate().GoToUrl(BingAddress);
            Wait();

            Console.WriteLine("Create new instance and session of devTools, enable network commands, and some block file types");
            IDevTools devTools = DriverGc as IDevTools;
            DevToolsSession session = devTools.CreateDevToolsSession();
            session.Network.Enable(new OpenQA.Selenium.DevTools.Network.EnableCommandSettings());
            session.Network.SetBlockedURLs(new OpenQA.Selenium.DevTools.Network.SetBlockedURLsCommandSettings()
            {
                Urls = new[] { "*://*/*.css" , "*://*/*.jpg" , "*://*/*.png"}
            });

            //Act
            Console.WriteLine("Navigating to the test page to show images and css no longer load");
            DriverGc.Navigate().GoToUrl(BingAddress);
            Wait();

            //Assert
            Console.WriteLine("Placeholder assert to validate the expected page has loaded");
            Assert.IsTrue(DriverGc.Title.ToLower().Contains(BingAddress.Split('.')[^2]));
        }


        //
        //Helper Methods
        //
        /// <summary>
        /// Explicit general wait
        /// </summary>
        private static void Wait()
        {
            Thread.Sleep(5000);
        }
    }
}