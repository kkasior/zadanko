using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using TidioTests.Pages;

namespace Tests
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void SendingMessageFromWidget()
        {
            string loginEmail = "karol.kasior@gmail.com";
            string loginPassword = "ThisIsMyVeryStrongPassword";
            string message = "samplemessage";
            string sendingEmail = "sample@email.com";

            SampleWidgetPage sampleWidgetPage = new SampleWidgetPage(driver);
            var tidioChat = sampleWidgetPage.OpenChat();
            tidioChat.EnterTheMessage(message);
            tidioChat.SendTheMessage(sendingEmail);
            tidioChat.VerifyIfCorrectMessageWasSent(message);

            LoginPage loginPage = new LoginPage(driver);
            var dashboardPage = loginPage.LoginToDashboardPage(loginEmail, loginPassword);
            var conversationWindow = dashboardPage.OpenConversationWindowIfThereIsNewMessage();
            conversationWindow.OpenReceivedMessageIfReceivedMessageIsInUnassigned(sendingEmail);

            conversationWindow.VerifyIfReceivedMessageIsCorrect(message);
        }

        [TearDown]
        public void After()
        {
            driver.Quit();
        }
    }
}