using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace WPF.Models
{
    public class Prestation(int id, string titre, int duree, string description, double tarif)
    {

        public int Id { get; set; } = id;
        public string Titre { get; set; } = titre;
        public int Duree { get; set; } = duree;
        public string Description { get; set; } = description;
        public double Tarif { get; set; } = tarif;

    }
}
       