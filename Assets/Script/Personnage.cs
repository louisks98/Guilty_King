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
        public int id { get; set; }
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

        public FighterMovement deplacement;

        // battle stats
        private int battleHp;
        public int BattleHp
        {
            get { return battleHp; }
            set
            {
                battleHp = value;
                if (battleHp <= 0)
                {
                    battleHp = 0;
                    defeated = true;
                }

                if (battleHp >= hpTotal)
                {
                    battleHp = hpTotal;
                }
            }
        }
        public int turnStunned { get; set; }
        public bool stunned { get; set; } //?
        public int battleStr { get; set; }
        public int battleDef { get; set; }
        public int battleSpd { get; set; }

        public List<Sort> sorts { get; set; }
        

        public Personnage(string name, int hp, int level, int nbAme, int str, int def, int sp)
        {
            this.name = name;
            this.hpTotal = hp;
            this.level = level;
            this.nbAmes = nbAme;
            this.strength = str;
            this.defence = def;
            this.speed = sp;
            
            setupBattleStats();
        }

        public Personnage(GameObject gameObject, int id_personnage)
        {
            this.gameObject = gameObject;

            id = id_personnage;

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
                //if (reader.GetString(7) == "N")
                //    defeated = false;
                //else if (reader.GetString(7) == "O")
                //    defeated = true;
                deplacement = gameObject.GetComponent<FighterMovement>();
                setupBattleStats();
                defeated = false;
            }

            reader = bd.select("SELECT Valeur,Type,NbAttaque,Acquis,Nom from Sort where Personnage = " + id_personnage);
            sorts = new List<Sort>();

            while (reader.Read())
            {
                sorts.Add(new Sort(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2),reader.GetString(3),reader.GetString(4)));
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
        public void dealDamage(int nbDamage)
        {
            BattleHp = BattleHp + nbDamage;
        }
    }

    public class Sort
    {
        public int damage { get; set; }
        public string type { get; set; }
        public int nbAttaque { get; set; }
        public string acquis { get; set; }
        public string nom { get; set; }

        public Sort()
        {

        }

        public Sort(int damage, string type, int nbAttaque, string acquis, string nom)
        {
            this.damage = damage;
            this.type = type;
            this.nbAttaque = nbAttaque;
            this.acquis = acquis;
            this.nom = nom;
        }
    }
}
