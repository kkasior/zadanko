using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TidioTests.Pages
{
    public class SampleWidgetPage
    {
        private string Url = "https://widget-v4.tidiochat.com/preview.html?code=fpvjcccgzws73b73mfxbws69c0hxpzaz";
        private IWebDriver Driver;
       
        public SampleWidgetPage(IWebDriver driver)
        {
            Driver = driver;
            Driver.Url = Url;
        }

        private IWebElement ChatIFrame => Driver.FindElement(By.Id("tidio-chat-iframe"));
        
        public TidioChat OpenChat()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            Driver.SwitchTo().Frame(ChatIFrame);
            var widgetButton = Driver.FindElement(By.Id("button-body"));
            widgetButton.Click();
            return new TidioChat(Driver);
        }


    }
}
