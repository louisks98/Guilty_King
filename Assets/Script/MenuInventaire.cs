using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class MenuInventaire : MonoBehaviour {

    // todo change tes tableau en list

    private int[] item = new int[20];
    private Sprite[] imageItem = new Sprite[10];

    // Use this for initialization
    void Start()
    {
        imageItem[0] = Resources.Load(Application.dataPath + @"\Item\health") as Sprite;
        imageItem[1] = Resources.Load(Application.dataPath + @"\Item\mana") as Sprite;
        imageItem[2] = Resources.Load(Application.dataPath + @"\Item\steroid") as Sprite;
        get_Item();
        Afficher();
    }
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void get_Item()
    {
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        reader =  bd.select("select Item from InventaireItem where Personnage = 1");
        while(reader.Read())
        {
            int i = 0;
            item[i] = reader.GetInt32(0);
            i++;
        }
        Debug.Log("get item");
    }
    private void Afficher()
    {
        GameObject panel = GameObject.Find("Slot panel");
        Image[] slots = panel.GetComponents<Image>();
        Image img;

        for(int i = 0; i < item.Length; i++)
        {
            for (int j = 0; j < slots.Length; i++)
            {
                if(slots[i] == null)
                {
                    switch(item[i])
                    {
                        case 3:
                            img = slots[i].GetComponentInChildren<Image>();
                            img.sprite = imageItem[0];
                            Debug.Log("health");
                            break;
                        case 4:
                            img = slots[i].GetComponentInChildren<Image>();
                            img.sprite = imageItem[1];
                            Debug.Log("mana");
                            break;
                        case 7:
                            img = slots[i].GetComponentInChildren<Image>();
                            img.sprite = imageItem[3];
                            Debug.Log("steroid");
                            break;
                    }
                }
            }
        }
    }
}
