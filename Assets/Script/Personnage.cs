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
        // base stats
        public string name { get; set; }
        public int hpTotal { get; set; }
        public int level { get; set; }
        public int nbAmes { get; set; }
        public int strength { get; set; }
        public int defence { get; set; }
        public int speed { get; set; }
        public Sprite image { get; set; }
        public bool defeated { get; set; }
        public GameObject gameObject { get; set; }

        private FighterMovement deplacement;

        // battle stats
        public int battleHp {
            get { return battleHp; }
            set
            {
                if(battleHp + value <= 0)
                {
                    battleHp = 0;
                    defeated = true;
                }
                else { battleHp -= value; }

                if (battleHp + value >= hpTotal) {battleHp = hpTotal;}
                else { battleHp += value; }
                
            }
        }
        public int turnStunned { get; set; }
        public bool stunned { get; set; } //?
        public int battleStr { get; set; }
        public int battleDef { get; set; }
        public int battleSpd { get; set; }

        

        public Personnage(string name, int hp, int level, int nbAme, int str, int def, int sp)
        {
            this.name = name;
            this.hpTotal = hp;
            this.level = level;
            this.nbAmes = nbAme;
            this.strength = str;
            this.defence = def;
            this.speed = sp;
            deplacement = gameObject.GetComponent<FighterMovement>();
        }

        public Personnage(GameObject gameObject, int id_personnage)
        {
            this.gameObject = gameObject;

            AccesBD bd = new AccesBD();
            SqliteDataReader reader = bd.select("select Nom, Point_de_vie, niveau, nbAmes, Force, Defence, Vitesse, vaincue from Personnage inner join Stats on Personnage.Stat = Stats.idStats WHERE idPersonnage =" + id_personnage);

            while (reader.Read())
            {
                name = reader.GetString(0);
                hpTotal = reader.GetInt32(1);
                level = reader.GetInt32(2);
                nbAmes = reader.GetInt32(3);
                strength = reader.GetInt32(4);
                defence = reader.GetInt32(5);
                speed = reader.GetInt32(6);
                if (reader.GetString(7) == "N")
                    defeated = false;
                else if (reader.GetString(7) == "O")
                    defeated = true;
                Debug.Log(reader.GetValue(0).ToString() + reader.GetValue(1).ToString() + reader.GetValue(2).ToString() + reader.GetValue(3).ToString() + reader.GetValue(4).ToString() + reader.GetValue(5).ToString());
            }
                        
            bd.Close();
        }

        public void setupBattleStats()
        {
            turnStunned = 0;
            battleHp = hpTotal;
            battleStr = strength;
            battleDef = defence;
            battleSpd = speed;
        }
        public void MoveLeft()
        {
            deplacement.movingLeft = true;
        }
        public void MoveRight()
        {
            deplacement.movingRight = true;
        }
    }
}
