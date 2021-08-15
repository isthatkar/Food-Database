using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AutoNuoma.Repos
{
	public class ReceptoSudetingumasRepository
	{
        public List<ReceptoSudetingumas> getReceptoSudetingumas()
        {
            List<ReceptoSudetingumas> sudetingumai = new List<ReceptoSudetingumas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from " + "gaminimo_sudetingumas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                sudetingumai.Add(new ReceptoSudetingumas
                {
                    id_ = Convert.ToInt32(item["id_"]),
                    name = Convert.ToString(item["name"])
                });
            }
            return sudetingumai;
        }
    }
}