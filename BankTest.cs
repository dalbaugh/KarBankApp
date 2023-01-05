using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KarBankApp;

namespace KarBankApp{
    [TestClass]
    public class BankTest
    {
        Bank.Checking checking = new Bank.Checking("Checking", 10000.00m);
        Bank.Investment<Bank.Individual> investmentIndividual = new Bank.Investment<Bank.Individual>("Investment", 5000.00m);
        Bank.Investment<Bank.Corporate> investmentCorporate = new Bank.Investment<Bank.Corporate>("Corporate", 100000.00m);

        /*      Checking TEST       */
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Withdraw_CheckingAccountShouldPass(decimal amount)
        {    
            var expectBalance = checking.Balance - amount;
            
            checking.Withdraw(amount);
            
            Assert.AreEqual(checking.Balance, expectBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Withdraw_CheckingAccountShouldFail(decimal amount)
        {    
            Action withdraw = delegate() { checking.Withdraw(amount); };
            
            Assert.ThrowsException<ArgumentException>(withdraw);
        }

        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Deposit_CheckingAccountShouldPass(decimal amount)
        {    
            var expectBalance = checking.Balance + amount;
            
            checking.Deposit(amount);
            
            Assert.AreEqual(checking.Balance, expectBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Transfer_CheckingAccountShouldPass(decimal amount)
        {            
            var expectFromBalance = checking.Balance - amount;
            var expectToBalance = investmentIndividual.Balance + amount;
            checking.Transfer(investmentIndividual, amount);
            
            Assert.AreEqual(checking.Balance, expectFromBalance);
            Assert.AreEqual(investmentIndividual.Balance, expectToBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Transfer_CheckingAccountShouldFail(decimal amount)
        {    
            var expectFromBalance = checking.Balance - amount;
            var expectToBalance = investmentIndividual.Balance + amount;
            Action transfer = delegate() { checking.Transfer(investmentIndividual, amount); };
            
            Assert.ThrowsException<ArgumentException>(transfer);
        }

        /*      Investment Individual Tests    */
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Withdraw_InvestmentIndividualAccountShouldPass(decimal amount)
        {                
            var expectBalance = investmentIndividual.Balance - amount;
            
            investmentIndividual.Withdraw(amount);
            
            Assert.AreEqual(investmentIndividual.Balance, expectBalance);
        }

        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Withdraw_InvestmentIndividualAccountShouldFail(decimal amount)
        {    
            Action withdraw = delegate() { investmentIndividual.Withdraw(amount); };
            
            Assert.ThrowsException<ArgumentException>(withdraw);
        }
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Deposit_InvestmentIndividualShouldPass(decimal amount)
        {    
            var expectBalance = investmentIndividual.Balance + amount;
            
            investmentIndividual.Deposit(amount);
            
            Assert.AreEqual(investmentIndividual.Balance, expectBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Transfer_InvestmentIndividualShouldPass(decimal amount)
        {            
            var expectFromBalance = investmentIndividual.Balance - amount;
            var expectToBalance = checking.Balance + amount;
            investmentIndividual.Transfer(checking, amount);
            
            Assert.AreEqual(investmentIndividual.Balance, expectFromBalance);
            Assert.AreEqual(checking.Balance, expectToBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Transfer_InvestmentIndividualShouldFail(decimal amount)
        {    
            var expectFromBalance = investmentIndividual.Balance - amount;
            var expectToBalance = checking.Balance + amount;
            Action transfer = delegate() { investmentIndividual.Transfer(checking, amount); };
            
            Assert.ThrowsException<ArgumentException>(transfer);
        }

        /*      Investment Corporate Tests    */
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Withdraw_InvestmentCorporateAccountShouldPass(decimal amount)
        {                
            var expectBalance = investmentCorporate.Balance - amount;
            
            investmentCorporate.Withdraw(amount);
            
            Assert.AreEqual(investmentCorporate.Balance, expectBalance);
        }

        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Withdraw_InvestmentCorporateAccountShouldFail(decimal amount)
        {    
            Action withdraw = delegate() { investmentCorporate.Withdraw(amount); };
            
            Assert.ThrowsException<ArgumentException>(withdraw);
        }
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Deposit_InvestmentCorporateShouldPass(decimal amount)
        {    
            var expectBalance = investmentCorporate.Balance + amount;
            
            investmentCorporate.Deposit(amount);
            
            Assert.AreEqual(investmentCorporate.Balance, expectBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(PassData), DynamicDataSourceType.Method)]
        public void Transfer_InvestmentCorporateShouldPass(decimal amount)
        {            
            var expectFromBalance = investmentCorporate.Balance - amount;
            var expectToBalance = checking.Balance + amount;
            investmentCorporate.Transfer(checking, amount);
            
            Assert.AreEqual(investmentCorporate.Balance, expectFromBalance);
            Assert.AreEqual(checking.Balance, expectToBalance);
        }
        [DataTestMethod]
        [DynamicData(nameof(FailData), DynamicDataSourceType.Method)]
        public void Transfer_InvestmentCorporateShouldFail(decimal amount)
        {    
            var expectFromBalance = investmentCorporate.Balance - amount;
            var expectToBalance = checking.Balance + amount;
            Action transfer = delegate() { investmentCorporate.Transfer(checking, amount); };
            
            Assert.ThrowsException<ArgumentException>(transfer);
        }



        //Pass in Dynamic Data decimal
        private static IEnumerable<object[]> PassData()
        {
            return new[]
            {
                new object[] { 500.00m },
                new object[] { 123.45m }
            };
        }
        private static IEnumerable<object[]> FailData()
        {
            return new[]
            {
                new object[] { 100000000.00m },
                new object[] { 1234567.89m }
            };
        }
    
    }
}