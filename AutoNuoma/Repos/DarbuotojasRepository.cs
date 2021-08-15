using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using MySql.Data.MySqlClient;
using AutoNuoma.Models;

namespace AutoNuoma.Repos
{
    public class DarbuotojasRepository
    {
        public List<DarbuotojasViewModel> getDarbuotojai()
        {
            List<DarbuotojasViewModel> darbuotojai = new List<DarbuotojasViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojai.Add(new DarbuotojasViewModel
                {
                    Tabelio_nr = Convert.ToInt32(item["Tabelio_nr"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Isidarbinimo_data = Convert.ToDateTime(item["Isidarbinimo_data"]),
                    Pareigos = Convert.ToString(item["Pareigos"]),
                    Tel_numeris = Convert.ToString(item["Tel_numeris"]),
                    Restroranas = Convert.ToString(item["fk_RestoranasPavadinimas"])

                }); ;
            }

            return darbuotojai;
        }

        public List<DarbuotojasEditViewModel> getRestoranoDarbuotojai(string pav)
        {
            List<DarbuotojasEditViewModel> darbuotojai = new List<DarbuotojasEditViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"select * from darbuotojas WHERE fk_RestoranasPavadinimas=?pav";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = pav;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojai.Add(new DarbuotojasEditViewModel
                {
                    Tabelio_nr = Convert.ToInt32(item["Tabelio_nr"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    Isidarbinimo_data = Convert.ToDateTime(item["Isidarbinimo_data"]),
                    Pareigos = Convert.ToString(item["Pareigos"]),
                    Tel_numeris = Convert.ToString(item["Tel_numeris"]),
                    Restroranas = Convert.ToString(item["fk_RestoranasPavadinimas"])

                }); ;
            }

            return darbuotojai;
        }
        public DarbuotojasEditViewModel getDarbuotojas(int tabnr)
        {
            DarbuotojasEditViewModel darbuotojas = new DarbuotojasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojas where tabelio_nr=?tabnr";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?tabnr", MySqlDbType.VarChar).Value = tabnr;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojas.Tabelio_nr = Convert.ToInt32(item["Tabelio_nr"]);
                darbuotojas.Vardas = Convert.ToString(item["Vardas"]);
                darbuotojas.Pavarde = Convert.ToString(item["Pavarde"]);
                darbuotojas.Isidarbinimo_data = Convert.ToDateTime(item["Isidarbinimo_data"]);
                darbuotojas.Pareigos = Convert.ToString(item["Pareigos"]);
                darbuotojas.Tel_numeris = Convert.ToString(item["Tel_numeris"]);
                darbuotojas.Restroranas = Convert.ToString(item["fk_RestoranasPavadinimas"]);
            }

            return darbuotojas;
        }

        public bool updateDarbuotojas(DarbuotojasEditViewModel darbuotojas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE "+ "darbuotojas a SET a.Vardas=?Vardas, a.Pavarde=?Pavarde, a.Pareigos=?Pareigos, a.Isidarbinimo_data=?Isidarbinimo_data, a.Tel_numeris=?Tel_numeris, a.fk_RestoranasPavadinimas=?Restroranas WHERE a.Tabelio_nr=?Tabelio_nr";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Vardas", MySqlDbType.VarChar).Value = darbuotojas.Vardas;
                mySqlCommand.Parameters.Add("?Pavarde", MySqlDbType.VarChar).Value = darbuotojas.Pavarde;
                mySqlCommand.Parameters.Add("?Tabelio_nr", MySqlDbType.Int32).Value = darbuotojas.Tabelio_nr;
                mySqlCommand.Parameters.Add("?Pareigos", MySqlDbType.VarChar).Value = darbuotojas.Pareigos;
                mySqlCommand.Parameters.Add("?Isidarbinimo_data", MySqlDbType.Date).Value = darbuotojas.Isidarbinimo_data;
                mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = darbuotojas.Tel_numeris;
                mySqlCommand.Parameters.Add("?Restroranas", MySqlDbType.VarChar).Value = darbuotojas.Restroranas;

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

        public bool addDarbuotojas(DarbuotojasEditViewModel darbuotojas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO "+Globals.dbPrefix+ "darbuotojas(Tabelio_nr,Vardas,Pavarde,Pareigos,Isidarbinimo_data,Tel_numeris,fk_RestoranasPavadinimas)VALUES(?Tabelio_nr,?Vardas,?Pavarde,?Pareigos,?Isidarbinimo_data,?Tel_numeris,?Restroranas);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?Vardas", MySqlDbType.VarChar).Value = darbuotojas.Vardas;
                mySqlCommand.Parameters.Add("?Pavarde", MySqlDbType.VarChar).Value = darbuotojas.Pavarde;
                mySqlCommand.Parameters.Add("?Tabelio_nr", MySqlDbType.Int32).Value = darbuotojas.Tabelio_nr;
                mySqlCommand.Parameters.Add("?Pareigos", MySqlDbType.VarChar).Value = darbuotojas.Pareigos;
                mySqlCommand.Parameters.Add("?Isidarbinimo_data", MySqlDbType.Date).Value = darbuotojas.Isidarbinimo_data;
                mySqlCommand.Parameters.Add("?Tel_numeris", MySqlDbType.VarChar).Value = darbuotojas.Tel_numeris;
                mySqlCommand.Parameters.Add("?Restroranas", MySqlDbType.VarChar).Value = darbuotojas.Restroranas;
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

      

        public void deleteDarbuotojas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "darbuotojas where Tabelio_nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public bool deleteDarbuotojai(string pavadinimas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM a USING darbuotojas as a where a.fk_RestoranasPavadinimas=?pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = pavadinimas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }
    }
}