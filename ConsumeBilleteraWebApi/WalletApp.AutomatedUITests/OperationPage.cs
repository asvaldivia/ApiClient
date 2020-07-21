using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace WalletApp.AutomatedUITests
{
    class OperationPage
    {
        private readonly IWebDriver _driver;
        private const string URI = "http://localhost:27246/Billeteras/Create";

        private IWebElement OperacionElement => _driver.FindElement(By.Id("operacion"));
        private IWebElement MontoElement => _driver.FindElement(By.Id("monto"));
        private IWebElement ConfirmarElement => _driver.FindElement(By.Id("confirmar"));
        public string OperationErrorMessage => _driver.FindElement(By.Id("danger")).Text;

        public OperationPage( IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
            .GoToUrl(URI);
        public void PopulateOperacion(string operacion) => OperacionElement.SendKeys(operacion);
        public void PopulateMonto(string monto) => MontoElement.SendKeys(monto);
        public void ClickConfirmar() => ConfirmarElement.Click();
        public void Wait(int seconds) => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
    }
}
