using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;
using System.Collections;

namespace CubeAutomation
{
    public class Program
    {
        public static IWebDriver driver;
        public static void Main(string[] args)
        {
            Console.WriteLine("Program start...");
            //List<string> midList = File.ReadAllLines(midfile).Skip(1).Select(v => MidFromCsv(v)).ToList();
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            using (driver = new ChromeDriver(options))
            {
                try
                {
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(300);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
                    driver.Navigate().GoToUrl("https://adcb.paysky.io/Portal/Account/Login");

                    WaitUntilElementClickable("UserName");

                    IWebElement userNameTextBox = driver.FindElement(By.Id("UserName"));
                    userNameTextBox.Clear();
                    userNameTextBox.SendKeys("waqarmaker");

                    IWebElement userPasswordTextBox = driver.FindElement(By.Id("userpassword"));
                    userPasswordTextBox.Clear();
                    userPasswordTextBox.SendKeys("Fiserv@M2024$");

                    driver.FindElement(By.Id("sendotp")).Click();

                    Thread.Sleep(3000);

                    IWebElement otpTextBox = driver.FindElement(By.Id("otp"));
                    otpTextBox.Clear();
                    otpTextBox.SendKeys("1111");

                    driver.FindElement(By.Id("signinbtn")).Click();

                    Thread.Sleep(4000);

                    driver.Navigate().GoToUrl("https://adcb.paysky.io/Portal/MerchantManagement/Merchants/EditRecord?editID=-1");

                    WaitUntilElementClickable("MerchantID");

                    IWebElement merchantNameTextBox = driver.FindElement(By.Id("MerchantName"));
                    merchantNameTextBox.Clear();
                    merchantNameTextBox.SendKeys("MerchantName");
                    //merchantIdTextBox.SendKeys(Keys.Enter);
                    //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 100)");

                    IWebElement categorycodedropdown = driver.FindElement(By.Name("Merchant.CategoryCodeId_input"));
                    categorycodedropdown.Clear();
                    categorycodedropdown.SendKeys("6513");
                    Thread.Sleep(500);
                    categorycodedropdown.SendKeys(Keys.Enter);

                    IWebElement countryIddropdown = driver.FindElement(By.Name("Merchant.CountryId_input"));
                    countryIddropdown.Clear();
                    countryIddropdown.SendKeys("united arab");
                    Thread.Sleep(500);
                    countryIddropdown.SendKeys(Keys.Enter);

                    Thread.Sleep(1000);

                    IWebElement stateIddropdown = driver.FindElement(By.Name("Merchant.StateId_input"));
                    stateIddropdown.Clear();
                    stateIddropdown.SendKeys("dub");
                    Thread.Sleep(500);
                    stateIddropdown.SendKeys(Keys.Enter);

                    IWebElement merchantaddress1TextBox = driver.FindElement(By.Id("Merchant_Address1"));
                    merchantaddress1TextBox.Clear();
                    merchantaddress1TextBox.SendKeys("Merchant_Address1");

                    driver.FindElement(By.ClassName("selected-dial-code")).Click();

                    IWebElement element = driver.FindElement(By.CssSelector("li[data-dial-code=971]"));
                    element.Click();

                    IWebElement phoneNumberTextBox = driver.FindElement(By.Id("TempMerchant_ContactPersonPhone"));
                    phoneNumberTextBox.Clear();
                    phoneNumberTextBox.SendKeys("568691036");

                    driver.FindElement(By.Id("MerChantUserCanRefund")).Click();


                    Console.WriteLine("Program end...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
        public static void WaitUntilElementClickable(string id, int timeout = 20000)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
                IWebElement element = wait.Until(c => c.FindElement(By.Id(id)));
                element.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with id: '" + id + "' not found in current context page.");
                throw;
            }
        }
    }
}
