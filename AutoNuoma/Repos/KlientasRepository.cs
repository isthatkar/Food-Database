using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class KlientasRepository
    {
        public List<Klientas> getKlientai()
        {
            List<Klientas> klientai = new List<Klientas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+"klientas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientai.Add(new Klientas
                {
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Tel_numeris = Convert.ToString(item["Tel_numeris"]),
                    El_pastas = Convert.ToString(item["El_pastas"])
                });
            }
            return klientai;
        }

        public bool addKlientas(Klientas klientas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"klientas(Vardas,Pavarde,Tel_numeris,El_pastas)VALUES(?Vardas,?Pavarde,?Tel_numeris,?El_pastas);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Vardas", MySqlDbType.VarChar).Value = klientas.Vardas;
                mySqlCommand.Parameters.Add("?Pavarde", MySqlDbType.VarChar).Value = klientas.Pavarde;
                mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = klientas.Tel_numeris;
                mySqlCommand.Parameters.Add("?El_pastas", MySqlDbType.VarChar).Value = klientas.El_pastas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateKlientas(Klientas klientas)
        {

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE " + Globals.dbPrefix + "klientas a SET a.Vardas=?Vardas, a.Pavarde=?Pavarde, a.Tel_numeris=?Tel_numeris, a.El_pastas=?El_pastas WHERE a.Tel_numeris=?Tel_numeris";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Vardas", MySqlDbType.VarChar).Value = klientas.Vardas;
                mySqlCommand.Parameters.Add("?Pavarde", MySqlDbType.VarChar).Value = klientas.Pavarde;
                mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = klientas.Tel_numeris;
                mySqlCommand.Parameters.Add("?El_pastas", MySqlDbType.VarChar).Value = klientas.El_pastas;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Klientas getKlientas(string Tel_numeris)
        {
            Klientas klientas = new Klientas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "klientas where Tel_numeris=?Tel_numeris";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = Tel_numeris;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientas.Vardas = Convert.ToString(item["Vardas"]);
                klientas.Pavarde = Convert.ToString(item["Pavarde"]);
                klientas.Tel_numeris = Convert.ToString(item["Tel_numeris"]);
                klientas.El_pastas = Convert.ToString(item["El_pastas"]);
            }
            return klientas;
        }

        public int getKlientasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(Uzsakymo_numeris) as kiekis from " + "uzsakymas where fk_KlientasTel_numeris=\"" + id + "\"";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public void deleteKlientas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SET FOREIGN_KEY_CHECKS=0;DELETE FROM klientas where Tel_numeris=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}