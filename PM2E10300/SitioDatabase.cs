using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E10300
{
    public class SitioDatabase
    {
        readonly SQLiteConnection database;

        public SitioDatabase(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Sitio>();
        }

        public List<Sitio> GetSitios()
        {
            return database.Table<Sitio>().ToList();
        }

        public void InsertSitio(Sitio sitio)
        {
            database.Insert(sitio);
        }

        public void DeleteSitio(Sitio sitio)
        {
            database.Delete(sitio);
        }
    }
}
