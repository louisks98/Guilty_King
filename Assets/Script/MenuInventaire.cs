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
    private Dictionary<int, bool> itemPlaced = new Dictionary<int, bool>();
    private List<Text> textinfo;
    Dictionary<int,bool> placedSlots = new Dictionary<int, bool>();
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
                if (!itemPlaced.ContainsKey(i))
                    itemPlaced.Add(i, false);
            }
            i++;
        }
        bd.Close();
        Debug.Log("close get_item");
        Debug.Log("get item");
    }
    public void Afficher()
    {
        //get_Item();
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        int nbItem = 0;
        List<Image> slots = new List<Image>(panel.GetComponentsInChildren<Image>());
        List<Text> textNb = new List<Text>(panel.GetComponentsInChildren<Text>());
        
        int NbItemPlacer = 0;
        textinfo[0].text = "";
        textinfo[1].text = "";

        for (int i = 0; i < item.Count; i++)
        {
            string sql = "select Quantite from InventaireItem where Personnage = 1 and Item = (select idItem from Item where Nom = '" + item[i] + "')";
            reader = bd.select(sql);
            while (reader.Read())
            {
                nbItem = reader.GetInt32(0);
            }
            if (nbItem > 0)
            {
                for (int j = 0; j < slots.Count; j++)
                {
                    if (!itemPlaced[i])
                    {
                        if (placedSlots.ContainsKey(j + 1))
                        {
                            if(!placedSlots[j + 1])
                            {
                                slots[j + 1].sprite = imageItem[i];
                                textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                itemPlaced[i] = true;
                                placedSlots[j + 1] = true;
                                NbItemPlacer++;
                            }
                            else
                            {
                                if(slots[j + 1].sprite == imageItem[i])
                                {
                                    textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                                    NbItemPlacer++;
                                }
                            }
                        }
                        else
                        {
                            slots[j + 1].sprite = imageItem[i];
                            textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                            itemPlaced[i] = true;
                            placedSlots.Add(j + 1, true);
                            NbItemPlacer++;
                        }
                    }
                    else
                    {
                        if (slots[j + 1].sprite == imageItem[i])
                        {
                            textNb[NbItemPlacer].text = "x" + nbItem.ToString();
                            NbItemPlacer++;
                        }
                    }
                    j++;
                }
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
