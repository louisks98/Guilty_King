using Assets.Script;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private List<Button> listBtnEnemy;
    private List<Button> listBtnSpell;

    void Start()
    {
        listBtnSpell = new List<Button>(pnlAttakBtn.GetComponentsInChildren<Button>());
        listBtnEnemy = new List<Button>(pnlEnemySelect.GetComponentsInChildren<Button>());
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
            }
            reader.Close();

            reader = bd.select(selectNbDef);
            while (reader.Read())
            {
                listText[1].text = "x" + reader.GetInt32(0).ToString();
            }
            reader.Close();
            
            reader = bd.select(selectNbSpeed);
            while (reader.Read())
            {
                listText[2].text = "x" + reader.GetInt32(0).ToString();
            }
            reader.Close();

            reader = bd.select(selectNbSteroid);
            while (reader.Read())
            {
                listText[3].text = "x" + reader.GetInt32(0).ToString();
            }
            reader.Close();
            bd.Close();
        }
        catch(SqliteException e) { Debug.Log(e.Message); }
        
    }
    

    private void AfficherEnemy()
    {
        ReactivateButtons(listBtnEnemy);
        if(listBtnEnemy != null)
        {
            for (int i = 0; i < listBtnEnemy.Count; i++)
            {
                if (i < listEnemySprites.Count && listEnemySprites[i] != null)
                {
                    listBtnEnemy[i].image.sprite = listEnemySprites[i];
                    Debug.Log("Afficher enemy sprite" + i);
                }
                else
                    listBtnEnemy[i].gameObject.SetActive(false);
            }
            listBtnEnemy[listBtnEnemy.Count - 1].gameObject.SetActive(true);
        }
    }
        
    public void AfficherSpells(int player)
    {
        ReactivateButtons(listBtnSpell);
        if(listBtnSpell != null)
        {
            switch (player)
            {
                case 0:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellHero.Count - 1 && listSpellHero[i] != null)
                            listBtnSpell[i].image.sprite = listSpellHero[i];
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 1:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellFire.Count - 1 && listSpellFire[i] != null)
                            listBtnSpell[i].image.sprite = listSpellFire[i];
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 2:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellEarth.Count - 1 && listSpellEarth[i] != null)
                            listBtnSpell[i].image.sprite = listSpellEarth[i];
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 3:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellIce.Count - 1 && listSpellIce[i] != null)
                            listBtnSpell[i].image.sprite = listSpellIce[i];
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
