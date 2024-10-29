using OpenQA.Selenium;
using STA_Coding_Challenge.Utilities;


namespace STA_Coding_Challenge.PageObject
{
    // Represents the journey results page where users can view journey details.
    public class JourneyResults_Page
    {
        private IWebDriver driver; // The WebDriver instance for browser interactions

        public JourneyResults_Page(IWebDriver _driver)
        {
            this.driver = _driver;
        }

        // Element locators
        By cyclingTime = By.CssSelector("a[class='journey-box cycling'] div[class='col2 journey-info']");
        By walkingTime = By.CssSelector("a[class='journey-box walking'] div[class='col2 journey-info']");
        By editPrefrence = By.CssSelector(".toggle-options.more-options");
        By leastWalkingTime = By.XPath("//label[normalize-space()='Routes with least walking']");
        By updateJourneyButton = By.CssSelector("div[id='more-journey-options'] div input[value='Update journey']");
        By updatedWalkingTime = By.CssSelector("div[id='option-1-heading'] div[class='journey-time no-map']");
        By loaderMessage = By.Id("loader-message");
        By viewDetailsButton = By.XPath("//button[contains(.,'View details')]");
        By upstairs = By.CssSelector("div[class='access-information'] a[aria-label='Up stairs']");
        By uplift = By.CssSelector("div[class='access-information'] a[aria-label='Up lift']");
        By levelWalkway = By.CssSelector("div[class='access-information'] a[aria-label='Level walkway']");
        By disambiguationMsg = By.XPath("//div[@class='info-message disambiguation']//span[1]");

        // Verifies the cycling and walking times displayed on the journey results page.
        public void VerifyCyclingAndWalkingTime(int expectedCyclingTime, int expectedWalkingTime)
        {
            WebAutomation.ExplicitWait(driver, editPrefrence, 10);
            // Retrieve and convert to time in minutes
            int actualCyclingTime = WebAutomation.ConverToMinutes(driver.FindElement(cyclingTime).Text);
            int actualWalkingTime = WebAutomation.ConverToMinutes(driver.FindElement(walkingTime).Text);
            // Verify cycling time
            if (actualCyclingTime != expectedCyclingTime)
                throw new ApplicationException($"Cycling time mismatch. Expected {expectedCyclingTime} but got {actualCyclingTime}");
            // Verify walking time
            if (expectedWalkingTime != actualWalkingTime) 
                throw new ApplicationException($"Walking time mismatch. Expected {expectedWalkingTime} but got {actualWalkingTime}");
        }

        // Updates the journey preferences to select the route with the least walking time.
        public void UpdateJourney()
        {
            WebAutomation.ExplicitWait(driver, editPrefrence, 10);
            driver.FindElement(editPrefrence).Click();
            WebAutomation.ExplicitWait(driver, leastWalkingTime,10);
            driver.FindElement(leastWalkingTime).Click(); // Select least walking time option
            Thread.Sleep(500); //Wait for previous action to be completed.
            driver.FindElement(updateJourneyButton).Click(); // Click the update journey button
            WebAutomation.ExplicitWait(driver, updatedWalkingTime, 10);
        }

        // Verifies the updated walking time displayed after changing journey preferences.
        public void VerifyUpdatedWalkingTime(int expectedWalkingTime)
        {
            WebAutomation.ExplicitWait(driver, viewDetailsButton, 30);
            string time = driver.FindElement(updatedWalkingTime).Text.Replace("Total time:\r\n", ""); // Get and clean up time text
            int actualWalkingTime = WebAutomation.ConverToMinutes(time); // Convert hours/mins text to minutes(int)
            if (actualWalkingTime != expectedWalkingTime) // Verify updated walking time
                throw new ApplicationException($"Walking time mismatch. Expected {expectedWalkingTime} but got {actualWalkingTime}");
        }

        // Verifies the presence of access details after viewing journey details.
        public void VerifyViewDetails()
        {
            WebAutomation.ExplicitWait(driver, viewDetailsButton, 30);
            driver.FindElement(viewDetailsButton).Click(); // Click on the view details button
            // Verify the presence of access information
            if (!WebAutomation.IsElementPresent(driver, uplift))
                throw new ApplicationException("Up lift information not present.");
            if (!WebAutomation.IsElementPresent(driver, upstairs))
                throw new ApplicationException("Up stairs information not present.");
            if (!WebAutomation.IsElementPresent(driver, levelWalkway))
                throw new ApplicationException("Level walkway information not present.");
        }

        // Verifies the displayed message for an invalid location input.
        public void VerifyResultForInvalidLocation(string invalidLocation)
        {
            WebAutomation.ExplicitWait(driver, disambiguationMsg, 30);
            // Verify that the message contains the expected text
            if (!WebAutomation.DoesPageContainsText(driver, driver.FindElement(disambiguationMsg).Text))
                throw new ApplicationException($"Result displayed for invalid location {invalidLocation}. Expected: Should not display result.");
        }
    }
}
