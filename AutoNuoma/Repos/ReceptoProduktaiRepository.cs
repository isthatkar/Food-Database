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
    public class ReceptoProduktaiRepository
    {
        public List<ReceptoProduktai> GetReceptoProduktai(int receptas)
        {
            List<ReceptoProduktai> produktai = new List<ReceptoProduktai>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + Globals.dbPrefix + "recepto_produktas WHERE fk_Receptasid_=" + receptas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                produktai.Add(new ReceptoProduktai
                {
                    fk_Receptasid_ = Convert.ToInt32(item["fk_Receptasid_"]),
                    fk_ProduktasPavadinimas = Convert.ToString(item["fk_ProduktasPavadinimas"]),
                    id_ = Convert.ToInt32(item["id_"]),
                    Kiekio_matas = Convert.ToInt32(item["Kiekio_matas"]),
                    Kiekis = Convert.ToInt32(item["Kiekis"])
                });
            }

            return produktai;
        }

        public bool deleteReceptoProduktus(int receptas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM a USING recepto_produktas as a
                                where a.fk_Receptasid_ =?receptas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?receptas", MySqlDbType.Int32).Value = receptas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool insertUzsakytaPaslauga(ReceptoProduktai produktas)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `recepto_produktas` (
                                    `fk_Receptasid_`,
                                    `fk_ProduktasPavadinimas`,
                                    `Kiekio_matas`,
                                    `Kiekis`,
                                    `id_`)
                                    VALUES(
                                    ?fk_Receptasid_,
                                    ?fk_ProduktasPavadinimas,
                                    ?Kiekio_matas,
                                    ?Kiekis,
                                    ?id_
                                     )";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fk_Receptasid_", MySqlDbType.Int32).Value = produktas.fk_Receptasid_;
            mySqlCommand.Parameters.Add("?fk_ProduktasPavadinimas", MySqlDbType.VarChar).Value = produktas.fk_ProduktasPavadinimas ;
            mySqlCommand.Parameters.Add("?Kiekio_matas", MySqlDbType.Int32).Value = produktas.Kiekio_matas;
            mySqlCommand.Parameters.Add("?Kiekis", MySqlDbType.Int32).Value = produktas.Kiekis;
            mySqlCommand.Parameters.Add("?id_", MySqlDbType.Int32).Value = produktas.id_;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getRecProdduktuCount()
        {
            int count = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(*)  from " + "recepto_produktas";
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