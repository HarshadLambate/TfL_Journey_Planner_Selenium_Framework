using OpenQA.Selenium;
using STA_Coding_Challenge.PageObject;
using STA_Coding_Challenge.Utilities;
using System;
using TechTalk.SpecFlow;

namespace STA_Coding_Challenge.StepDefinitions
{
    [Binding] // Indicates class containing SpecFlow step definitions
    public class PlanJourneyStepDefinitions
    {
        private TestHelper helper = new TestHelper();  // Helper class for various test utilities
        private IWebDriver driver; // WebDriver instance
        private Home_Page home_Page; // Page object for the home page
        private JourneyResults_Page journeyResultsPage; // Page object for journey results page

        public PlanJourneyStepDefinitions(IWebDriver _driver)
        {
            this.driver = _driver; // Assign the WebDriver instance to the class field
            home_Page = new Home_Page(driver); // Initialize home page object
            journeyResultsPage = new JourneyResults_Page(driver); // Initialize journey results page object
        }

        [Given(@"I am on TFL Homepageto plan a journey")]
        public async Task GivenIAmOnTFLHomepagetoPlanAJourney()
        {
            var env = await helper.GetEnvironmentDataAsync(); // Retrieve environment data asynchronously
            driver.Navigate().GoToUrl(env.Url); // Navigate to the given URL
            home_Page.ClickCookiesPrefrence(); // Click to accept cookie preferences
        }

        [Given(@"I enter source '([^']*)' and destination '([^']*)'")]
        public void GivenIEnterSourceAndDestination(string from, string to)
        {
            home_Page.EnterPlaceWithAutoSuggestion(from, to); // Enter source and destination with auto-suggestions
        }

        [When(@"I submit my details")]
        public void WhenISubmitMyDetails()
        {
            home_Page.ClickSubmitButton();  // Click the submit button to plan the journey
        }

        [Then(@"I validate cycling time is (.*) mins and walking time is (.*) mins")]
        public void ThenIValidateCyclingTimeIsMinsAndWalkingTimeIsMins(int expectedCyclingTime, int expectedWalkingTime)
        {// Verify the cycling and walking times
            journeyResultsPage.VerifyCyclingAndWalkingTime(expectedCyclingTime, expectedWalkingTime);
        }

        [Given(@"I am on journey result page")]
        public async Task GivenIAmOnJourneyResultPage()
        {
            var env = await helper.GetEnvironmentDataAsync(); // Retrieve environment data asynchronously
            driver.Navigate().GoToUrl(env.ResultUrl); // Navigate to the given URL
            WebAutomation.WaitForPageToLoad(driver);
            home_Page.ClickCookiesPrefrence(); // Click to accept cookie preferences
        }

        [When(@"I edit my prefrence with least walking journey")]
        public void WhenIEditMyPrefrenceWithLeastWalkingJourney()
        {
            journeyResultsPage.UpdateJourney(); // Update the journey preferences to select the least walking option
        }

        [Then(@"I validate my updated walking time is (.*)")]
        public void ThenIValidateMyUpdatedWalkingTimeIs(int expectedWalkingTime)
        {
            journeyResultsPage.VerifyUpdatedWalkingTime(expectedWalkingTime); // Verify the updated walking time
        }

        [Then(@"I click on View Details and verify the information")]
        public void ThenIClickOnViewDetailsAndVerifyTheInformation()
        {
            journeyResultsPage.VerifyViewDetails(); // Verify the access details displayed after clicking "View Details"
        }

        [Given(@"I validate result for invalid source '([^']*)' and valid destination '([^']*)'")]
        public void GivenIValidateResultForInvalidSourceAndValidDestination(string invaidSource, string validDestination)
        {
            home_Page.EnterPlace(invaidSource, validDestination); // Enter invalid source and valid destination
            home_Page.ClickSubmitButton(); // Submit the journey
            journeyResultsPage.VerifyResultForInvalidLocation(invaidSource); // Verify the result for the invalid source
        }

        [Then(@"I validate result for invalid destination '([^']*)' and valid source '([^']*)'")]
        public void ThenIValidateResultForInvalidDestinationAndValidSource(string validSource, string invalidDestination)
        {
            home_Page.EnterPlace(validSource, invalidDestination); // Enter valid source and invalid destination
            home_Page.ClickSubmitButton(); // Submit the journey
            journeyResultsPage.VerifyResultForInvalidLocation(invalidDestination); // Verify the result for the invalid destination
        }

        [Then(@"I verify widget does not plan journey when location is blank")]
        public void ThenIVerifyWidgetDoesNotPlanJourneyWhenLocationIsBlank()
        {// Verify that the validation message appears when the location is blank
            home_Page.VerifyValidationMessage();
        }
    }
}
