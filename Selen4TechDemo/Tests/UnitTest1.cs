using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selen4TechDemo.Tests
{
    public class Tests
    {
        private static IWebDriver _driverGc;

        [SetUp]
        public void Setup()
        {
            _driverGc = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            _driverGc.Close();
            _driverGc.Quit();
        }

        [Test]
        public void BasicSearch()
        {
            //Arrange
            _driverGc.Navigate().GoToUrl("http://www.google.com");
            _driverGc.FindElement(By.CssSelector("input[title*=\"Search\"]")).SendKeys("Parametric");
            //Act
            _driverGc.FindElement(By.XPath(@"/html/body/div/div[3]/form/div[2]/div[1]/div[3]/center/input[1]")).Click();
            //Assert
            Assert.IsTrue(_driverGc.Url.Contains("/search"));
        }
    }
}