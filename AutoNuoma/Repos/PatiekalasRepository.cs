using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class PatiekalasRepository
    {
        public List<Patiekalas> getPatiekalai()
        {
            List<Patiekalas> patiekalai = new List<Patiekalas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * from "+"patiekalas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                patiekalai.Add(new Patiekalas
                {
                    Pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    Kaina_eurais = Convert.ToDouble(item["Kaina_eurais"]),
                    Kategorija = Convert.ToString(item["Kategorija"]),
                    fk_RestoranasPavadinimas = Convert.ToString(item["fk_RestoranasPavadinimas"])

                });
            }

            return patiekalai;
        }

        public Patiekalas GetPatiekalas(string id)
        {
            Patiekalas patiekalas = new Patiekalas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.Pavadinimas,
                                       a.Kaina_eurais,
                                       a.Kategorija,
                                       a.fk_RestoranasPavadinimas
                                       FROM " +  @"patiekalas a
                                       WHERE a.Pavadinimas= " + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                patiekalas.Pavadinimas = Convert.ToString(item["Pavadinimas"]);
                patiekalas.Kaina_eurais = Convert.ToDouble(item["Kaina_eurais"]);
                patiekalas.Kategorija = Convert.ToString(item["Kategorija"]);
                patiekalas.fk_RestoranasPavadinimas = Convert.ToString(item["fk_RestoranasPavadinimas"]);
            }

            return patiekalas;
        }

        //public bool updatePaslauga(Patiekalas paslauga)
        //{
        //    string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        //    MySqlConnection mySqlConnection = new MySqlConnection(conn);
        //    string sqlquery = @"UPDATE "+Globals.dbPrefix+"paslaugos a SET a.pavadinimas=?pavadinimas, a.aprasymas=?aprasymas WHERE a.id=?id";
        //    MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
        //    mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = paslauga.id;
        //    mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = paslauga.pavadinimas;
        //    mySqlCommand.Parameters.Add("?aprasymas", MySqlDbType.VarChar).Value = paslauga.aprasymas;
        //    mySqlConnection.Open();
        //    mySqlCommand.ExecuteNonQuery();
        //    mySqlConnection.Close();
        //    return true;
        //}

        public int insertPatiekalas(Patiekalas paslauga)
        {
            int insertedId = -1;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            //string sqlquery = @"INSERT INTO " + Globals.dbPrefix + "paslaugos(pavadinimas,aprasymas)VALUES(?pavadinimas,?aprasymas);";
            //MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            //mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = paslauga.pavadinimas;
            //mySqlCommand.Parameters.Add("?aprasymas", MySqlDbType.VarChar).Value = paslauga.aprasymas;
            //mySqlConnection.Open();
            //mySqlCommand.ExecuteNonQuery();
            //mySqlConnection.Close();
            //insertedId = Convert.ToInt32(mySqlCommand.LastInsertedId);
            return insertedId;
        }

        //public void deletePaslauga(int id)
        //{
        //    string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        //    MySqlConnection mySqlConnection = new MySqlConnection(conn);
        //    string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"paslaugos where id=?id";
        //    MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
        //    mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
        //    mySqlConnection.Open();
        //    mySqlCommand.ExecuteNonQuery();
        //    mySqlConnection.Close();
        //}
    }
}