using Domain.Payee;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IPayeeRepository
    {
        void Add(Payee payee);
        List<Payee> GetAll();
        void Remove(int payeeId);
        void AddKeyword(string keyword, int payeeId);
        void RemoveKeyword(string keyword, int payeeId);
    }
}
