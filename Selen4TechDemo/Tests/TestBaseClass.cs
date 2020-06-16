using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selen4TechDemo.Tests
{
    [TestFixture]
    public class TestBaseClass
    {
        internal static IWebDriver DriverGc;

        [SetUp]
        public void Setup()
        {
            DriverGc = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            DriverGc.Close();
            DriverGc.Quit();
        }
    }
}