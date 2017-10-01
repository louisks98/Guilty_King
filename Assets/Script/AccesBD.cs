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
        private IDbCommand dbcmd;
        public AccesBD()
        {
            string conn = "URI=file:" + Application.dataPath + "/BD.db"; //Path to database.
            dbconn = new SqliteConnection(conn);
            dbconn.Open();
        }

        public IDataReader select(string query)
        {
            
            IDbCommand dbcmd = dbconn.CreateCommand();
            dbcmd.CommandText = query;
            return dbcmd.ExecuteReader();
        }

        public void insert(string query)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconn;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void Close()
        {
            //dbcmd.Dispose();
            //dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }

    }
}
