using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;

public class MenuInventaire : MonoBehaviour {

    // todo change tes tableau en list

    private List<int> item = new List<int>();
    public List<Sprite> imageItem = new List<Sprite>();
    public GameObject panel;

    // Use this for initialization
    void Start()
    {
        //imageItem.Add(Resources.Load(@"\health.png") as Sprite);
        //imageItem.Add(Resources.Load(@"\mana.png") as Sprite);
        //imageItem.Add(Resources.Load(@"\steroid.png") as Sprite);
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
            item.Add(reader.GetInt32(0));
            i++;
        }
        bd.Close();
        Debug.Log("get item");
    }
    private void Afficher()
    {
        List<Image> slots = new List<Image>(panel.GetComponentsInChildren<Image>());
        Image img;
        bool isPlaced;

        for(int i = 0; i < item.Count ; i++)
        {
            isPlaced = false;
            for (int j = 1; j < slots.Count && !isPlaced; j++)
            {
                if(slots[j + 1].sprite == null)
                {
                    switch(item[i])
                    {
                        case 3:
                            img = slots[i + 1].transform.GetChild(0).GetComponent<Image>();
                            img.sprite = imageItem[0];
                            Debug.Log("health");
                            isPlaced = true;
                            break;
                        case 4:
                            img = slots[i + 1].transform.GetChild(0).GetComponent<Image>();
                            img.sprite = imageItem[1];
                            Debug.Log("mana");
                            isPlaced = true;
                            break;
                        case 7:
                            img = slots[i + 1].transform.GetChild(0).GetComponent<Image>();
                            img.sprite = imageItem[3];
                            Debug.Log("steroid");
                            isPlaced = true;
                            break;
                    }
                }
                j++;
            }
        }
    }
}
