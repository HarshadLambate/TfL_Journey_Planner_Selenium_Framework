using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using STA_Coding_Challenge.Utilities;
using TechTalk.SpecFlow;
using BoDi;

namespace STA_Coding_Challenge.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        private IWebDriver driver;
        public Hooks(IObjectContainer container)
        {
            _container = container;
        }
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {// Get the browser name from the configuration
            string browser = TestHelper.GetAppConfig().Browser; 
            
            switch (browser?.ToLower())
            {// Initialize the WebDriver based on the browser
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new NotSupportedException($"Browser is not supported.");
            }

            driver.Manage().Window.Maximize(); // Maximize the browser window
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {// Resolve the WebDriver from the container
            var driver = _container.Resolve<IWebDriver>();
            // Quit the driver if it's not null
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error quitting the driver: {ex.Message}");
                }
            }
        }
    }
}
