using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TidioTests.Pages
{
    public class LoginPage
    {
        private string Url = "https://www.tidio.com/panel/login";
        private IWebDriver Driver;
        public LoginPage(IWebDriver driver)
        {
            Driver = driver;

            var js = (IJavaScriptExecutor)Driver;
            const string script = @"window.open();";
            js.ExecuteScript(script);

            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Driver.Url = Url;
        }


        private IWebElement EmailInput => Driver.FindElement(By.CssSelector("input[type='email']"));

        private IWebElement PasswordInput => Driver.FindElement(By.CssSelector("input[type='password']"));

        private IWebElement SubmitLoginButton => Driver.FindElement(By.ClassName("form__button"));

        public DashboardPage LoginToDashboardPage(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SubmitLoginButton.Click();
            return new DashboardPage(Driver);
        }
    }
}
