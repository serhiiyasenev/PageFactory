using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TestFramework.Pages
{
    public class FilterPage : BasePage
    {
        public FilterPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.Id, Using = "price[min]")]
        private IWebElement _minimumPrice;

        [FindsBy(How = How.Id, Using = "price[max]")]
        private IWebElement _maximumPrice;

        [FindsBy(How = How.CssSelector, Using = ".g-i-tile-i.available")]
        public IList<IWebElement> ResultSet;

        [FindsBy(How = How.Id, Using = "submitprice")]
        private IWebElement _filterByPrice;


        public FilterPage SetMinimumPrice(int? price)
        {
            if (price == null) return this;
            _minimumPrice.SendKeys(price.ToString());
            return this;
        }

        public FilterPage SetMaximumPrice(int? price)
        {
            if (price == null) return this;
            _maximumPrice.Click();
            _maximumPrice.Clear();
            _maximumPrice.SendKeys(price.ToString());
            return this;
        }

        public int? GetMinPrice()
        {
            var stringValue = string.IsNullOrEmpty(_minimumPrice.Text)
                ? _minimumPrice.GetAttribute("value") : _minimumPrice.Text;
            if (string.IsNullOrEmpty(stringValue))
                return null;
            else
            {
                int.TryParse(stringValue, out int result);
                return result;
            }
        }

        public int? GetMaxPrice()
        {
            var stringValue = string.IsNullOrEmpty(_maximumPrice.Text)
                ? _minimumPrice.GetAttribute("value") : _minimumPrice.Text;
            if (string.IsNullOrEmpty(stringValue))
                return null;
            else
            {
                int.TryParse(stringValue, out int result);
                return result;
            }
        }

        public void FilterByPrice()
        {
            _filterByPrice.Click();
        }
    }
}