using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TidioTests.Pages
{
    public class DashboardPage
    {
        private IWebDriver Driver;

        public DashboardPage(IWebDriver driver)
        {
            Driver = driver;
        }

        private IWebElement ChatIcon => Driver.FindElement(By.CssSelector("#menu > li:first-child > a"));

        private IWebElement NewMessageNotification => Driver.FindElement(By.CssSelector("#menu > li:first-child > a > span > span"));

        public ConversationWindowPage OpenConversationWindowIfThereIsNewMessage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("progress-bar")));

            if (NewMessageNotification.Displayed && NewMessageNotification.Enabled)
            {
                ChatIcon.Click();
            }
            return new ConversationWindowPage(Driver);
        }
    }
}
