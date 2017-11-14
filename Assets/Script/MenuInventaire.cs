using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.EventSystems;

public class MenuInventaire : MonoBehaviour {

    private List<string> item = new List<string>();
    private Dictionary<string, string> itemDesc = new Dictionary<string, string>();
    private Dictionary<int, bool> placed = new Dictionary<int, bool>();
    private List<Text> textinfo;
    public List<Sprite> imageItem = new List<Sprite>();
    public GameObject panel;
    public GameObject pnlInfo;

    //void Start()
    //{
    //    enabled = false;
    //}

    void OnEnable()
    {
        get_Item();
        Afficher();
    }

    void OnDisable()
    {
        item.Clear();
    }

    private void get_Item()
    {
        textinfo = new List<Text>(pnlInfo.GetComponentsInChildren<Text>());
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        SqliteDataReader reader2;
        reader =  bd.select("select Item from InventaireItem where Personnage = 1");

        int i = 0;
        while(reader.Read())
        {
            reader2 =  bd.select("select Nom , Description from Item where idItem = " + reader.GetInt32(0));
            while(reader2.Read())
            {
                item.Add(reader2.GetString(0));
                if(!itemDesc.ContainsKey(reader2.GetString(0)))
                {
                    itemDesc.Add(reader2.GetString(0), reader2.GetString(1));
                }
                if (!placed.ContainsKey(i))
                    placed.Add(i, false);
            }
            i++;
        }
        bd.Close();
        //Debug.Log("close get_item");
        //Debug.Log("get item");
    }
    public void Afficher()
    {
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;

        string selectNbHealth = "select Quantite from InventaireItem where Personnage = 1 and Item = 3";
        string selectNbDef = "select Quantite from InventaireItem where Personnage = 1 and Item = 6";
        string selectNbSpeed = "select Quantite from InventaireItem where Personnage = 1 and Item = 5";
        string selectNbSteroid = "select Quantite from InventaireItem where Personnage = 1 and Item = 7";

        int nbItem = 0;
        //get_Item();
        List<Image> slots = new List<Image>(panel.GetComponentsInChildren<Image>());
        List<Text> textNb = new List<Text>(panel.GetComponentsInChildren<Text>());
        Image img;
        int NbItemPlacer= 0;

        for(int i = 0; i < item.Count ; i++)
        {
            for (int j = 0; j < slots.Count && !placed[i]; j++)
            {
                if(slots[j + 1].sprite != imageItem[0] && slots[j + 1].sprite != imageItem[1] 
                    && slots[j + 1].sprite != imageItem[2] && slots[j + 1].sprite != imageItem[3])
                {
                    switch (item[i])
                    {
                        case "Regeneration":
                            reader = bd.select(selectNbHealth);
                            while (reader.Read())
                            {
                                nbItem = reader.GetInt32(0);
                            }
                            if (nbItem > 0)
                            {
                                img = slots[j + 1];
                                img.sprite = imageItem[0];
                                textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                Debug.Log("health");
                                placed[i] = true;
                                NbItemPlacer++;
                            }
                            reader.Close();
                            break;

                        case "Harden":
                            reader = bd.select(selectNbDef);
                            while (reader.Read())
                            {
                                nbItem = reader.GetInt32(0);
                            }
                            if (nbItem > 0)
                            {
                                img = slots[j + 1];
                                img.sprite = imageItem[1];
                                textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                Debug.Log("mana");
                                placed[i] = true;
                                NbItemPlacer++;
                            }
                            reader.Close();
                            break;

                        case "steroids":
                            reader = bd.select(selectNbSteroid);
                            while (reader.Read())
                            {
                                nbItem = reader.GetInt32(0);
                            }
                            if (nbItem > 0)
                            {
                                img = slots[j + 1];
                                img.sprite = imageItem[2];
                                textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                Debug.Log("steroid");
                                placed[i] = true;
                                NbItemPlacer++;
                            }
                            reader.Close();
                            break;

                        case "Speed boost":
                            reader = bd.select(selectNbSpeed);
                            while (reader.Read())
                            {
                                nbItem = reader.GetInt32(0);
                            }
                            if (nbItem > 0)
                            {
                                img = slots[j + 1];
                                img.sprite = imageItem[3];
                                textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                Debug.Log("Speed boost");
                                placed[i] = true;
                                NbItemPlacer++;
                            }
                            break;
                    }
                }
                j++;
            }
        }
    }

    public void ShowInfo()
    {
        Image imgPanel = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Image>();
        List<Image> imgItem = new List<Image>(imgPanel.GetComponentsInChildren<Image>());

        switch (imgItem[1].sprite.name)
        {
            case "health":
                textinfo[0].text = item[0];
                textinfo[1].text = itemDesc[item[0]];
                break;

            case "def":
                textinfo[0].text = item[2];
                textinfo[1].text = itemDesc[item[2]];
                break;

            case "steroid":
                textinfo[0].text = item[3];
                textinfo[1].text = itemDesc[item[3]];
                break;

            case "speed":
                textinfo[0].text = item[1];
                textinfo[1].text = itemDesc[item[1]];
                break;
        }
    }
}
