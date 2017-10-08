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

                //IDbCommand dbcmd = dbconn.CreateCommand();
                //dbcmd.CommandText = query;
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

        public void Close()
        {
            //cmd.Dispose();
            //cmd = null;
            dbconn.Close();
            dbconn = null;
        }

    }
}
