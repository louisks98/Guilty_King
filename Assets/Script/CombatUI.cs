using Assets.Script;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

    public List<Sprite> listSpellHero;
    public List<Sprite> listSpellFire;
    public List<Sprite> listSpellEarth;
    public List<Sprite> listSpellIce;
    public List<Sprite> listEnemySprites;
    public GameObject pnlMenuBtn;
    public GameObject pnlAttakBtn;
    public GameObject pnlEnemySelect;
    public GameObject pnlItemSelect;
    public string selectedSpell { get; set; }
    public int selectedEnemy { get; set; }
    public List<Personnage> listEnnemies;

    private List<Button> listBtnEnemy;
    private List<Button> listBtnSpell;
    private List<Button> ListBtnItem;
    private Dictionary<string, int> listNbItem;
    

    void Start()
    {
        listNbItem = new Dictionary<string, int>();
        listNbItem.Add("health", 0);
        listNbItem.Add("def", 0);
        listNbItem.Add("steroid", 0);
        listNbItem.Add("speed", 0);
        listBtnSpell = new List<Button>(pnlAttakBtn.GetComponentsInChildren<Button>());
        listBtnEnemy = new List<Button>(pnlEnemySelect.GetComponentsInChildren<Button>());
        ListBtnItem = new List<Button>(pnlItemSelect.GetComponentsInChildren<Button>());
    }

    public void onClickAttack()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(true);
        pnlItemSelect.SetActive(false);
    }
    public void onClickCancel()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(true);
        pnlAttakBtn.SetActive(false);
        pnlItemSelect.SetActive(false);
    }

    public void onClickSpell()
    {
        pnlEnemySelect.SetActive(true);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(false);
        pnlItemSelect.SetActive(false);
        AfficherEnemy();
    }

    public void onClickItem()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(false);
        pnlItemSelect.SetActive(true);
        AfficherItem();
    }

    public void UseItem()
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        AccesBD bd = new AccesBD();
        switch (btnName)
        {
            case "Btn_Item_Health":
                if(listNbItem["health"] > 0)
                {
                    string sqlupdate = "update InventaireItem set Quantite = " + listNbItem["health"]-- + "where Item = 3";
                    bd.insert(sqlupdate);
                }
                break;
            case "Btn_Item_Def":
                if (listNbItem["def"] > 0)
                {
                    string sqlupdate = "update InventaireItem set Quantite = " + listNbItem["def"]-- + "where Item = 6";
                    bd.insert(sqlupdate);
                }
                break;
            case "Btn_Item_Steroids":
                if (listNbItem["steroid"] > 0)
                {
                    string sqlupdate = "update InventaireItem set Quantite = " + listNbItem["steroid"]-- + "where Item = 7";
                    bd.insert(sqlupdate);
                }
                break;
            case "Btn_Item_Speed":
                if (listNbItem["speed"] > 0)
                {
                    string sqlupdate = "update InventaireItem set Quantite = " + listNbItem["speed"]-- + "where Item = 5";
                    bd.insert(sqlupdate);
                }
                break;
        }
        onClickCancel();
        bd.Close();
    }

    private void AfficherItem()
    {
        try
        {
            List<Text> listText = new List<Text>(pnlItemSelect.GetComponentsInChildren<Text>());
            AccesBD bd = new AccesBD();
            SqliteDataReader reader;
            string selectNbHealth = "select Quantite from InventaireItem where Personnage = 1 and Item = 3";
            string selectNbDef = "select Quantite from InventaireItem where Personnage = 1 and Item = 6";
            string selectNbSpeed = "select Quantite from InventaireItem where Personnage = 1 and Item = 5";
            string selectNbSteroid = "select Quantite from InventaireItem where Personnage = 1 and Item = 7";

            reader = bd.select(selectNbHealth);
            while(reader.Read())
            {
                listText[0].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["health"] = reader.GetInt32(0);
            }
            reader.Close();

            reader = bd.select(selectNbDef);
            while (reader.Read())
            {
                listText[1].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["def"] = reader.GetInt32(0);
            }
            reader.Close();
            
            reader = bd.select(selectNbSpeed);
            while (reader.Read())
            {
                listText[2].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["speed"] = reader.GetInt32(0);
            }
            reader.Close();

            reader = bd.select(selectNbSteroid);
            while (reader.Read())
            {
                listText[3].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["steroid"] = reader.GetInt32(0);
            }
            reader.Close();
            bd.Close();
        }
        catch(SqliteException e) { Debug.Log(e.Message); }
        
    }
    

    private void AfficherEnemy()
    {
        ReactivateButtons(listBtnEnemy);
        if(listBtnEnemy != null && listEnnemies != null)
        {
            for (int i = 0; i < listBtnEnemy.Count; i++)
            {
                if (i < listEnemySprites.Count && listEnemySprites[i] != null && listEnnemies[i] != null)
                {
                    listBtnEnemy[i].image.sprite = listEnemySprites[i];
                    listBtnEnemy[i].onClick.AddListener(() => {
                        selectedEnemy = listEnnemies[i].id;
                        Debug.Log("enemy id : " + listEnnemies[i].id);
                    });
                    Debug.Log("Afficher enemy sprite" + i);
                }
                else
                    listBtnEnemy[i].gameObject.SetActive(false);
            }
            listBtnEnemy[listBtnEnemy.Count - 1].gameObject.SetActive(true);
        }
    }
        
    public void AfficherSpells(Personnage pers)
    {
        ReactivateButtons(listBtnSpell);
        if(listBtnSpell != null && pers != null)
        {
            switch (pers.name)
            {
                case "Jimmy":
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellHero.Count - 1 && listSpellHero[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellHero[i];
                            listBtnSpell[i].onClick.AddListener(() => {
                                selectedSpell = pers.sorts[i].id;
                                Debug.Log("id sort jimmy :" + pers.sorts[i].id);
                            });
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case "Maryse":
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellFire.Count - 1 && listSpellFire[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellFire[i];
                            listBtnSpell[i].onClick.AddListener(() => {
                                selectedSpell = pers.sorts[i].id;
                                Debug.Log("id sort Maryse :" + pers.sorts[i].id);
                            });
                        }
                            
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case "Bob":
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellEarth.Count - 1 && listSpellEarth[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellEarth[i];
                            listBtnSpell[i].onClick.AddListener(() => {
                                selectedSpell = pers.sorts[i].id;
                                Debug.Log("id sort Bob :" + pers.sorts[i].id);
                            });
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case "Jeanne":
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellIce.Count - 1 && listSpellIce[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellIce[i];
                            listBtnSpell[i].onClick.AddListener(() => {
                                selectedSpell = pers.sorts[i].id;
                                Debug.Log("id sort Jeanne :" + pers.sorts[i].id);
                            });
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;
            }
            listBtnSpell[listBtnSpell.Count - 1].gameObject.SetActive(true);
        }
    }

    private void ReactivateButtons(List<Button> listBtn)
    {
        if(listBtn != null)
        {
            foreach (Button b in listBtn)
            {
                b.gameObject.SetActive(true);
            }
        }
    }
}
