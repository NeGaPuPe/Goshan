using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using GoshanMarket.Classes;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public  void TestMethodAuthtorization()
        {
            var application = FlaUI.Core.Application.Launch("C:\\Users\\Мария\\Downloads\\GoshanMarket\\GoshanMarket\\GoshanMarket\\bin\\Debug\\GoshanMarket.exe");
            
            Thread.Sleep(9000);
            Mouse.Position = new Point(950, 200);
            Mouse.Click();
            Thread.Sleep(1000);
            var mainwindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EmailAddressTextBox")).AsTextBox().Enter("rich416@mail.ru");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("PasswordTextBox")).AsTextBox().Enter("321");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EnterButton")).AsButton().Click();
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationTextBox")).AsTextBox().Enter("543572");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationButton")).AsButton().Click();
        }
        [TestMethod]
        public void TestMethodAddInBasket()
        {
            var application = FlaUI.Core.Application.Launch("C:\\Users\\Мария\\Downloads\\GoshanMarket\\GoshanMarket\\GoshanMarket\\bin\\Debug\\GoshanMarket.exe");

            Thread.Sleep(9000);
            Mouse.Position = new Point(950, 200);
            Mouse.Click();
            Thread.Sleep(1000);
            var mainwindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EmailAddressTextBox")).AsTextBox().Enter("rich416@mail.ru");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("PasswordTextBox")).AsTextBox().Enter("321");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EnterButton")).AsButton().Click();
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationTextBox")).AsTextBox().Enter("432991");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationButton")).AsButton().Click();
            Thread.Sleep(500);
            Mouse.MoveBy(10,0);
            Mouse.Click();
            Thread.Sleep(1500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("InBasketButton")).AsButton().Click();
        }

        [TestMethod]
        public void TestMethodAddInFavourites()
        {
            var application = FlaUI.Core.Application.Launch("C:\\Users\\Мария\\Downloads\\GoshanMarket\\GoshanMarket\\GoshanMarket\\bin\\Debug\\GoshanMarket.exe");

            Thread.Sleep(9000);
            Mouse.Position = new Point(950, 200);
            Mouse.Click();
            Thread.Sleep(1000);
            var mainwindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EmailAddressTextBox")).AsTextBox().Enter("rich416@mail.ru");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("PasswordTextBox")).AsTextBox().Enter("321");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EnterButton")).AsButton().Click();
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationTextBox")).AsTextBox().Enter("543572");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationButton")).AsButton().Click();
            Thread.Sleep(500);
            Mouse.MoveBy(10, 0);
            Mouse.Click();
            Thread.Sleep(1500);
            Mouse.Position = new Point(825, 320);
            Thread.Sleep(500);
            Mouse.Click();
        }

        [TestMethod]
        public void TestMethodExitAccount()
        {
            var application = FlaUI.Core.Application.Launch("C:\\Users\\Мария\\Downloads\\GoshanMarket\\GoshanMarket\\GoshanMarket\\bin\\Debug\\GoshanMarket.exe");

            Thread.Sleep(9000);
            Mouse.Position = new Point(950, 200);
            Mouse.Click();
            Thread.Sleep(1000);
            var mainwindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EmailAddressTextBox")).AsTextBox().Enter("rich416@mail.ru");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("PasswordTextBox")).AsTextBox().Enter("321");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EnterButton")).AsButton().Click();
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationTextBox")).AsTextBox().Enter("432991");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationButton")).AsButton().Click();
            Thread.Sleep(1500);
            Mouse.Position = new Point(950, 200);
            Mouse.RightClick();
            Thread.Sleep(500);
            Mouse.Position = new Point(820, 295);
            Thread.Sleep(1000);
            Mouse.Click();
        }

        [TestMethod]
        public void TestMethodRegistration()
        {
            var application = FlaUI.Core.Application.Launch("C:\\Users\\Мария\\Downloads\\GoshanMarket\\GoshanMarket\\GoshanMarket\\bin\\Debug\\GoshanMarket.exe");

            Thread.Sleep(7000);
            Mouse.Position = new Point(950, 200);
            Mouse.Click();
            Thread.Sleep(1000);
            var mainwindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

            Mouse.Position = new Point(780, 645);
            Mouse.Click();
            Thread.Sleep(1000);

            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("NameTextBox")).AsTextBox().Enter("Антон");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("EmailAddressTextBox")).AsTextBox().Enter("5511@mail.ru");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("PasswordTextBox")).AsTextBox().Enter("123");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VereficationButton")).AsButton().Click();
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("VerificationTextBox")).AsTextBox().Enter("378295");
            Thread.Sleep(500);
            mainwindow.FindFirstDescendant(conditionFactory.ByAutomationId("RegButton")).AsButton().Click();
            Thread.Sleep(500);
        }
    }
}
