using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selen4TechDemo
{
    [TestFixture]
    public class TestBaseClass
    {
        internal static IWebDriver DriverGc;
        internal string ScreenShotFileName = "ElementScreenShot.png";

        [SetUp]
        public void Setup()
        {
            DriverGc = new ChromeDriver();
            DriverGc.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(ScreenShotFileName))
                File.Delete(ScreenShotFileName);
            DriverGc.Close();
            DriverGc.Quit();
        }
    }
}