using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class CombatTurn : MonoBehaviour {

   public enum CombatStates {
        START,
        ALLY1,
        ALLY2,
        ALLY3,
        ALLY4,
        ENEMY1,
        ENEMY2,
        ENEMY3,
        ENEMY4,
        LOSE,
        WIN,
        NOTINCOMBAT
    }

    public int id_enemy1;
    public int id_enemy2;
    public int id_enemy3;
    public int id_enemy4;

    public GameObject go_enemy1 = null;
    public GameObject go_enemy2 = null;
    public GameObject go_enemy3 = null;
    public GameObject go_enemy4 = null;

    List<Personnage> ennemies;
    List<Personnage> allies;

    public CombatStates currentState { get; set; }

    public Transform target_combat;
    public Transform target_win;
    public Transform target_loose;

    public GameObject hero;

    public GameObject combatUI;
    private GameObject pnlAlly;
    private GameObject pnlEnemy;
    private GameObject pnlButton;

    void Start () {
        currentState = CombatStates.NOTINCOMBAT;
    }

    void Update () {
        Debug.Log(currentState);
        //yield WaitForSeconds(1);
        switch (currentState)
        {
            case (CombatStates.START):
                Combat_Start();
                break;
            case (CombatStates.ALLY1):
                Combat_Ally1_Turn();
                break;
            case (CombatStates.ALLY2):
                Combat_Ally2_Turn();
                break;
            case (CombatStates.ALLY3):
                Combat_Ally3_Turn();
                break;
            case (CombatStates.ALLY4):
                Combat_Ally4_Turn();
                break;
            case (CombatStates.ENEMY1):
                Combat_Enemy1_Turn();
                break;
            case (CombatStates.ENEMY2):
                Combat_Enemy2_Turn();
                break;
            case (CombatStates.ENEMY3):
                Combat_Enemy3_Turn();
                break;
            case (CombatStates.ENEMY4):
                Combat_Enemy4_Turn();
                break;
            case (CombatStates.WIN):
                Combat_WIN();
                break;
            case (CombatStates.LOSE):
                Combat_Lose();
                break;
            case (CombatStates.NOTINCOMBAT):
                //On ne fait rien on est pas en combat
                break;
        }
	}

    void Combat_Start()
    {
        StartCoroutine(Animation_Start());
        Initialize_Component();
        Define_Turn();
    }

    IEnumerator Animation_Start()
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        yield return StartCoroutine(sf.FadeToBlack());

        //Positionner la caméra sur le combat
        CameraMovment.inCombat = true;
        CameraMovment.target_Combat = target_combat;

        //Empêcher le joueur de bouger
        PlayerMovment.inCombat = true;
        PlayerMovment.canMove = false;

        yield return StartCoroutine(sf.FadeToClear());

        //Le combat débute
        currentState = CombatStates.ENEMY1;
    }

    void Combat_Ally1_Turn()
    {
        combatUI.GetComponent<CombatUI>().AfficherSpells(CombatStates.ALLY1);
        Ally_Turn(0);
        Next_Turn(CombatStates.ALLY2);
    }

    void Combat_Ally2_Turn()
    {
        combatUI.GetComponent<CombatUI>().AfficherSpells(CombatStates.ALLY2);
        Ally_Turn(1);
        Next_Turn(CombatStates.ALLY3);
    }

    void Combat_Ally3_Turn()
    {
        combatUI.GetComponent<CombatUI>().AfficherSpells(CombatStates.ALLY3);
        Ally_Turn(2);
        Next_Turn(CombatStates.ALLY4);
    }

    void Combat_Ally4_Turn()
    {
        combatUI.GetComponent<CombatUI>().AfficherSpells(CombatStates.ALLY4);
        Ally_Turn(3);
        Next_Turn(CombatStates.ENEMY1);
    }

    void Combat_Enemy1_Turn()
    {
        Enemy_Turn(0);
        Next_Turn(CombatStates.ENEMY2);
    }

    void Combat_Enemy2_Turn()
    {
        Enemy_Turn(1);
        Next_Turn(CombatStates.ENEMY3);
    }

    void Combat_Enemy3_Turn()
    {
        Enemy_Turn(2);
        Next_Turn(CombatStates.ENEMY4);
    }

    void Combat_Enemy4_Turn()
    {
        Enemy_Turn(3);
        Next_Turn(CombatStates.ALLY1);
    }


    void Quit(Transform target)
    {
        //Repositionne la caméra sur le personnage.
        CameraMovment.target_Combat = hero.GetComponent<Transform>();

        //Hero retourne ou il doit etre après le combat.
        hero.transform.position = target_win.position;

        //Le personnage peut bouger
        CameraMovment.inCombat = false;
        PlayerMovment.canMove = true;

        //Le combet est terminer
        currentState = CombatStates.NOTINCOMBAT;

        combatUI.SetActive(false);
    }

    void Ally_Turn(int id)
    {
        //selectionner l'oposant à attaquer
        //sélectionner l'attaque
        //faire l'animation
        //faire de dégat
        //Victoire ? défaite ?
    }

    void Enemy_Turn(int id)
    {
        //Choisir un target aléatoire 
        //Choisir une ataque aléatoire 
        //faire l'animation
        //faire le dégat
        //Victoire ? défaite ?
    }

    void Combat_WIN()
    {
        Quit(target_win);
    }

    void Combat_Lose()
    {
        Quit(target_loose);
    }

    void Initialize_Component()
    {
        Init_Personnages();
        InitUI();
    }

    void Init_Personnages()
    {
        Init_Ally();
        Init_Ennemy();
    }

    void Init_Ally()
    {
        allies = new List<Personnage>();
        allies.Add(null);
        allies.Add(null);
        allies.Add(null);
        allies.Add(null);
        if (Personnage_Is_In_Team(1))
        {
            allies[0] = new Personnage(GameObject.FindGameObjectWithTag("HeroCombat"), 1);
        }
        if (Personnage_Is_In_Team(2))
        {
            allies[1]  = new Personnage(GameObject.FindGameObjectWithTag("ForestAlly"), 2);
        }
        if (Personnage_Is_In_Team(3))
        {
            allies[2] = new Personnage(GameObject.FindGameObjectWithTag("FireAlly"), 3);
        }
        if (Personnage_Is_In_Team(4))
        {
            allies[3] = new Personnage(GameObject.FindGameObjectWithTag("IceAlly"), 4);
        }
    }

    bool Personnage_Is_In_Team(int id_Personnage)
    {
        bool isKnown = false;
        AccesBD bd = new AccesBD();
        try
        {
            SqliteDataReader reader = bd.select("SELECT vaincue FROM Personnage where idPersonnage =" + id_Personnage);

            while (reader.Read())
            {
                string rep = reader.GetString(0).ToString();
                if (rep.Equals("O"))
                {
                    isKnown = true;
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.Log(e);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            bd.Close();
        }

        Debug.Log("Est dans la team:" + isKnown.ToString());
        return isKnown;
    }

    void Init_Ennemy()
    {
        ennemies = new List<Personnage>();
        ennemies.Add(null);
        ennemies.Add(null);
        ennemies.Add(null);
        ennemies.Add(null);
        if (go_enemy1 != null && id_enemy1 != 0)
        {
            ennemies[0] = new Personnage(go_enemy1, id_enemy1);
        }
        if (go_enemy2 != null && id_enemy2 != 0)
        {
            ennemies[1] = new Personnage(go_enemy2, id_enemy2);
        }
        if (go_enemy3 != null && id_enemy3 != 0)
        {
            ennemies[2] = new Personnage(go_enemy3, id_enemy3);
        }
        if (go_enemy4 != null && id_enemy4 != 0)
        {
            ennemies[3] = new Personnage(go_enemy4, id_enemy4);
        }
    }

    //Define_Turn
    //Détermine qu'elle équipe selon la ripidité qui commence.
    void Define_Turn()
    {
        if (Team_Speed(allies) >= Team_Speed(ennemies))
        {
            currentState = CombatStates.ALLY1;
        }
        else
        {
            currentState = CombatStates.ENEMY1;
        }        
    }

    int Team_Speed(List<Personnage> team)
    {
        int speed = 0;
        foreach (Personnage personnage in team)
        {
            if (personnage != null)
            {
                speed += personnage.speed;
            }
        }
        return speed;
    }

    void Next_Turn(CombatStates nextPlayer)
    {
        if (isWin())
        {
            currentState = CombatStates.WIN;
        }
        else if (isLoose())
        {
            currentState = CombatStates.LOSE;
        }
        else
        {
            currentState = nextPlayer;
        }
    }

    //IsLoose
    //Retourne true si la partie est perdue.
    bool isLoose()
    {
        return Team_Defeated(allies);
    }

    //IsWin
    //Retourne false si la partie est gagnée.
    bool isWin()
    {
        return Team_Defeated(ennemies);
    }

    bool Team_Defeated(List<Personnage> team)
    {
        bool defeted = true;
        foreach (Personnage personnage in team)
        {
            if (personnage != null)
            {
               // if (!personnage.defeated)
               // {
                    defeted = false;
               // }
            }
        }
        return defeted;
    }

    void InitUI()
    {
        CombatUI ui = combatUI.GetComponent<CombatUI>();
        SpriteRenderer sprite;
        try
        {
            if (go_enemy1 != null)
            {
                sprite = go_enemy1.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[0] = sprite.sprite;
                Debug.Log("enemy sprite 1");
            }
            if (go_enemy2 != null)
            {
                sprite = go_enemy2.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[1] = sprite.sprite;
                Debug.Log("enemy sprite 2");
            }
            if (go_enemy3 != null)
            {
                sprite = go_enemy3.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[2] = sprite.sprite;
                Debug.Log("enemy sprite 3");
            }
                
            if (go_enemy4 != null)
            {
                sprite = go_enemy4.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[3] = sprite.sprite;
                Debug.Log("enemy sprite 4");
            }
                
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        

        combatUI.SetActive(true);
        pnlAlly = GameObject.Find("PNL_TeamHp");
        pnlEnemy = GameObject.Find("PNL_Enemy");
        pnlButton = GameObject.Find("PNL_Button");
        List<Button> ListBtn = new List<Button>(pnlButton.GetComponentsInChildren<Button>());
        List<Text> hpTextAlly = new List<Text>(pnlAlly.GetComponentsInChildren<Text>());
        List<Text> hpTextEnemy = new List<Text>(pnlEnemy.GetComponentsInChildren<Text>());
        List<Slider> hpBarAlly = new List<Slider>(pnlAlly.GetComponentsInChildren<Slider>());
        List<Slider> hpBarEnemy = new List<Slider>(pnlEnemy.GetComponentsInChildren<Slider>());

        ListBtn[2].onClick.AddListener(QuitButton);
        for(int i = 0; i < hpTextAlly.Count; i++)
        {
            if(allies[i] != null)
            {
                hpTextAlly[i].text = allies[i].name + " : " + allies[i].BattleHp + "/" + allies[i].hpTotal;
                hpBarAlly[i].minValue = 0;
                hpBarAlly[i].maxValue = allies[i].hpTotal;
                hpBarAlly[i].value = allies[i].BattleHp;
            }
            else
            {
                hpTextAlly[i].gameObject.SetActive(false);
                hpBarAlly[i].gameObject.SetActive(false);
            }
            
        }
        for(int i = 0; i < hpTextEnemy.Count; i++)
        {
            if (ennemies[i] != null)
            {
                hpTextEnemy[i].text = ennemies[i].name;
                hpBarEnemy[i].minValue = 0;
                hpBarEnemy[i].maxValue = ennemies[i].hpTotal;
                hpBarEnemy[i].value = ennemies[i].BattleHp;
            }
            else
            {
                hpTextEnemy[i].gameObject.SetActive(false);
                hpBarEnemy[i].gameObject.SetActive(false);
            }
        }
        
    }

    public void QuitButton()
    {
        Quit(target_loose);
    }

    public class ExtendedBehavior : MonoBehaviour
    {
        public void Wait(float seconds)
        {
            StartCoroutine(_wait(seconds));
        }
        IEnumerator _wait(float time)
        {
            yield return new WaitForSeconds(time);
        }
    }


}

