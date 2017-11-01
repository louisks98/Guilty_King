using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class MenuInventaire : MonoBehaviour {

    private List<string> item = new List<string>();
    private Dictionary<int, bool> placed = new Dictionary<int, bool>();
    public List<Sprite> imageItem = new List<Sprite>();
    public GameObject panel;

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
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        SqliteDataReader reader2;
        reader =  bd.select("select Item from InventaireItem where Personnage = 1");

        int i = 0;
        while(reader.Read())
        {
            reader2 =  bd.select("select Nom from Item where idItem = " + reader.GetInt32(0));
            while(reader2.Read())
            {
                item.Add(reader2.GetString(0));
                if (!placed.ContainsKey(i))
                    placed.Add(i, false);
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
        List<Image> slots = new List<Image>(panel.GetComponentsInChildren<Image>());
        Image img;

        for(int i = 0; i < item.Count ; i++)
        {
            for (int j = 1; j < slots.Count && !placed[i]; j++)
            {
                if(slots[j + 1].sprite == null)
                {
                    switch(item[i])
                    {
                        case "Regeneration":
                            img = slots[j + 1];
                            img.sprite = imageItem[0];
                            Debug.Log("health");
                            placed[i] = true;
                            break;
                        case "Harden":
                            img = slots[j + 1];
                            img.sprite = imageItem[1];
                            Debug.Log("mana");
                            placed[i] = true;
                            break;
                        case "steroids":
                            img = slots[j + 1];
                            img.sprite = imageItem[2];
                            Debug.Log("steroid");
                            placed[i] = true;
                            break;
                        case "Speed boost" :
                            img = slots[j + 1];
                            img.sprite = imageItem[3];
                            Debug.Log("Speed boost");
                            placed[i] = true;
                            break;
                    }
                }
                j++;
            }
        }
    }
}
