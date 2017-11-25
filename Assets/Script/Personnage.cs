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
    public class Personnage
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

        private int battleDef;
        public int BattleDef
        {
            get { return battleDef; }
            set
            {
                battleDef = value;
                if (battleDef < 0)
                {
                    battleDef = 0;
                }

                if (battleDef > 95)
                {
                    battleDef = 95;
                }
            }
        }

        private int battleStr;
        public int BattleStr
        {
            get { return battleStr; }
            set
            {
                battleStr = value;
                if (battleStr < 0)
                {
                    battleStr = 0;
                }

                if (battleStr > 200)
                {
                    battleStr = 200;
                }
            }
        }


        private int battleSpd;
        public int BattleSpd
        {
            get { return battleSpd; }
            set
            {
                battleSpd = value;
                if (battleSpd < 0)
                {
                    battleSpd = 0;
                }

                if (battleSpd > 95)
                {
                    battleSpd = 95;
                }
            }
        }

        public List<Sort> sorts { get; set; }

        public Personnage(string name, int hp, int level, int nbAme, int str, int def, int sp, bool defeat)
        {
            this.name = name;
            this.hpTotal = hp;
            this.level = level;
            this.nbAmes = nbAme;
            this.strength = str;
            this.defence = def;
            this.speed = sp;
            this.defeated = defeat;
            
            setupBattleStats();
        }

        public Personnage(GameObject gameObject, int id_personnage)
        {
            this.gameObject = gameObject;

            AccesBD bd = new AccesBD();
            SqliteDataReader reader = bd.select("select Nom, Point_de_vie, niveau, nbAmes, Force, Defence, Vitesse, vaincue from Personnage inner join Stats on Personnage.Stat = Stats.idStats WHERE idPersonnage =" + id_personnage);

            while (reader.Read())
            {
                id = id_personnage;
                name = reader.GetString(0);
                hpTotal = reader.GetInt32(1);
                level = reader.GetInt32(2);
                nbAmes = reader.GetInt32(3);
                strength = reader.GetInt32(4);
                defence = reader.GetInt32(5);
                speed = reader.GetInt32(6);
                deplacement = gameObject.GetComponent<FighterMovement>();
                setupBattleStats();
                defeated = false;
            }

            reader = bd.select("select id,nom,description,valeur,acquis,type,nbattaque from Sort where personnage = " + id_personnage);
            sorts = new List<Sort>();

            while (reader.Read())
            {
                sorts.Add(new Sort(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6)));
            }

            bd.Close();
        }

        public void setupBattleStats()
        {
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

        public void Attaque()
        {
            deplacement.isAttacking = true;
        }

        public void Die()
        {
            deplacement.isDying = true;
        }

        public void dealDamage(int nbDamage)
        {
            if(defence > 0 && nbDamage < 0)
            {
                BattleHp = BattleHp + nbDamage - (nbDamage * defence / 100);
            }
            else
            {
                BattleHp = BattleHp + nbDamage;
            }
        }

        public int GetDamage(string id)
        {
            int damage = 0;
            foreach(Sort item in sorts)
            {
                if(item.id.Equals(id))
                {
                    damage = item.valeur;

                    if (strength > 0) // On veut tu pouvoir ammener le damage à zero avec beaucoup de débuff.
                    {
                        if(item.type == "AS" || item.type == "AZ")
                        {
                            damage += damage * strength / 100;
                        }
                    }
                }
            }

            return damage;
        }
    }

    public class Sort
    {
        public string id { get; set; }
        public string nom { get; set; }
        public string description { get; set; }
        public int valeur { get; set; }
        public string acquis { get; set; }
        public string type { get; set; }
        public int nbattaque { get; set; }

        public Sort(string id, string nom, string description, int valeur, string acquis, string type, int nbattaque)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            if (type == "GR")
                this.valeur = -valeur;
            else
                this.valeur = valeur;
            this.acquis = acquis;
            this.type = type;
            this.nbattaque = nbattaque;
        }
    }
}
