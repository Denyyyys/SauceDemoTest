using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LoginTests.Pages
{
    public class LoginPage : IDisposable
    {
        private static string Url { get; } = "https://www.saucedemo.com/";

        private IWebDriver driver;

        private IWebElement? usernameInput;

        private IWebElement? passwordInput;

        private IWebElement? loginButton;

        private IWebElement? errorElement;

        public LoginPage(string browser)
        {
            driver = WebDriverFactory.CreateDriver(browser);
        }

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LoginPage Open()
        {
            driver.Navigate().GoToUrl(Url);
            return this;
        }

        public LoginPage FindLoginElements()
        {
            usernameInput = driver.FindElement(By.XPath("//*[@id='user-name']"));
            passwordInput = driver.FindElement(By.XPath("//*[@id='password']"));
            loginButton = driver.FindElement(By.XPath("//*[@id='login-button']"));
            return this;
        }

        public LoginPage SendCredentialsToForm(string username, string password)
        {
            usernameInput?.SendKeys(username);
            passwordInput?.SendKeys(password);
            return this;
        }

        public LoginPage ClearUsernameInput()
        {
            usernameInput?.SendKeys(Keys.Control + "a" + Keys.Delete);
            return this;
        }

        public LoginPage ClearPasswordInput()
        {
            passwordInput?.SendKeys(Keys.Control + "a" + Keys.Delete);
            return this;
        }

        public LoginPage ClearLoginInput()
        {
            ClearUsernameInput();
            ClearPasswordInput();
            return this;
        }

        public LoginPage SubmitLoginAndWaitTwoSeconds()
        {
            Actions clickAndSendKeysActions = new Actions(driver);
            clickAndSendKeysActions
                .Click(loginButton)
                .Pause(TimeSpan.FromSeconds(2))
                .Perform();
            return this;
        }

        public LoginPage FindErrorElement()
        {
            errorElement = driver.FindElement(By.XPath("//*[@data-test='error']"));
            return this;
        }

        public string GetDashboardTitleText()
        {
            return driver.FindElement(By.XPath("//*[@class='app_logo']")).Text;
        }

        public string GetErrorElementText()
        {
            ArgumentNullException.ThrowIfNull(errorElement);
            return errorElement.Text;
        }

        public string GetAcceptedUsername()
        {
            IWebElement acceptedUsernamesHeader = driver.FindElement(By.XPath("//div[@id='login_credentials']/*[text() = 'Accepted usernames are:']"));

            string firstUsername = (string)((IJavaScriptExecutor)driver).ExecuteScript(
                "return arguments[0].nextSibling.nodeValue;", acceptedUsernamesHeader);

            return firstUsername;
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
