using System.IO;
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
            DriverGc.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists("elementScreenshot.png"))
                File.Delete("elementScreenshot.png");
            DriverGc.Close();
            DriverGc.Quit();
        }
    }
}