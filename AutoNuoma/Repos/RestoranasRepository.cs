using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Repos
{
    public class RestoranasRepository
    {
        public List<Restoranas> GetRestoranai()
        {
            List<Restoranas> restoranai = new List<Restoranas>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM "+"restoranas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                restoranai.Add(new Restoranas
                {
                   
                        Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                        Virtuves_tipas = Convert.ToString(item["Virtuves_tipas"]),
                        Atidarymo_metai = Convert.ToInt32(item["Atidarymo_metai"]),
                        Tel_numeris = Convert.ToString(item["Tel_numeris"]),
                        Vietu_skaicius = Convert.ToInt32(item["Vietu_skaicius"]),
                        Ivertinimas = Convert.ToInt32(item["Ivertinimas"]),
                        Miestas = Convert.ToString(item["Miestas"])

                });
            }

            return restoranai;
        }

        public RestoranasEditViewModel GetRestoranas(string id)
        {
            RestoranasEditViewModel restoranas = new RestoranasEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM Restoranas WHERE Pavadinimas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                restoranas.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
                restoranas.Virtuves_tipas = Convert.ToString(item["Virtuves_tipas"]);
                restoranas.Atidarymo_metai = Convert.ToInt32(item["Atidarymo_metai"]);
                restoranas.Tel_numeris = Convert.ToString(item["Tel_numeris"]);
                restoranas.Vietu_skaicius = Convert.ToInt32(item["Vietu_skaicius"]);
                restoranas.Ivertinimas = Convert.ToInt32(item["Ivertinimas"]);
                restoranas.Miestas = Convert.ToString(item["Miestas"]);
            }

            return restoranas;
        }

        public bool updateRestoranas(RestoranasEditViewModel restoranas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE restoranas a SET a.Virtuves_tipas=?Virtuves_tipas, a.Atidarymo_metai=?Atidarymo_metai, Tel_numeris=?Tel_numeris, Vietu_skaicius=?Vietu_skaicius, Ivertinimas=?Ivertinimas, Miestas=?Miestas  WHERE a.Pavadinimas=?Pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?Pavadinimas", MySqlDbType.VarChar).Value = restoranas.Pavadinimas;
            mySqlCommand.Parameters.Add("?Virtuves_Tipas", MySqlDbType.VarChar).Value = restoranas.Virtuves_tipas;
            mySqlCommand.Parameters.Add("?Atidarymo_metai", MySqlDbType.Int32).Value = restoranas.Atidarymo_metai;
            mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = restoranas.Tel_numeris;
            mySqlCommand.Parameters.Add("?Vietu_skaicius", MySqlDbType.Int32).Value = restoranas.Vietu_skaicius;
            mySqlCommand.Parameters.Add("?Ivertinimas", MySqlDbType.Int32).Value = restoranas.Ivertinimas;
            mySqlCommand.Parameters.Add("?Miestas", MySqlDbType.VarChar).Value = restoranas.Miestas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addRestoranas(RestoranasEditViewModel restoranas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO restoranas(Pavadinimas,Virtuves_Tipas,Atidarymo_metai,Tel_numeris,Vietu_skaicius,Ivertinimas,Miestas)VALUES(?Pavadinimas,?Virtuves_Tipas,?Atidarymo_metai,?Tel_numeris,?Vietu_skaicius,?Ivertinimas,?Miestas)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?Pavadinimas", MySqlDbType.VarChar).Value = restoranas.Pavadinimas;
            mySqlCommand.Parameters.Add("?Virtuves_Tipas", MySqlDbType.VarChar).Value = restoranas.Virtuves_tipas;
            mySqlCommand.Parameters.Add("?Atidarymo_metai", MySqlDbType.Int32).Value = restoranas.Atidarymo_metai;
            mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = restoranas.Tel_numeris;
            mySqlCommand.Parameters.Add("?Vietu_skaicius", MySqlDbType.Int32).Value = restoranas.Vietu_skaicius;
            mySqlCommand.Parameters.Add("?Ivertinimas", MySqlDbType.Int32).Value = restoranas.Ivertinimas;
            mySqlCommand.Parameters.Add("?Miestas", MySqlDbType.VarChar).Value = restoranas.Miestas;

            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

     

        public void deleteRestoranas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM restoranas where Pavadinimas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        public int getDarbuotojuCount(string id)
        {
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*)  from darbuotojas WHERE fk_RestoranasPavadinimas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                count = Convert.ToInt32(item["count(*)"] == DBNull.Value ? 0 : item["count(*)"]);
            }
            return count;
        }
    }
}