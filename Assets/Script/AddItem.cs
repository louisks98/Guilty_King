using Assets.Script;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour {

    public string item;
    public bool Open = false;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        AccesBD bd = new AccesBD();
        if (collision.gameObject.name == "Hero")
        {
            if(!Open)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    int nbItem = 0;
                    SqliteDataReader reader;
                    
                    try
                    {
                        
                        reader = bd.select("select count(idInventaireItem) from InventaireItem where Personnage = 1");
                        while (reader.Read())
                        {
                            nbItem = reader.GetInt32(0);
                            Debug.Log("nbItem : " + nbItem);
                        }

                        bd.insert("insert into InventaireItem values(" + (nbItem + 1) + ",(select idItem from Item where Nom = '" + item + "'), 1, 1)");
                        Debug.Log("item ajouter");
                        Open = true;
                        sprite = GetComponent<SpriteRenderer>();
                        sprite.sprite = Resources.Load(Application.dataPath + @"\Item\chest_Open", typeof(Image)) as Sprite;
                        reader.Dispose();
                        bd.Close();
                        Debug.Log("close addItem");

                    }
                    catch(SqliteException e)
                    {
                        bd.Close();
                        Debug.Log(e);
                    }
                }
                
            }
            
        }
   }

}
