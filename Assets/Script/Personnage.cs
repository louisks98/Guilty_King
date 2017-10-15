using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Script
{
    class Personnage
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int hp;
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        private int nbAmes;
        public int NbAmes
        {
            get { return nbAmes; }
            set { nbAmes = value; }
        }
        private int strength;
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        private int defence;
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
        }
        private int speed;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Sprite image;
        public Sprite Image
        {
            get { return image; }
            set { image = value; }
        }
        private bool defeated;
        public bool isDefeated
        {
            get { return defeated; }
            set { defeated = value; }
        }


        public Personnage(string name, int hp, int level, int nbAme, int str, int def, int sp)
        {
            Name = name;
            Hp = hp;
            Level = level;
            NbAmes = nbAme;
            Strength = str;
            Defence = def;
            Speed = sp;
        }
    }
}
