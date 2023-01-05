using System;
using System.Reflection;

namespace KarBankApp{
    public class Bank {
        public interface IAccounts{
            string Owner{ get; set; }
            decimal Balance { get; set; }  
        }
        public interface ITransactions {
            public decimal Withdraw(decimal amount);
            public decimal Deposit(decimal amount);
            public bool Transfer(IAccounts accTo, decimal amount);
        }
        public class Checking : ITransactions, IAccounts
        {
            public Checking(string owner, decimal balance){
                this.Owner = owner;
                this.Balance = balance;
            }
            public string Owner{ get; set; } = default!;
            public decimal Balance { get; set; } 
            public decimal Deposit(decimal amount)
            {
                return this.Balance += amount;
            }

            public bool Transfer(IAccounts accTo, decimal amount)
            {
                if(amount < this.Balance){
                    this.Balance -= amount;
                    accTo.Balance += amount;
                    Console.WriteLine(this.Owner + " Transferred $" + amount + " to " + accTo.Owner);
                    return true;
                } else {
                    Console.WriteLine("Insufficient Funds to Transfer");
                    throw new ArgumentException("Amount greater than Balance. Cannot have negative balance"); 
                    //return false;
                }
            }

            public decimal Withdraw(decimal amount)
            {
                if(amount <= this.Balance) return this.Balance -= amount;
                else {
                    Console.WriteLine("Insufficient Funds to Withdraw");
                    throw new ArgumentException("Amount greater than Balance. Cannot have negative balance");  
                    //return this.Balance;
                } 
            }
        }

        public class Investment<T> : IAccounts, ITransactions where T : struct
        {   
            T investmentAccount;
            public Investment(string _owner, decimal _balance){           
                this.Owner = _owner;
                this.Balance = _balance;
                this.investmentAccount = new T();
            }
            public string Owner{ get; set; } = default!;
            public decimal Balance { get; set; }

            public decimal Deposit(decimal amount)
            {
                return this.Balance += amount;
            }

            public bool Transfer(IAccounts accTo, decimal amount)
            {
                if(amount < this.Balance){
                    this.Balance -= amount;
                    accTo.Balance += amount;
                    Console.WriteLine(this.Owner + " Transferred $" + amount + " to " + accTo.Owner);
                    return true;
                } else {
                    Console.WriteLine("Insufficient Funds to Transfer");
                    throw new ArgumentException("Amount greater than Balance. Cannot have negative balance"); 
                    //return false;
                }
            }

            public decimal Withdraw(decimal amount)
            {
                if(amount <= this.Balance){
                    if(investmentAccount.GetType() == typeof(Individual) && amount > 500){
                        Console.WriteLine("You cannot withdraw more than $500.00 with an {0} Investment Account!", investmentAccount.GetType().Name);
                        throw new ArgumentException("Amount greater withdrawal limit of $500.00"); 
                        //return this.Balance;
                    } else return this.Balance -= amount;
                } else {
                    Console.WriteLine("Insufficient Funds");
                    throw new ArgumentException("Amount greater than Balance. Cannot have negative balance"); 
                    //return this.Balance;
                } 
            }
        }
        public struct Corporate {
        }
        public struct Individual {
        }
        
            
        public static void Main(string[] args){
        }
    }
}
