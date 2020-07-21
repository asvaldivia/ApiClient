using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;
using System.Linq.Expressions;


namespace WalletApp.AutomatedUITests
{

    public class AutomatedUITests: IDisposable
    {


        private readonly IWebDriver _driver;
        private readonly BalancePage _balance;
        private readonly OperationPage _operation;

        public AutomatedUITests()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            _driver = new ChromeDriver(@".\", chromeOptions);

            _balance = new BalancePage(_driver);
            //_balance.Navigate();
            _operation = new OperationPage(_driver);
            //_operation.Navigate();
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void Open_Balance_ReturnsBalanceView()
        {
             
            _balance.Navigate();
            Assert.Equal("Balance - My ASP.NET Application", _balance.Title);
            Assert.Contains("Otra Transaccion", _balance.Source);
            Assert.Contains("Historico de Transacciones", _balance.Source);
        
        }

        [Fact]
        public void Deposit_Correct_ReturnsBalanceView()
        {
            _operation.Navigate();

            _operation.Wait(5);

            _operation.PopulateOperacion("deposito");

            _operation.PopulateMonto("10");

            _operation.ClickConfirmar();

            Assert.Contains("Otra Transaccion", _balance.Source);
            Assert.Contains("Historico de Transacciones", _balance.Source);

        }

        [Fact]
        public void Deposit_Correct_ReturnsBalanceView_BalanceCorrect()
        {
            _balance.Navigate();

            int BalanceBeforeOperation = _balance.GetBalance();

            _operation.Navigate();

            _operation.Wait(5);

            _operation.PopulateOperacion("deposito");

            string monto = "10";
            _operation.PopulateMonto(monto);

            _operation.ClickConfirmar();

            Assert.Contains("Otra Transaccion", _balance.Source);
            Assert.Contains("Historico de Transacciones", _balance.Source);
            Assert.Equal(BalanceBeforeOperation+int.Parse(monto),int.Parse(_balance.Balance));

        }

        [Theory, CsvData(true)]
        
        public void MasiveOperations_Correct_ReturnsBalanceView(string transaccion, string monto)
        {
            _balance.Navigate();

            int BalanceBeforeOperation = _balance.GetBalance();

            _operation.Navigate();

            _operation.Wait(5);

            _operation.PopulateOperacion(transaccion);

            _operation.PopulateMonto(monto);

            _operation.ClickConfirmar();

            Assert.Contains("Otra Transaccion", _balance.Source);
            Assert.Contains("Historico de Transacciones", _balance.Source);
            if(transaccion == "deposito")
                Assert.Equal(BalanceBeforeOperation + int.Parse(monto), int.Parse(_balance.Balance));
            if(transaccion == "retiro")
                Assert.Equal(BalanceBeforeOperation - int.Parse(monto), int.Parse(_balance.Balance));
        }

        [Fact]
        public void Withdraw_Incorrect_ReturnsErrorMessage()
        {
            _operation.Navigate();

            _operation.Wait(5);

            _operation.PopulateOperacion("retiro");

            _operation.PopulateMonto("0");

            _operation.ClickConfirmar();

            Assert.Equal("Error, contacta con el administrador", _operation.OperationErrorMessage);

        }
    }
}
