using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Domain.Payee;

namespace DataAccess
{
    public class PayeeRepository : IPayeeRepository
    {
        private readonly IDbConnection connection;

        public PayeeRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Payee payee)
        {
            var sql = "" +
                "INSERT INTO Payee (Name) VALUES (@Name); " +
                "SELECT CAST(LAST_INSERT_ROWID() AS int);";
            var id = connection.Query<int>(sql, payee).Single();

            connection.Execute("INSERT INTO Keyword(Value, PayeeId) VALUES (@value, @id)", new { value = payee.Name, id });

            payee.Id = id;
        }

        public List<Payee> GetAll()
        {
            var sql =
                "SELECT P.*, K.* FROM Payee P " +
                "INNER JOIN Keyword K ON P.Id = K.PayeeId";

            var lookup = new Dictionary<int, Payee>();
            connection.Query<Payee, Keyword, Payee>(sql, (p, k) =>
            {
                if (!lookup.TryGetValue(p.Id, out var payee))
                {
                    payee = p;
                    lookup.Add(p.Id, payee);
                }

                payee.Keywords.Add(k);

                return payee;
            });

            return lookup.Values.ToList();
        }

        public void Remove(int payeeId)
        {
            connection.Execute("DELETE FROM Payee WHERE Id = @payeeId", new { payeeId });
        }

        public void AddKeyword(string keyword, int payeeId)
        {
            connection.Execute("INSERT INTO Keyword (Value, PayeeId) VALUES (@keyword, @payeeId)", new { keyword, payeeId });
        }

        public void RemoveKeyword(string keyword, int payeeId)
        {
            connection.Execute("DELETE FROM Keyword WHERE Value = @keyword AND PayeeId = @payeeId", new { keyword, payeeId });
        }
    }
}
