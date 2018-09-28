using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace SeleniumTest
{
    [TestFixture]
    public class TestsRegister
    {
        public List<string> pass = new List<string>();
        IWebDriver driver;

        [OneTimeSetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/");
        }
        [SetUp]
        public void SetUp()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/");
        }

        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            //driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/logout");

             driver.Close();
            //driver.Quit();
        }


        [Test, Order(0)]
        [TestCase("Orest", "Shkhumov", "@gmail.com", "0637239610", "04532354", "Soft", "Fedkovicha",
            "Sadova", "Lviv", "64738", "Ukraine", "L'vivs'ka Oblast'", "orest", "orest")]
        public void RegisterTests1(string FirstName, string LastName, string EMail, string Telephone, string Fax,
            string Company, string Address1, string Address2, string City, string PostCode, string Country,
            string Region, string Password, string PasswordConfirm)
        {
            driver.FindElement(By.ClassName("dropdown")).Click();
            driver.FindElement(By.CssSelector("#top-links > ul > li.dropdown.open > ul > li:nth-child(1) > a")).Click();


            driver.FindElement(By.Id("input-firstname")).SendKeys(FirstName);
            driver.FindElement(By.Id("input-lastname")).SendKeys(LastName);

            //Email            
            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            pass.Add(FirstName + randomInt + EMail);
            foreach (var i in pass)
            {
                driver.FindElement(By.Id("input-email")).SendKeys(i);
            }
            driver.FindElement(By.Id("input-telephone")).SendKeys(Telephone);
            driver.FindElement(By.Id("input-fax")).SendKeys(Fax);
            driver.FindElement(By.Id("input-company")).SendKeys(Company);
            driver.FindElement(By.Id("input-address-1")).SendKeys(Address1);
            driver.FindElement(By.Id("input-address-2")).SendKeys(Address2);
            driver.FindElement(By.Id("input-city")).SendKeys(City);
            driver.FindElement(By.Id("input-postcode")).SendKeys(PostCode);
            driver.FindElement(By.Id("input-country")).SendKeys(Country);
            driver.FindElement(By.Id("input-zone")).SendKeys(Region);

            driver.FindElement(By.Id("input-password")).SendKeys(Password);
            driver.FindElement(By.Id("input-confirm")).SendKeys(PasswordConfirm);
            //Subscribe
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.ClassName("radio-inline")), 1, 1).Click().Perform();
            //I have read and agree to the Privacy Policy
            Actions action2 = new Actions(driver);
            action2.MoveToElement(driver.FindElement(By.Name("agree")), 1, 1).Click().Perform();

            //button

            driver.FindElement(By.CssSelector("#content > form > div > div > input.btn.btn-primary")).Click();
            driver.FindElement(By.CssSelector("#content > div > div")).Click();

        }
        [Test, Order(1)]
        [TestCase("orest@gmail.com", "orest")]
        public void EditAccountTest2(string email, string password)
        {
            driver.FindElement(By.ClassName("dropdown")).Click();
            driver.FindElement(By.CssSelector("#top-links > ul > li.dropdown.open > ul > li:nth-child(2) > a")).Click();

            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys(email);

            driver.FindElement(By.Id("input-password")).Clear();
            
            driver.FindElement(By.Id("input-password")).SendKeys(password);
            
            driver.FindElement(By.CssSelector("#content > div > div:nth-child(2) > div > form > input")).Click();
            driver.FindElement(By.CssSelector("#column-right > div > a:nth-child(2)")).Click();

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(10);
            driver.FindElement(By.Id("input-telephone")).Clear();
            driver.FindElement(By.Id("input-telephone")).SendKeys("+38063723961" + randomInt);

            driver.FindElement(By.CssSelector("#content > form > div > div.pull-right > input")).Click();

        }
        [Test, Order(2)]
        [TestCase("orest")]
        public void LoginAndChangePass(string password)
        {
            driver.FindElement(By.ClassName("dropdown")).Click();
            driver.FindElement(By.CssSelector("#top-links > ul > li.dropdown.open > ul > li:nth-child(2) > a")).Click();

            driver.FindElement(By.Id("input-email")).Clear();
            
            
            driver.FindElement(By.Id("input-email")).SendKeys(password);
            
            driver.FindElement(By.Id("input-password")).SendKeys(password);
            driver.FindElement(By.CssSelector("#content > div > div:nth-child(2) > div > form > input")).Click();

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(1000);
            string repass = password + randomInt;
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys(repass);

            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-confirm")).SendKeys(repass);

            driver.FindElement(By.CssSelector("#content > form > div > div.pull-right > input")).Click();

            driver.FindElement(By.CssSelector("#column-right > div > a:nth-child(13)")).Click();

        }
    }
}
