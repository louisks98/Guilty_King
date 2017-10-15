using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class MenuStatTeam : MonoBehaviour {

    private List<Personnage> listPers = new List<Personnage>();
    public List<GameObject> characterPanels = new List<GameObject>();
    public Sprite sp_hero;
    public Sprite sp_Fire;
    public Sprite sp_Forest;
    public Sprite sp_Ice;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    void OnEnable()
    {
        GetStatsTeam();
        Afficher();
    }

    void GetStatsTeam()
    {
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        reader = bd.select("select Nom, Point_de_vie, niveau, nbAmes, Force, Defence, Vitesse from Personnage inner join Stats on Personnage.Stat = Stats.idStats");
        while(reader.Read())
        {
            string nom = reader.GetString(0);
            int hp = reader.GetInt32(1);
            int level = reader.GetInt32(2);
            int ames = reader.GetInt32(3);
            int force = reader.GetInt32(4);
            int def = reader.GetInt32(5);
            int speed = reader.GetInt32(6);
            listPers.Add(new Personnage(nom, hp, level, ames, force, def, speed));
        }
        reader.Close();
        bd.Close();
        listPers[0].Image = sp_hero;
        listPers[1].Image = sp_Forest;
        listPers[2].Image = sp_Fire;
        listPers[3].Image = sp_Ice;
    }

    void Afficher()
    {
        int i = 0;
        foreach (GameObject obj in characterPanels)
        {
            List<Text> txt = new List<Text>(obj.GetComponentsInChildren<Text>());
            List<Image> img = new List<Image>(obj.GetComponentsInChildren<Image>());

            img[1].sprite = listPers[i].Image;
            txt[0].text = listPers[i].Name;
            txt[1].text = "Niveau : " + listPers[i].Level.ToString();
            txt[2].text = "Hp : "  + listPers[i].Hp.ToString() + "/" + listPers[i].Hp.ToString();
            txt[3].text = "Ames : " + listPers[i].NbAmes.ToString();
            i++;
        }
    }
}

//public class Personnage
//{
//    private string name;
//    public string Name
//    {
//        get { return name; }
//        set { name = value; }
//    }
//    private int hp;
//    public int Hp
//    {
//        get { return hp; }
//        set { hp = value; }
//    }
//    private int level;
//    public int Level
//    {
//        get { return level; }
//        set { level = value; }
//    }
//    private int nbAmes;
//    public int NbAmes
//    {
//        get { return nbAmes; }
//        set { nbAmes = value; }
//    }


//    public Personnage(string name, int hp, int level, int nbAme)
//    {
//        this.Name = name;
//        this.Hp = hp;
//        this.Level = level;
//        this.NbAmes = nbAme;
//    }
//}

