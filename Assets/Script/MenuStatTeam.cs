using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuStatTeam : MonoBehaviour {

    private List<Personnage> listPers = new List<Personnage>();
    public List<GameObject> characterPanels = new List<GameObject>();
    public GameObject statsCharacterPanel;
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
        AfficherTeam();
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

    void AfficherTeam()
    {
        int i = 0;
        foreach (GameObject obj in characterPanels)
        {
            List<Text> txt = new List<Text>(obj.GetComponentsInChildren<Text>());
            List<Image> img = new List<Image>(obj.GetComponentsInChildren<Image>());
            obj.GetComponent<Button>().onClick.AddListener(() => { Affichercharacter(); });

            img[1].sprite = listPers[i].Image;
            txt[0].text = listPers[i].Name;
            txt[1].text = "Niveau : " + listPers[i].Level.ToString();
            txt[2].text = "Hp : "  + listPers[i].Hp.ToString() + "/" + listPers[i].Hp.ToString();
            txt[3].text = "Ames : " + listPers[i].NbAmes.ToString();
            i++;
        }
    }
    void Affichercharacter()
    {
        List<Text> txt = new List<Text>(statsCharacterPanel.GetComponentsInChildren<Text>());
        List<Image> img = new List<Image>(statsCharacterPanel.GetComponentsInChildren<Image>());

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "PanelCharacter1":
                txt[0].text = listPers[0].Name;
                txt[1].text = "Niveau : " + listPers[0].Level.ToString();
                txt[2].text = "HP : " + listPers[0].Hp.ToString();
                txt[3].text = "Force : " + listPers[0].Strength.ToString();
                txt[4].text = "Defence : " + listPers[0].Defence.ToString();
                txt[5].text = "Speed : " + listPers[0].Speed.ToString();
                txt[6].text = "Nombre d'ame : " + listPers[0].NbAmes.ToString();
                img[1].sprite = listPers[0].Image;
                break;

            case "PanelCharacter2":
                txt[0].text = listPers[1].Name;
                txt[1].text = "Niveau : " + listPers[1].Level.ToString();
                txt[2].text = "HP : " + listPers[1].Hp.ToString();
                txt[3].text = "Force : " + listPers[1].Strength.ToString();
                txt[4].text = "Defence : " + listPers[1].Defence.ToString();
                txt[5].text = "Speed : " + listPers[1].Speed.ToString();
                txt[6].text = "Nombre d'ame : " + listPers[1].NbAmes.ToString();
                img[1].sprite = listPers[1].Image;
                break;

            case "PanelCharacter3":
                txt[0].text = listPers[2].Name;
                txt[1].text = "Niveau : " + listPers[2].Level.ToString();
                txt[2].text = "HP : " + listPers[2].Hp.ToString();
                txt[3].text = "Force : " + listPers[2].Strength.ToString();
                txt[4].text = "Defence : " + listPers[2].Defence.ToString();
                txt[5].text = "Speed : " + listPers[2].Speed.ToString();
                txt[6].text = "Nombre d'ame : " + listPers[2].NbAmes.ToString();
                img[1].sprite = listPers[2].Image;
                break;

            case "PanelCharacter4":
                txt[0].text = listPers[3].Name;
                txt[1].text = "Niveau : " + listPers[3].Level.ToString();
                txt[2].text = "HP : " + listPers[3].Hp.ToString();
                txt[3].text = "Force : " + listPers[3].Strength.ToString();
                txt[4].text = "Defence : " + listPers[3].Defence.ToString();
                txt[5].text = "Speed : " + listPers[3].Speed.ToString();
                txt[6].text = "Nombre d'ame : " + listPers[3].NbAmes.ToString();
                img[1].sprite = listPers[3].Image;
                break;
        }
        

    }
}

