using OpenQA.Selenium;
using STA_Coding_Challenge.Utilities;

namespace STA_Coding_Challenge.PageObject
{
    // Represents the home page of the application where users can plan their journey.
    public class Home_Page
    {
        private IWebDriver driver; // The WebDriver instance for browser interactions
        public Home_Page(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        // Element locators
        By from = By.Id("InputFrom");
        By to = By.Id("InputTo");
        By planMyJourney = By.Id("plan-journey-button");
        By autoSuggestion = By.XPath("//span[contains(@id,'stop-points-search-suggestion')]");

        // Enters the source and destination places with auto-suggestions.
        public void EnterPlaceWithAutoSuggestion(string scource, string destination)
        {
            WebAutomation.ExplicitWait(driver, planMyJourney,30);
            EnterTextWithAutoSuggestion(from, scource); // Enter the source with auto suggestion
            EnterTextWithAutoSuggestion(to, destination); // Enter the destination with auto suggestion
        }

        public void ClickSubmitButton()
        {
            driver.FindElement(planMyJourney).Click();           
        }
        private void EnterTextWithAutoSuggestion(By elementLocator, string text)
        {
            var element = driver.FindElement(elementLocator);
            element.SendKeys(text);    // Enter the given text       
            element.SendKeys(Keys.Space); // Trigger auto suggestion by sending a space key

            WebAutomation.ExplicitWait(driver, autoSuggestion, 5, true); // wait for auto suggestions

            IList<IWebElement> suggestionList = driver.FindElements(autoSuggestion); // Retrieve list of suggestions

            if (suggestionList.Count > 1)
            {               
                suggestionList[0].Click();// Click on the desired suggestion (first from the list)
            }
            else
            {
                Console.WriteLine("Auto-suggestions not available or not enough suggestions found.");
            }
        }

        // Clicks the cookie preferences button if it is present.
        public void ClickCookiesPrefrence()
        {
            if (WebAutomation.DoesPageContainsText(driver, "Your cookie settings"))
            {
                WebAutomation.ClickButtonByText(driver, "Accept all cookies");
            }
            driver.Navigate().Refresh();
        }   
        
        public void VerifyValidationMessage()
        {            
            ClickSubmitButton();
            if (!WebAutomation.DoesPageContainsText(driver, "field is required."))
                throw new ApplicationException("Details not filled."); // Throw error if validation message not displayed
        }

        // enter location without auto suggestion for complete input or invalid input
        public void EnterPlace(string source, string destination)
        {
            driver.FindElement(from).SendKeys(source);
            driver.FindElement(to).SendKeys(destination);
        }
    }
}
