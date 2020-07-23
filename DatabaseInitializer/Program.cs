using Dapper;
using System;
using System.Data.SQLite;
using System.IO;

namespace DatabaseInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\MyYnabConverter\\payees.sqlite");
            var connectionString = $"Data Source={path};Version=3;";

            // Ensure database file is created
            if (!File.Exists(path)) SQLiteConnection.CreateFile(path);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Execute("DROP TABLE IF EXISTS Payee");
                connection.Execute("DROP TABLE IF EXISTS Keyword");
                connection.Execute("" +
                    "CREATE TABLE Payee ( " +
                    "   Id              INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "   Name            TEXT NOT NULL " +
                    ");" +
                    "" +
                    "CREATE TABLE Keyword ( " +
                    "   Id              INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "   Value           TEXT NOT NULL, " +
                    "   PayeeId         INTEGER NOT NULL, " +
                    "   FOREIGN KEY(PayeeId) REFERENCES Payee(Id) ON DELETE CASCADE " +
                    ");");
            }
        }
    }
}
