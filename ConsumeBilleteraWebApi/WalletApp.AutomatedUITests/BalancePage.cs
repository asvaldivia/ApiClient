using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace WalletApp.AutomatedUITests
{
    class BalancePage
    {
        private readonly IWebDriver _driver;
        private const string URI = "http://localhost:27246/Billeteras/Balance";

        private IWebElement OtraTransaccionEnlace => _driver.FindElement(By.TagName("a href"));
        private IWebElement HistoricoTransaccionesEnlace => _driver.FindElement(By.TagName("a href"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;

        public string Balance => _driver.FindElement(By.Id("balance")).Text;

        public BalancePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
            .GoToUrl(URI);

        public void ClickOtraTransaccion() => OtraTransaccionEnlace.Click();
        public void ClickHistoricoTransacciones() => HistoricoTransaccionesEnlace.Click();
        public int GetBalance() => int.Parse(Balance);

    }
}
