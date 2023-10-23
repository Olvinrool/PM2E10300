using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E10300
{
    public class Sitio
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string ImagenPath { get; set; }
    }
}
