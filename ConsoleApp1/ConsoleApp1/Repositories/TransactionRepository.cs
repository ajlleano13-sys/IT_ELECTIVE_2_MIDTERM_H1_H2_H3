using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories
{
    public static class TransactionRepository
    {
        private static readonly List<Transaction> _transactions = new List<Transaction>();

        public static IEnumerable<Transaction> GetAll()
        {
            return _transactions.OrderByDescending(t => t.Date);
        }

        public static Transaction? GetById(Guid id)
        {
            return _transactions.FirstOrDefault(t => t.TransactionId == id);
        }

        public static void Add(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}