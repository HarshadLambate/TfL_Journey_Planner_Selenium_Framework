# TfL Journey Planner Automation Framework
**Project Overview:**

This project demonstrates my ability to create test automation scripts using Selenium with C#. It includes tests for the specified scenarios, executed through a browser of choice. <br />

**Tools and Frameworks Used:**<br />

C# (.NET 6): Programming language and framework used to write the test scripts.<br />
Selenium WebDriver: For browser automation and interaction with web elements.<br />
NUnit: Used to structure and run the test cases.<br />
ChromeDriver: Chrome browser driver used for test execution (configurable for other browsers).<br />

**Prerequisites:**<br />

.NET Core SDK: Ensure the .NET SDK is installed on your system.<br />
Chrome Browser: The project is configured to run tests on Chrome (can be configured for other browsers).<br />
NuGet Packages: The following packages are required and will be restored automatically on project build:<br />
  Selenium.WebDriver<br />
  SeleniumExtras.WaitHelpers<br />
  Selenium.WebDriver.ChromeDriver<br />
  NUnit<br />
  NUnit3TestAdapter<br />
  Specflow.Nunit<br />

**Setup Instructions:**<br />

Clone the repository to your local machine:<br />
Navigate to the project directory.<br />
Restore the necessary NuGet packages.<br />
Build the project to ensure everything is set up correctly.<br />
Run the test cases using Test Explorer.<br />

**Test Execution:**<br />

By default, the tests are configured to run on the Chrome browser. The ChromeDriver is automatically invoked during test execution.<br />
Test results will be displayed in the console output.<br />
To run the tests in other browsers (e.g., Firefox, Edge), modify the browser settings in the test configuration file.<br />
  
