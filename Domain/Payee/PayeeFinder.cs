using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Payee
{
    public static class PayeeFinder
    {
        public static List<Payee> Find(string input, List<Payee> payees)
        {
            var payeesWithOverlap = new List<(Payee payee, int rating)>();

            foreach (var payee in payees)
            {
                foreach (var keyword in payee.Keywords)
                {
                    var overlap = CalculateOverlap(input, keyword.Value);
                    payeesWithOverlap.Add((payee, overlap));
                }
            }

            return payeesWithOverlap.OrderByDescending(x => x.rating).Select(x => x.payee).ToList();
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

        public static int CalculateOverlap(string x, string y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
            {
                return 0;
            }

            if (string.Equals(x, y, StringComparison.CurrentCultureIgnoreCase))
            {
                return x.Length;
            }

            string s, l;
            if (x.Length == y.Length || x.Length < y.Length)
            {
                s = x;
                l = y;
            }
            else
            {
                s = y;
                l = x;
            }

            if (l.Contains(s, StringComparison.CurrentCultureIgnoreCase))
            {
                return s.Length;
            }

            for (int i = s.Length - 1; i > 0; i--)
            {
                for (int j = 0; j <= s.Length - i; j++)
                {
                    var c = s.Substring(j, i);
                    if (l.Contains(c, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return i;
                    }
                }
            }

            return 0;
        }
    }
}
