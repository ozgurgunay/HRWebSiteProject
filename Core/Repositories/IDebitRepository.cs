using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDebitRepository : IRepository<Debit>
    {
        Task<Debit> GetDebitByIDAsync(Guid debitID);
        Task<IEnumerable<Debit>> GetDebitsByUserIDAsync(Guid userID);
    }
}
