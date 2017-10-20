using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

namespace Assets.Script
{
    class Personnage
    {
        public string name { get; set; }
        public int hp { get; set; }
        public int level { get; set; }
        public int nbAmes { get; set; }
        public int strength { get; set; }
        public int defence { get; set; }
        public int speed { get; set; }
        public Sprite image { get; set; }
        public bool defeated { get; set; }
        public GameObject gameObject { get; set; }

        public Personnage(string name, int hp, int level, int nbAme, int str, int def, int sp)
        {
            this.name = name;
            this.hp = hp;
            this.level = level;
            this.nbAmes = nbAme;
            this.strength = str;
            this.defence = def;
            this.speed = sp;
        }

        public Personnage(GameObject gameObject, int id_personnage)
        {
            this.gameObject = gameObject;

            AccesBD bd = new AccesBD();
            SqliteDataReader reader = bd.select("SELECT * FROM Personnage INNER JOIN Stats ON Personnage.Stat = Stats.idStats WHERE idPersonnage =" + id_personnage);

            while (reader.Read())
            {
                Debug.Log(reader.GetValue(0).ToString() + reader.GetValue(1).ToString() + reader.GetValue(2).ToString() + reader.GetValue(3).ToString() + reader.GetValue(4).ToString() + reader.GetValue(5).ToString());
            }
                        
            bd.Close();
        }
    }
}
