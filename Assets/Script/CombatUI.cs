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
    public List<Sprite> listEnemySprites { get; set; }
    public List<Sprite> listAllySprites { get; set; }
    public GameObject pnlMenuBtn;
    public GameObject pnlAttakBtn;
    public GameObject pnlEnemySelect;
    public GameObject pnlItemSelect;
    public Sort selectedSpell { get; set; }
    public int selectedEnemy { get; set; }
    public List<Personnage> listEnnemies { get; set; }
    public List<Personnage> listAllies { get; set; }

    private List<Button> listBtnEnemy;
    private List<Button> listBtnSpell;
    private List<Button> ListBtnItem;
    private List<Text> toolTipSpellCharacter;
    private List<Text> toolTipItem;
    private List<Image> bgToolTipSpellCharacter;
    private List<Image> bgtoolTipItem;
    private Dictionary<string, int> listNbItem;
    private GameObject combatMessage;

    private Personnage currentPerso { get; set; }

    public void Start_Init_UI()
    {
        listNbItem = new Dictionary<string, int>();
        listNbItem.Add("health", 0);
        listNbItem.Add("def", 0);
        listNbItem.Add("steroid", 0);
        listNbItem.Add("speed", 0);
        listBtnSpell = new List<Button>(pnlAttakBtn.GetComponentsInChildren<Button>());
        listBtnSpell.Remove(listBtnSpell[listBtnSpell.Count-1]);

        listBtnEnemy = new List<Button>(pnlEnemySelect.GetComponentsInChildren<Button>());
        listBtnEnemy.Remove(listBtnEnemy[listBtnEnemy.Count - 1]);

        ListBtnItem = new List<Button>(pnlItemSelect.GetComponentsInChildren<Button>());
        ListBtnItem.Remove(ListBtnItem[ListBtnItem.Count - 1]);

        combatMessage =GameObject.Find("Combat_Message");

        toolTipItem = new List<Text>(GameObject.Find("ToolTipItem").GetComponentsInChildren<Text>());
        toolTipSpellCharacter = new List<Text>(GameObject.Find("ToolTipSpell_CharSelect").GetComponentsInChildren<Text>());

        bgToolTipSpellCharacter = new List<Image>(GameObject.Find("ToolTipSpell_CharSelect").GetComponentsInChildren<Image>());
        bgtoolTipItem = new List<Image>(GameObject.Find("ToolTipItem").GetComponentsInChildren<Image>());
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

    public void onClickSpell(int i)
    {
        pnlEnemySelect.SetActive(true);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(false);
        pnlItemSelect.SetActive(false);

        selectedSpell = currentPerso.sorts[i];

        if (selectedSpell.type == "GR" || selectedSpell.type == "AD" || selectedSpell.type == "AF")
        {
            AfficherAlly();
        }
        else if (currentPerso.sorts[i].type == "AZ")
        {
            closeMenu();
        }
        else
        {
            AfficherEnemy();
        }
    }

    public void onClickItem()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(false);
        pnlItemSelect.SetActive(true);
        AfficherItem();
    }

    public void onCLickTarget(int i)
    {
        if (selectedSpell.type == "GR" || selectedSpell.type == "AD" || selectedSpell.type == "AF")
        {
            if (listAllies[i] != null)
            {
                selectedEnemy = listAllies[i].id;
            }
        }
        else
        {
            if (listEnnemies[i] != null)
            {
                selectedEnemy = listEnnemies[i].id;
            }
        }
        closeMenu();
    }

    public void closeMenu()
    {
        onClickCancel();
        CombatTurn.selecting = false;
        HideMenu();
    }

    public void UseItem()
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        int buff = 0;
        switch (btnName)
        {
            case "Btn_Item_Health":
                if(listNbItem["health"] > 0)
                {
                    string sqlStat = "select Stat from Item where iditem = 3";
                    reader = bd.select(sqlStat);
                    while(reader.Read())
                    {
                        buff = reader.GetInt32(0);
                    }
                    currentPerso.BattleHp += buff;
                    string sqlupdate = "update InventaireItem set Quantite = " + (listNbItem["health"]- 1) + " where Item = 3";
                    bd.insert(sqlupdate);
                    StartCoroutine(ShowMessage(currentPerso.name + " a utilisé : Regénération", 2));
                    selectedSpell = null;
                    closeMenu();
                    SoundManager.instance.PlayItem();
                }
                break;
            case "Btn_Item_Def":
                if (listNbItem["def"] > 0)
                {
                    string sqlStat = "select Stat from Item where iditem = 6";
                    reader = bd.select(sqlStat);
                    while (reader.Read())
                    {
                        buff = reader.GetInt32(0);
                    }
                    currentPerso.BattleDef += buff;
                    string sqlupdate = "update InventaireItem set Quantite = " + (listNbItem["def"]-1) + " where Item = 6";
                    bd.insert(sqlupdate);
                    StartCoroutine(ShowMessage(currentPerso.name + " a utilisé : Harden", 2));
                    selectedSpell = null;
                    closeMenu();
                }
                break;
            case "Btn_Item_Steroids":
                if (listNbItem["steroid"] > 0)
                {
                    string sqlStat = "select Stat from Item where iditem = 7";
                    reader = bd.select(sqlStat);
                    while (reader.Read())
                    {
                        buff = reader.GetInt32(0);
                    }
                    currentPerso.BattleStr += buff;
                    string sqlupdate = "update InventaireItem set Quantite = " + (listNbItem["steroid"]-1) + " where Item = 7";
                    bd.insert(sqlupdate);
                    StartCoroutine(ShowMessage(currentPerso.name + " a utilisé : Stéroid", 2));
                    selectedSpell = null;
                    closeMenu();
                }
                break;
            case "Btn_Item_Speed":
                if (listNbItem["speed"] > 0)
                {
                    string sqlStat = "select Stat from Item where idItem = 5";
                    reader = bd.select(sqlStat);
                    while (reader.Read())
                    {
                        buff = reader.GetInt32(0);
                    }
                    currentPerso.battleSpd += buff;
                    string sqlupdate = "update InventaireItem set Quantite = " + (listNbItem["speed"]- 1) + " where Item = 5";
                    bd.insert(sqlupdate);
                    StartCoroutine(ShowMessage(currentPerso.name + " a utilisé : Speed boost", 2));
                    selectedSpell = null;
                    closeMenu();
                }
                break;
        }
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
                listText[3].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["speed"] = reader.GetInt32(0);
            }
            reader.Close();

            reader = bd.select(selectNbSteroid);
            while (reader.Read())
            {
                listText[2].text = "x" + reader.GetInt32(0).ToString();
                listNbItem["steroid"] = reader.GetInt32(0);
            }
            reader.Close();
            bd.Close();
        }
        catch(SqliteException e) { Debug.Log(e.Message); }
        
    }
    
    public void AfficherEnemy()
    {
        ReactivateButtons(listBtnEnemy);
        if(listBtnEnemy != null && listEnnemies != null)
        {
            for (int i = 0; i < listBtnEnemy.Count; i++)
            {
                if (i < listEnemySprites.Count && listEnemySprites[i] != null && listEnnemies[i] != null)
                {
                    listBtnEnemy[i].image.sprite = listEnemySprites[i];
                }
                else
                    listBtnEnemy[i].gameObject.SetActive(false);
            }
            //listBtnEnemy[listBtnEnemy.Count - 1].gameObject.SetActive(true);
        }
    }

    public void AfficherAlly()
    {
        ReactivateButtons(listBtnEnemy);
        if (listBtnEnemy != null && listAllies != null)
        {
            for (int i = 0; i < listBtnEnemy.Count; i++)
            {
                if (i < listAllySprites.Count && listAllySprites[i] != null && listAllies[i] != null)
                {
                    listBtnEnemy[i].image.sprite = listAllySprites[i];
                    //Debug.Log("Afficher enemy sprite" + i);
                }
                else
                    listBtnEnemy[i].gameObject.SetActive(false);
            }
            //listBtnEnemy[listBtnEnemy.Count - 1].gameObject.SetActive(true);
        }
    }

    public void AfficherSpells(Personnage pers)
    {
        currentPerso = pers;
        ReactivateButtons(listBtnSpell);
        if(listBtnSpell != null && pers != null)
        {
            switch (pers.id)
            {
                case 1:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellHero.Count && listSpellHero[i] != null && pers.sorts[i] != null && pers.sorts[i].acquis.Equals("O"))
                        {
                            listBtnSpell[i].image.sprite = listSpellHero[i];
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 3:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellFire.Count && listSpellFire[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellFire[i];
                        }
                            
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 2:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellEarth.Count && listSpellEarth[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellEarth[i];
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;

                case 4:
                    for (int i = 0; i < listBtnSpell.Count; i++)
                    {
                        if (i < listSpellIce.Count && listSpellIce[i] != null && pers.sorts[i] != null)
                        {
                            listBtnSpell[i].image.sprite = listSpellIce[i];
                        }
                        else
                            listBtnSpell[i].gameObject.SetActive(false);
                    }
                    break;
            }
            //listBtnSpell[listBtnSpell.Count - 1].gameObject.SetActive(true);
        }
    }

    public IEnumerator ShowMessage(string msg, int delai)
    {
        //CombatTurn.anim = true;
        combatMessage.GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(delai);
        combatMessage.GetComponent<Text>().text = "";
        //CombatTurn.anim = false;
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

    public void Reset_BTN()
    {
        ReactivateButtons(listBtnEnemy);
        ReactivateButtons(listBtnSpell);
        ReactivateButtons(ListBtnItem);

        onClickCancel();
    }

    public void HideMenu()
    {
        pnlMenuBtn.SetActive(false);
    }

    public void ShowMenu()
    {
        pnlMenuBtn.SetActive(true);
    }

    public void ShowToolTipSpell(int idBtn)
    {
        Color cl = bgToolTipSpellCharacter[idBtn].color;
        cl.a = 255;
        bgToolTipSpellCharacter[idBtn].color = cl;
        toolTipSpellCharacter[idBtn].text = " " + currentPerso.sorts[idBtn].nom + " ," + currentPerso.sorts[idBtn].type + " ," + currentPerso.sorts[idBtn].nbattaque;
        
    }

    public void HideToolTipSpell(int idBtn)
    {
        Color cl = bgToolTipSpellCharacter[idBtn].color;
        cl.a = 0;
        bgToolTipSpellCharacter[idBtn].color = cl;
        toolTipSpellCharacter[idBtn].text = "";
    }

    public void ShowTooltipItem(int idBtn)
    {
        Color cl = bgtoolTipItem[idBtn].color;
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;
        string desc = "";
        cl.a = 255;
        bgtoolTipItem[idBtn].color = cl;
        switch(idBtn)
        {
            case 0:
                reader = bd.select("select Description from Item where idItem = 3");
                while(reader.Read())
                {
                    desc = reader.GetString(0);
                }
                toolTipItem[idBtn].text = desc;
                break;
            case 1:
                reader = bd.select("select Description from Item where idItem = 7");
                while (reader.Read())
                {
                    desc = reader.GetString(0);
                }
                toolTipItem[idBtn].text = desc;
                break;
            case 2:
                reader = bd.select("select Description from Item where idItem = 6");
                while (reader.Read())
                {
                    desc = reader.GetString(0);
                }
                toolTipItem[idBtn].text = desc;
                break;
            case 3:
                reader = bd.select("select Description from Item where idItem = 5");
                while (reader.Read())
                {
                    desc = reader.GetString(0);
                }
                toolTipItem[idBtn].text = desc;
                break;
        }
    }

    public void HideToolTipItem(int idBtn)
    {
        Color cl = bgtoolTipItem[idBtn].color;
        cl.a = 0;
        bgtoolTipItem[idBtn].color = cl;
        toolTipItem[idBtn].text = "";
    }
}

