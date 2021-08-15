using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace AutoNuoma.Repos
{
    public class ReceptasRepository
    {
       
            public List<ReceptasListViewModel> GetReceptai()
            {
                List<ReceptasListViewModel> receptai = new List<ReceptasListViewModel>();
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT s.id_, s.Laikas_minutemis, s.fk_PatiekalasPavadinimas, b.name as Sudetingumas FROM receptas s, gaminimo_sudetingumas b WHERE s.Sudetingumas=b.id_ ORDER BY 1";

            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mySqlConnection.Close();

                foreach (DataRow item in dt.Rows)
                {
                receptai.Add(new ReceptasListViewModel
                {
                    PatiekalasPavadinimas = Convert.ToString(item["fk_PatiekalasPavadinimas"]),
                    Laikas_minutemis = Convert.ToInt32(item["Laikas_minutemis"]),
                    Sudetingumas = Convert.ToString(item["Sudetingumas"]),
                    id_ = Convert.ToInt32(item["id_"])
                }) ;
                }
                return receptai;
            }

        public ReceptasEditViewModel getReceptas(string pavadinimas)
        {
            ReceptasEditViewModel receptas = new ReceptasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT * FROM " +  "receptas where fk_PatiekalasPavadinimas=?pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value =pavadinimas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                receptas.fk_PatiekalasPavadinimas = Convert.ToString(item["fk_PatiekalasPavadinimas"]);
                receptas.Sudetingumas = Convert.ToInt32(item["Sudetingumas"]);
                receptas.Laikas_minutemis = Convert.ToInt32(item["Laikas_minutemis"]);
                receptas.id_ = Convert.ToInt32(item["id_"]);

            }


            return receptas;
        }

        public bool updateReceptas(ReceptasEditViewModel receptas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `receptas` SET
                                `Sudetingumas` = ?Sudetingumas,
                                `Laikas_minutemis` = ?Laikas_minutemis
                                WHERE fk_PatiekalasPavadinimas=?fk_PatiekalasPavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);

            mySqlCommand.Parameters.Add("?fk_PatiekalasPavadinimas", MySqlDbType.VarChar).Value = receptas.fk_PatiekalasPavadinimas;
            mySqlCommand.Parameters.Add("?Sudetingumas", MySqlDbType.Int32).Value = receptas.Sudetingumas;
            mySqlCommand.Parameters.Add("?Laikas_minutemis", MySqlDbType.Int32).Value = receptas.Laikas_minutemis;
        
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addReceptas(ReceptasEditViewModel receptas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `" + Globals.dbPrefix + @"receptas` (
                                    `Laikas_minutemis`,
                                    `Sudetingumas`,
                                    `fk_PatiekalasPavadinimas`,
                                    `id_`)
                                    VALUES(
                                    ?Laikas_minutemis,
                                    ?Sudetingumas,
                                    ?fk_PatiekalasPavadinimas,
                                    ?id_
                                     )";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?Laikas_minutemis", MySqlDbType.Int32).Value = receptas.Laikas_minutemis;
            mySqlCommand.Parameters.Add("?Sudetingumas", MySqlDbType.String).Value = receptas.Sudetingumas;
            mySqlCommand.Parameters.Add("?fk_PatiekalasPavadinimas", MySqlDbType.String).Value = receptas.fk_PatiekalasPavadinimas;
            mySqlCommand.Parameters.Add("?id_", MySqlDbType.Int32).Value = getReceptuCount()+100;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteReceptas(string pavadinimas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM receptas where fk_PatiekalasPavadinimas=?pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = pavadinimas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public int getReceptuCount()
        {
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*)  from " + "receptas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
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

        public int getReceptoProdCount(int id)
        {
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*)  from recepto_produktas WHERE fk_Receptasid_="+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
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