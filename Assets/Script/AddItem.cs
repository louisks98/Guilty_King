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
    public Sprite imgChest;
    private SpriteRenderer sprite;
    public AudioClip sound;

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
                        reader = bd.select("select Quantite from InventaireItem where Personnage = 1 and Item = (select idItem from Item where Nom = '"+ item +"')");
                        while (reader.Read())
                        {
                            nbItem = reader.GetInt32(0);
                            Debug.Log("nbItem : " + nbItem);
                        }

                        bd.insert("update InventaireItem set Quantite = " + (nbItem + 1) + " where Item = (select idItem from Item where Nom = '" + item + "') and Personnage = 1");
                        Debug.Log("item ajouter");
                        Open = true;
                        sprite = GetComponent<SpriteRenderer>();
                        sprite.sprite = imgChest;
                        reader.Dispose();
                        bd.Close();
                        Debug.Log("close addItem");
                        SoundManager.instance.PlaySingle(sound);
                        this.GetComponent<Collider2D>().enabled = false;
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
