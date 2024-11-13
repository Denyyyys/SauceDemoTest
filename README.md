# Important
The task description is located at the end of this file. Before evaluating my solution, please make sure to read all sections, 
the one titled `Personal Notes`.

## Troubleshooting
If you encounter issues when running tests with Firefox, try to compile project and then place the `TestProject1/geckodriver.exe` file into `TestProject1/bin/Debug/net8.0` folder.

## Logging
To view detailed logs, navigate to your solution folder in the terminal and execute:
```bash
dotnet test --logger "console;verbosity=detailed"
```

Logs will appear in the console. Running `dotnet test` without this command will not display logs.

## Parallel execution

Each test case (UC-1, UC-2, UC-3) is executed in parallel. However, tests for different browsers (e.g., Chrome, Firefox) run sequentially.

## Data Provider

To parameterize tests, I used the following attributes:
```cs
        [Theory]
        [InlineData("Chrome")]
        [InlineData("Firefox")]
```

## Personal Notes

### Locator Strategy
- While it would be preferable to use other locators like `By.Id()` or `By.ClassName()` for finding elements, the task specifically required:

	**Locators: XPath**

	Therefore, I adhered to using only XPath locators.
### Clearing Input Fields

-   Initially, I used the `Clear()` method to clear input fields, but it only removed the text and not the value, leading to unexpected form submissions. To fix it, I used:
	```cs
	SendKeys(Keys.Control + "a" + Keys.Delete)
	```

### Handling Error Messages
- In the `LoginEmptyCredentialsTest` and `LoginWithoutPasswordTest`, I used:

	```csharp
	errorText.Should().Contain("Username is required");
	```
	and

	```csharp
	errorText.Should().Contain("Password is required"); 
	```
	instead of `errorText.Should().Be()` because the web page returned error messages such as _"Epic sadface: Username is required"_ and _"Epic sadface: Password is required"_ instead of the exact expected text.

### Finding Accepted Username
-   To avoid hard-coding an accepted username, I implemented logic to retrieve it from the web page, making the code more flexible. Initially, I attempted to use `driver.FindElement()` for that but it was throwing errors because the usernames were rendered as plain text rather than enclosed within tags like `<li>standard_user</li>`. That's why I had to use some JavaScript code in the `GetAcceptedUsername()` method in `LoginPage` class.

## Task description
Launch URL: [https://www.saucedemo.com/](https://www.saucedemo.com/ "https://www.saucedemo.com/")

**UC-1** Test Login form with empty credentials:

Type any credentials into "Username" and "Password" fields.

Clear the inputs.

Hit the "Login" button.

Check the error messages: "Username is required".

**UC-2** Test Login form with credentials by passing Username:

Type any credentials in username.

Enter password.

Clear the "Password" input.

Hit the "Login" button.

Check the error messages: "Password is required".

**UC-3** Test Login form with credentials by passing Username & Password:

Type credentials in username which are under Accepted username are sections.

Enter password as secret sauce.

Click on Login and validate the title “Swag Labs” in the dashboard.

Provide parallel execution, add logging for tests and use Data Provider to parametrize tests. Make sure that all tasks are supported by these 3 conditions: UC-1; UC-2; UC-3.

Please, add task description as [README.md](http://readme.md/ "http://readme.md/") into your solution!

**To perform the task use the various of additional options:**

Test Automation tool: Selenium WebDriver;

Browsers: 1) Firefox; 2) Chrome;

Locators: XPath;

Test Runner: xUnit;

_[Optional] Patterns: 1) Singleton; 2) Adapter; 3) Strategy;_

_[Optional]_ Test automation approach: BDD;

Assertions: Fluent Assertion;

_[Optional] Loggers: SeriLog._
