using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

namespace Assets.Script
{
    class AccesBD
    {
        private SqliteConnection dbconn;
        SqliteCommand cmd;
        //private IDbCommand dbcmd;
        public AccesBD()
        {
            string conn = "URI=file:" + Application.dataPath + "/BD.db"; //Path to database.
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
        }

        public SqliteDataReader select(string query)
        {
            try
            {
               cmd = new SqliteCommand(query, dbconn);
            }
            catch(SqliteException e)
            {
                Close();
                Debug.Log(e);
            }
            return cmd.ExecuteReader();
        }

        public void insert(string query)
        {
            try
            {
                cmd = new SqliteCommand();
                cmd.Connection = dbconn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch(SqliteException e)
            {
                Close();
                Debug.Log(e);
            }
            
        }
        public void ClearBD()
        {
            // Table Personnage
            cmd = new SqliteCommand();
            cmd.Connection = dbconn;
            cmd.CommandText = "update Personnage set vaincue = 'N'";
            cmd.ExecuteNonQuery();
            
            cmd.CommandText = "update Personnage set vaincue = 'O' where idPersonnage = 1";
            cmd.ExecuteNonQuery();

            // Inventaire
            cmd.CommandText = "update InventaireItem set Quantite = 0";
            cmd.ExecuteNonQuery();

            // Stats
            cmd.CommandText = "update Stats set Point_de_Vie = 1500, Force = 0, Defence = 0, Vitesse = 0, nbAmes = 0 where idStats = 1";
            cmd.ExecuteNonQuery();

            // Sort
            cmd.CommandText = "update Sort set Acquis = 'N' where id like 'H2'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update Sort set Acquis = 'N' where id like 'H3'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "update Sort set Acquis = 'N' where id like 'H4'";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            Close();
        }


        public void Close()
        {
            dbconn.Close();
            dbconn = null;
        }

    }
}
