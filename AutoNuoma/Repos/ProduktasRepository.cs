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
    public class ProduktasRepository
    {
        public List<Produktas> getProducts()
        {
            List<Produktas> produktai = new List<Produktas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "produktas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                produktai.Add(new Produktas
                {
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Kaina_eurais = Convert.ToDouble(item["Kaina_eurais"]) ,
                    Baltymai = Convert.ToInt32(item["Baltymai"]),
                    Angliavandeniai = Convert.ToInt32(item["Angliavandeniai"]),
                    Riebalai = Convert.ToInt32(item["Riebalai"]),
                    Kalorijos =Convert.ToInt32(item["Kalorijos"])
                }); 
            }
            return produktai;
        
        }

        public bool addProduktas(Produktas produktas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO " + Globals.dbPrefix + "produktas(Pavadinimas,Kaina_eurais,Baltymai,Angliavandeniai,Riebalai,Kalorijos)VALUES(?Pavadinimas,?Kaina_eurais,?Baltymai,?Angliavandeniai,?Riebalai,?Kalorijos);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Pavadinimas", MySqlDbType.VarChar).Value = produktas.Pavadinimas;
                mySqlCommand.Parameters.Add("?Kaina_Eurais", MySqlDbType.Double).Value = produktas.Kaina_eurais;
                mySqlCommand.Parameters.Add("?Baltymai", MySqlDbType.Int32).Value = produktas.Baltymai;
                mySqlCommand.Parameters.Add("?Riebalai", MySqlDbType.Int32).Value = produktas.Riebalai;
                mySqlCommand.Parameters.Add("?Angliavandeniai", MySqlDbType.Int32).Value = produktas.Angliavandeniai;
                mySqlCommand.Parameters.Add("?Kalorijos", MySqlDbType.Int32).Value = produktas.Kalorijos;
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

        public bool updateProduktas(Produktas produktas)
        {

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE produktas a SET a.Kaina_eurais=?Kaina_eurais, a.Baltymai=?Baltymai, a.Riebalai=?Riebalai, a.Angliavandeniai=?Angliavandeniai, a.Kalorijos=?Kalorijos WHERE a.Pavadinimas=?Pavadinimas";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Pavadinimas", MySqlDbType.VarChar).Value = produktas.Pavadinimas;
                mySqlCommand.Parameters.Add("?Kaina_Eurais", MySqlDbType.Double).Value = produktas.Kaina_eurais;
                mySqlCommand.Parameters.Add("?Baltymai", MySqlDbType.Int32).Value = produktas.Baltymai;
                mySqlCommand.Parameters.Add("?Riebalai", MySqlDbType.Int32).Value = produktas.Riebalai;
                mySqlCommand.Parameters.Add("?Angliavandeniai", MySqlDbType.Int32).Value = produktas.Angliavandeniai;
                mySqlCommand.Parameters.Add("?Kalorijos", MySqlDbType.Int32).Value = produktas.Kalorijos;
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

        public Produktas GetProduktas(string Pavadinimas)
        {
            Produktas produktas = new Produktas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "produktas where Pavadinimas=?Pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?Pavadinimas", MySqlDbType.VarChar).Value = Pavadinimas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                produktas.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
                produktas.Kaina_eurais = Convert.ToDouble(item["Kaina_eurais"]);
                produktas.Baltymai = Convert.ToInt32(item["Baltymai"]);
                produktas.Angliavandeniai = Convert.ToInt32(item["Angliavandeniai"]);
                produktas.Riebalai = Convert.ToInt32(item["Riebalai"]);
                produktas.Kalorijos = Convert.ToInt32(item["Kalorijos"]);
               
            }
            return produktas;
        }

        public int getProduktasReceptuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(id_) as kiekis from " + "recepto_produktas where fk_ProduktasPavadinimas=\"" + id + "\"";
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

        public void deleteProduktas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "produktas where Pavadinimas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

    }
}