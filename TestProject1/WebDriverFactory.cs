using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace LoginTests
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver;
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "Firefox":
                    var firefoxService = FirefoxDriverService.CreateDefaultService(AppContext.BaseDirectory, "geckodriver.exe");
                    driver = new FirefoxDriver(firefoxService);
                    break;

                default:
                    throw new ArgumentException("Unsupported browser: " + browser);
            }

            return driver;
        }
    }
}
