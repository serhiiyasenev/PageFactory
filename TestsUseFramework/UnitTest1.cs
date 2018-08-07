using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TestFramework.Pages;

namespace TestsUseFramework
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private string _url = "https://rozetka.com.ua/hudojestvennaya-literatura/c4326593/";

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");

            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(_url);
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => d.Url == _url);
            Thread.Sleep(10000);
        }

        [TestCleanup]
        public void TestFinalize()
        {
            driver.Close();
        }

        [TestMethod]
        public void TooLowMinPriceShouldUpdatePriceToMinimalAvailable()
        {
            //Arrange
            var booksResultsPage = new FilterPage(driver);
            var minPriceValueToSet = 1;
            var maxPriceValueToSet = 250;

            //Act
            booksResultsPage
                .SetMinimumPrice(minPriceValueToSet)
                .SetMaximumPrice(maxPriceValueToSet)
                .FilterByPrice();

            //Assert
            Console.WriteLine($"Actual minimum price {booksResultsPage.GetMinPrice()}, expected greater than {minPriceValueToSet}");
            var isMinPriceCorrect = booksResultsPage.GetMinPrice() >= minPriceValueToSet;
            Console.WriteLine($"Actual maximum price {booksResultsPage.GetMinPrice()}, expected {maxPriceValueToSet}");
            var isMaxPriceCorrect = booksResultsPage.GetMaxPrice() == maxPriceValueToSet;

            var checksPassed = isMaxPriceCorrect && isMinPriceCorrect;
            checksPassed.Should().BeTrue("because filters should work correctly");
        }

        [TestMethod]
        public void FilterByPrice()
        {
            //Arrange
            var booksResultsPage = new FilterPage(driver);
            var bookPage = new GoodsItemPage(driver);
            var minPriceValueToSet = 20;
            var maxPriceValueToSet = 250;

            //Act
            booksResultsPage
                .SetMinimumPrice(minPriceValueToSet)
                .SetMaximumPrice(maxPriceValueToSet)
                .FilterByPrice();

            booksResultsPage.ResultSet[0].Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => d.Url != _url);

            //Assert
            var actualPrice = bookPage.GetPrice();
            actualPrice.Should().BeGreaterOrEqualTo(minPriceValueToSet);
            actualPrice.Should().BeLessOrEqualTo(maxPriceValueToSet);
        }
    }
}