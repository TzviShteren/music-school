using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchool2207FA.Service
{
    internal class PrsacticeService
    {
        public static Func<List<string>, bool> IsStartA = (x) => x.Any(s => s.StartsWith("a"));
        public static Func<List<string>, bool> IsOneEmpty = (x) => x.Any(s => string.IsNullOrEmpty(s));
        public static Func<List<string>, bool> AllAvA = (x) => x.All(s => s.Contains("a"));
        public static Func<List<string>, List<string>> ToUpperCase = (x) => x.Select(s => s.ToUpper()).ToList();
        public static Func<List<string>, List<string>> ToUpperCaseLinqQuery = (list) =>
        (
            from str in list
            select str.ToUpper()
        ).ToList();
        public static Func<List<string>, List<string>> LargTen3 = (x) => x.Where(s => s.Length > 3).ToList();
        public static Func<List<string>, List<string>> LargTen3LinqQuery = (list) =>
        (
            from str in list
            where str.Length > 3
            select str
        ).ToList();

        public static Func<List<string>, string> ToOneString = (x) => x.Aggregate(string.Empty, (acc, s) => $"{acc} {s}");

        public static Func<List<string>, int> ToOneInt = (x) => x.Aggregate(0, (acc, s) => acc + s.Length);

        public static Func<List<string>, List<string>> LargTen3Aggregate = (x) => x
        .Aggregate(new List<string>(), (acc, s) => s.Length > 3 ? [.. acc, s] : acc);


        //public static Func<List<string>, List<int>> StringToInt = (x) => x
        //.Aggregate(new List<string>(), (acc, s) => [.. acc, s.Length]);
    }
}
