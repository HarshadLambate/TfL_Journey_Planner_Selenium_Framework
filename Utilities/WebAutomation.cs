using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;

namespace STA_Coding_Challenge.Utilities
{
    // Utility class for common web automation functions using Selenium.
    public static class WebAutomation
    {
        // Waits explicitly for an element or collection of elements to be available.
        public static void ExplicitWait(IWebDriver driver, By elementLocator, int seconds, bool isCollection = false)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            if (isCollection)
            {
                // Wait elements in the collection to be present
                wait.Until(drv => drv.FindElements(elementLocator).Count >1);
            }
            else
            {
                // Wait for a single element to be clickable
                wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
        }

        // Clicks a button identified by its visible text.
        public static void ClickButtonByText(IWebDriver driver, string buttonText)
        {
            var buttonLocator = By.XPath($"//strong[normalize-space()='{buttonText}']");
            driver.FindElement(buttonLocator).Click();
        }

        // Waits until the page has fully loaded.
        public static void WaitForPageToLoad(IWebDriver driver, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(driver =>
            {
                return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString() == "complete";
            });
        }

        // Checks if the specified text is present anywhere on the page.
        public static bool DoesPageContainsText(IWebDriver driver, string text)
        {
            var bodyTag = driver.FindElement(By.TagName("body"));
            if(bodyTag.Text.Contains(text))
                return true;
            return false;
        }

        // Converts a string representation of time (ex. 1 hr 30 mins) to 90 minutes.
        public static int ConverToMinutes(string time)
        {// for scenarios conatining distance in hours and minutes
            int totalMinutes = 0;
            // Regular expression to match hours and minutes in the format '1 hr 30 mins'
            var match = Regex.Match(time, @"(?:(\d+)\s*hrs?)?\s*(?:(\d+)\s*mins?)?");

            if (match.Success)
            {
                // Parse hours if present
                if (!string.IsNullOrEmpty(match.Groups[1].Value) && int.TryParse(match.Groups[1].Value, out int hours))
                {
                    totalMinutes += hours * 60;
                }
                // Parse minutes if present
                if (!string.IsNullOrEmpty(match.Groups[2].Value) && int.TryParse(match.Groups[2].Value, out int minutes))
                {
                    totalMinutes += minutes;
                }
            }
            return totalMinutes;
        }

        // Checks if an element is present on the page.
        public static bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
