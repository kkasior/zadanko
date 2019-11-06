using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TidioTests.Pages
{
    public class TidioChat
    {
        private IWebDriver Driver;
        
        public TidioChat(IWebDriver driver)
        {
            Driver = driver;
        }
        private IWebElement NewMessageTextArea => Driver.FindElement(By.Id("new-message-textarea"));

        private IWebElement SendMessageButton => Driver.FindElement(By.Id("button-body"));

        private IWebElement EmailInput => Driver.FindElement(By.CssSelector("input[type='email']")); 

        private IWebElement Form => Driver.FindElement(By.TagName("Form"));

        private IWebElement MessageSentByVisitor => Driver.FindElement(By.CssSelector("#messages > div.message.message-visitor > span"));

        public void EnterTheMessage(string message)
        {
            NewMessageTextArea.SendKeys(message);
        }

        public void SendTheMessage(string email)
        {
            SendMessageButton.Click();
            EmailInput.SendKeys(email);
            var submitButton = Form.FindElement(By.XPath("//button[text() = 'Send']"));

            Actions actions = new Actions(Driver);
            actions.MoveToElement(submitButton).Build().Perform();
            submitButton.Click();
        }

        public void VerifyIfCorrectMessageWasSent(string message)
        {
            Assert.AreEqual(message, MessageSentByVisitor.Text);
        }
    }
}