using System;
using System.Collections.Generic;

namespace Wallet
{
    public class Account
    {
        public string Name { get; }
        public decimal Balance { get; private set; }
        private readonly List<Transaction> _transactions = new();
        public IReadOnlyList<Transaction> Transactions => _transactions;

        public Account(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя пусто!");
            }

            Name = name;
        }

        public void Deposit(decimal amount, string category, DateTime? date = null)
        {
            Transaction transaction = new(amount, category, date ?? DateTime.Now, TransactionType.Income);
            _transactions.Add(transaction);
            Balance += amount;
        }

        public void Withdraw(decimal amount, string category, DateTime? date = null)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException($"Недостаточно средств: на счёте {Balance}, запрошено {amount}");
            }

            Transaction transaction = new(amount, category, date ?? DateTime.Now, TransactionType.Expense);
            _transactions.Add(transaction);
            Balance -= amount;
        }

        public decimal GetTotalExpenses()
        {
            return _transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);
        }

        public decimal GetTotalIncome()
        {
            return _transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            Dictionary<string, decimal> transactionsByCategory = _transactions
                .Where(t => t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
            return transactionsByCategory;
        }

        public Transaction? GetLargestExpense()
        {
            Transaction? largestTransaction = _transactions
                .Where(t => t.Type == TransactionType.Expense)
                .OrderByDescending(t => t.Amount)
                .FirstOrDefault();
            return largestTransaction;
        }

        public IEnumerable<Transaction> GetTransactionsForPeriod(DateTime from, DateTime to)
        {
            IEnumerable<Transaction> transactionsForPeriod = _transactions
                .Where(t => t.Date >= from && t.Date < to.Date.AddDays(1))
                .OrderByDescending(t => t.Date);
            return transactionsForPeriod;
        }
    }
}
