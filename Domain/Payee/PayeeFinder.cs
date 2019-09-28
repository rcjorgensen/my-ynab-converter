using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Payee
{
    public static class PayeeFinder
    {
        public static List<Payee> Find(string input, List<Payee> payees)
        {
            var payeesWithOrderedKeywords = payees.Select(p => OrderKeywords(p));
            var foundPayeesWithRatings = new List<(Payee payee, int rating)>();

            foreach (var payee in payeesWithOrderedKeywords)
            {
                foreach (var keyword in payee.Keywords)
                {
                    if (InputContainsKeyword(input, keyword.Value))
                    {
                        foundPayeesWithRatings.Add((payee, keyword.Value.Length));
                        break;
                    }
                }
            }

            return foundPayeesWithRatings.OrderByDescending(x => x.rating).Select(x => x.payee).ToList();
        }

        internal static Payee OrderKeywords(Payee payee)
        {
            payee.Keywords = payee.Keywords.OrderByDescending(k => k.Value.Count()).ToList();
            return payee;
        }

        internal static bool InputContainsKeyword(string input, string keyword)
        {
            return input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
