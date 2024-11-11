using FluentAssertions;
using LoginTests.Pages;
using OpenQA.Selenium;
using Serilog;

namespace LoginTests
{

    public class LoginEmptyCredentialsTestClass
    {
        private readonly ILogger _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        [Theory]
        [InlineData("Chrome")]
        [InlineData("Firefox")]
        public void LoginEmptyCredentialsTest(string browser)
        {
            try
            {
                _logger.Information("Starting LoginEmptyCredentialsTest with browser: {Browser}", browser);
                using (var loginPage = new LoginPage(browser))
                {
                    string errorText = loginPage.Open()
                        .FindLoginElements()
                        .SendCredentialsToForm("dummy_username", "dummy_password")
                        .ClearLoginInput()
                        .SubmitLoginAndWaitTwoSeconds()
                        .FindErrorElement()
                        .GetErrorElementText();

                    errorText.Should().Contain("Username is required");
                    _logger.Information("Successfully ended LoginEmptyCredentialsTest with browser: {Browser}", browser);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred during the test execution in LoginEmptyCredentialsTest with browser: {Browser}", browser);
                throw;
            }

        }
    }

    public class LoginWithoutPasswordTestClass
    {
        private readonly ILogger _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        [Theory]
        [InlineData("Chrome")]
        [InlineData("Firefox")]
        public void LoginWithoutPasswordTest(string browser)
        {
            try
            {
                _logger.Information("Starting LoginWithoutPasswordTest with browser: {Browser}", browser);
                using (var loginPage = new LoginPage(browser))
                {
                    string errorText = loginPage.Open()
                    .FindLoginElements()
                    .SendCredentialsToForm("dummy_username", "dummy_password")
                    .ClearPasswordInput()
                    .SubmitLoginAndWaitTwoSeconds()
                    .FindErrorElement()
                    .GetErrorElementText();

                    errorText.Should().Contain("Password is required");
                    _logger.Information("Successfully ended LoginWithoutPasswordTest with browser: {Browser}", browser);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred during the test execution in LoginWithoutPasswordTest with browser: {Browser}", browser);
                throw;
            }

        }
    }

    public class LoginWithAcceptedCredensialsTestClass
    {
        private readonly ILogger _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        [Theory]
        [InlineData("Chrome")]
        [InlineData("Firefox")]
        public void LoginWithAcceptedCredensialsTest(string browser)
        {
            try
            {
                _logger.Information("Starting LoginWithAcceptedCredensialsTest with browser: {Browser}", browser);
                using (var loginPage = new LoginPage(browser))
                {
                    string title = loginPage.Open()
                    .FindLoginElements()
                    .SendCredentialsToForm(loginPage.GetAcceptedUsername(), "secret_sauce")
                    .SubmitLoginAndWaitTwoSeconds()
                    .GetDashboardTitleText();

                    title.Should().Be("Swag Labs");
                    _logger.Information("Successfully ended LoginWithAcceptedCredensialsTest with browser: {Browser}", browser);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred during the test execution in LoginWithAcceptedCredensialsTest with browser: {Browser}", browser);
                throw;
            }

        }
    }
}