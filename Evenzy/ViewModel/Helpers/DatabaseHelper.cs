using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evenzy.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "Evenzy.db");

        public static bool Insert<T>(T item) //funkcija koja se moze korisiti za bilo koji inseert u tabelu
        {
            bool result = false;
                
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Insert(item);
                if (rows > 0)
                    result = true;
            }

            return result;
        }

        public static bool Update<T>(T item) //funkcija koja se moze korisiti za bilo koji update u tabelu
        {
            bool result = false;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Update(item);
                if (rows > 0)
                    result = true;
            }

            return result;
        }

        public static bool Delete<T>(T item) //funkcija koja se moze korisiti za bilo koji inseert u tabelu
        {
            bool result = false;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Delete(item);
                if (rows > 0)
                    result = true;
            }

            return result;
        }

        public static List<T> Read<T>() where T : new() //da bismo mogli da koristimo genericku tabelu, moramo da koristimo where new()
        {
            List<T> items;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                items = conn.Table<T>().ToList();
            }

            return items;
        }
    }
}
