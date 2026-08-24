using System.Collections.Generic;
using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAll();
        Transaction? GetById(Guid id);
        void Add(Transaction transaction);
    }
}