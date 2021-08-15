using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using AutoNuoma.ViewModels;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class AtaskaituRepository
    {

        public List<AtaskaitaViewModel> getAtaskaitaUzsakymu(DateTime? nuo, DateTime? iki)
        {
            List<AtaskaitaViewModel> sutartys = new List<AtaskaitaViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @" SELECT Uzsakymo_numeris AS nr, restoranas.Pavadinimas AS pavadinimas,
          uzsakymas.Data AS data,
          kiekisPatiekalu AS patiekalu_Kiekis,
          uzsakymas.Kaina AS kaina,
          b.bendra_suma,
          klientas.Vardas,
          klientas.Pavarde,
          patRusys AS patRusys
          FROM
             (restoranas
            INNER JOIN uzsakymas ON uzsakymas.fk_RestoranasPavadinimas = restoranas.Pavadinimas
            LEFT JOIN klientas ON uzsakymas.fk_KlientasTel_numeris=klientas.Tel_numeris
            LEFT JOIN(
          SELECT
            Uzsakymo_numeris AS uzsNR,
            SUM(uzsakymo_patiekalas.Kiekis) AS kiekisPatiekalu,
            COUNT(uzsakymo_patiekalas.fk_PatiekalasPavadinimas) AS patRusys
               FROM
                uzsakymas
              INNER JOIN uzsakymo_patiekalas ON uzsakymas.Uzsakymo_numeris = uzsakymo_patiekalas.fk_UzsakymasUzsakymo_numeris
              GROUP BY
              uzsNR
          ) AS a
        ON
          Uzsakymo_numeris = uzsNR
            LEFT JOIN(
          SELECT
               uzsakymas.fk_RestoranasPavadinimas AS uzsREST,
               SUM(uzsakymas.Kaina) AS bendra_suma
         FROM
              uzsakymas
                INNER JOIN restoranas ON uzsakymas.fk_RestoranasPavadinimas = restoranas.Pavadinimas
                WHERE uzsakymas.Data
                >= IFNULL(?nuo, uzsakymas.Data) AND uzsakymas.Data <= IFNULL(?iki, uzsakymas.Data)
            GROUP BY
                uzsakymas.fk_RestoranasPavadinimas
                    ) AS b
            ON
              pavadinimas = uzsREST) WHERE uzsakymas.Data
             >= IFNULL(?nuo, uzsakymas.Data) AND uzsakymas.Data <= IFNULL(?iki, uzsakymas.Data) ORDER BY pavadinimas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = nuo;
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = iki;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                sutartys.Add(new AtaskaitaViewModel
                {
                    nr = Convert.ToInt32(item["nr"]),
                    data = Convert.ToDateTime(item["data"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    pat_kiekis = Convert.ToInt32(item["patiekalu_Kiekis"]),
                    kaina = Convert.ToDecimal(item["kaina"]),
                    bendraSuma = Convert.ToDecimal(item["bendra_suma"]),
                    skirtPat = Convert.ToInt32(item["patRusys"]),
                    VardasPavarde = Convert.ToString(item["Vardas"]) + " "+ Convert.ToString(item["Pavarde"])
                });
            }
            return sutartys;
        }

       

    }
}