using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TestFramework.Pages
{
    public class GoodsItemPage : BasePage
    {
        private IWebDriver _driver;

        public GoodsItemPage(IWebDriver driver) : base(driver)
        { }

        [FindsBy(How = How.Id, Using = "price_label")]
        public IWebElement Price;

        public int? GetPrice()
        {
            var stringValue = Price.Text;
            if (stringValue == null | stringValue == "")
                return null;
            else
            {
                int.TryParse(stringValue, out int result);
                return result;
            }
        }
    }
}