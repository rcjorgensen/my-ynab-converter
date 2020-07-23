using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Payee
{
    public static class PayeeFinder
    {
        public static List<PayeeSearchResult> Find(string input, List<Payee> payees)
        {
            var payeeSearchResults = new List<PayeeSearchResult>();

            foreach (var payee in payees)
            {
                foreach (var keyword in payee.Keywords)
                {
                    var overlap = GetOverlap(input, keyword.Value);
                    payeeSearchResults.Add(new PayeeSearchResult
                    {
                        SearchTerm = input,
                        Payee = payee,
                        Overlap = overlap
                    });
                }
            }

            return payeeSearchResults.OrderByDescending(x => x.Overlap.Length).ToList();
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

        public static string GetOverlap(string x, string y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            {
                return "";
            }

            if (string.Equals(x, y, StringComparison.CurrentCultureIgnoreCase))
            {
                return x;
            }

            string shortest, longest;
            if (x.Length == y.Length || x.Length < y.Length)
            {
                shortest = x;
                longest = y;
            }
            else
            {
                shortest = y;
                longest = x;
            }

            if (longest.Contains(shortest, StringComparison.CurrentCultureIgnoreCase))
            {
                return shortest;
            }

            for (int i = shortest.Length - 1; i > 0; i--)
            {
                for (int j = 0; j <= shortest.Length - i; j++)
                {
                    var potentialMatch = shortest.Substring(j, i);
                    if (longest.Contains(potentialMatch, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return potentialMatch;
                    }
                }
            }

            return "";
        }
    }
}
