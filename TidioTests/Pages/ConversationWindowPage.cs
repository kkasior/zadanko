using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TidioTests.Pages
{
    public class ConversationWindowPage 
    {
        private IWebDriver Driver;

        public ConversationWindowPage(IWebDriver driver)
        {
            Driver = driver;
        }
        private IWebElement VisitorConversations => Driver.FindElement(By.Id("visitor-conversations"));

        private IWebElement VisitorWindow => Driver.FindElement(By.ClassName("visitor-window"));

        private IList<IWebElement> MessageWrappers => Driver.FindElements(By.ClassName("message-wrapper"));

        public void OpenReceivedMessageIfReceivedMessageIsInUnassigned(string email)
        {
            IWebElement sendingEMailInVisitorConversationList = VisitorConversations.FindElement(By.XPath($"//span[@title = '{email}']"));

            bool isReceivedMessageInUnassigned = VerifyIfReceivedMessageIsInUnassigned(sendingEMailInVisitorConversationList);

            if (isReceivedMessageInUnassigned)
            {
                sendingEMailInVisitorConversationList.Click();
            }
        }

        public void VerifyIfReceivedMessageIsCorrect(string message)
        {
            IWebElement lastMessage = GetTheLastMessage(message);
            GetTheLastMessageTime(lastMessage);
            var date = DateTime.Now;
            var lastMessageTime = GetTheLastMessageTime(lastMessage);

            Assert.AreEqual(message,lastMessage.Text);
            Assert.That(date, Is.EqualTo(lastMessageTime).Within(2).Minutes);
        }

        public bool VerifyIfReceivedMessageIsInUnassigned(IWebElement sendingEMailInVisitorConversationList)
        {
            IList<IWebElement> sendingEmailInUnassigned = sendingEMailInVisitorConversationList.FindElements(By.XPath("//./following-sibling::span[@class = 'label new']"));
            return sendingEmailInUnassigned.Count > 0;
        }

        private IWebElement GetTheLastMessage(string message)
        {
            IList<IWebElement> messagesFromSendingEMail = new List<IWebElement>();

            foreach (var messageWrapper in MessageWrappers)
            {
                
                IWebElement messageFromMessageWrapper = messageWrapper.FindElement(By.CssSelector("div > span"));
                if (messageFromMessageWrapper.Text == message)
                {
                    messagesFromSendingEMail.Add(messageFromMessageWrapper);
                }
            }

            IWebElement lastMessage = messagesFromSendingEMail[messagesFromSendingEMail.Count - 1];

            return lastMessage;
        }

        private DateTime GetTheLastMessageTime(IWebElement lastMessage)
        {
            var time = lastMessage.GetAttribute("data-time");
            DateTime lastMessageDate = DateTime.ParseExact(time, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            return lastMessageDate;
        }
    }
}
