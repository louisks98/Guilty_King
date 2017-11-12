using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class CombatTurn : MonoBehaviour
{

    public enum CombatStates
    {
        ANIMSTART,
        START,
        STARTATTACK,
        ANIMLEFT,
        ANIMRIGHT,
        ANIMATTACK,
        ATTACK,
        NEXTPLAYER,
        LOSE,
        WIN,
        ANIMEND,
        NOTINCOMBAT
    }
    public static bool selecting { get; set; }
    bool anim;
    public CombatStates currentState { get; set; }

    public bool currentTeamIsAlly { get; set; }
    public int currentPlayer { get; set; }

    public int id_enemy1 = 0;
    public int id_enemy2 = 0;
    public int id_enemy3 = 0;
    public int id_enemy4 = 0;

    public GameObject go_enemy1 = null;
    public GameObject go_enemy2 = null;
    public GameObject go_enemy3 = null;
    public GameObject go_enemy4 = null;

    List<Personnage> ennemies;
    List<Personnage> allies;

    public Transform target_combat;
    public Transform target_win;
    public Transform target_loose;

    public Transform target_Ally_1;
    public Transform target_Ally_2;
    public Transform target_Ally_3;
    public Transform target_Ally_4;
    public Transform target_Exile;

    public GameObject hero;

    public GameObject combatUI;
    private GameObject pnlAlly;
    private GameObject pnlEnemy;
    private GameObject pnlButton;

    private List<Button> ListBtn;
    private List<Text> hpTextAlly;
    private List<Text> hpTextEnemy;
    private List<Slider> hpBarAlly;
    private List<Slider> hpBarEnemy;

    System.Random random;

    public GameObject lvlMenu;

    public int soolsAfterWin;

    void Start()
    {
        currentState = CombatStates.NOTINCOMBAT;
        CombatTurn.selecting = false;
    }

    void Update()
    {
       
        if (!anim && !currentPlayerIsMoving() && !selecting)
        {
            Debug.Log(currentState);
            switch (currentState)
            {
                case (CombatStates.ANIMSTART):
                    //Empêcher le joueur de bouger
                    PlayerMovment.inCombat = true;
                    PlayerMovment.canMove = false;

                    StartCoroutine(Fade());
                    currentState = CombatStates.START;
                    break;
                case (CombatStates.START):
                    //Positionner la caméra sur le combat
                    CameraMovment.inCombat = true;
                    CameraMovment.target_Combat = target_combat;

                    Combat_Start();

                    currentState = CombatStates.STARTATTACK;
                    break;
                case (CombatStates.STARTATTACK):
                    if (currentTeamIsAlly)
                    {
                        if (allies[currentPlayer] != null)
                        {
                            Draw_Spell_And_Target();
                            CombatTurn.selecting = true;
                        }
                        currentState = CombatStates.ANIMLEFT;
                    }
                    else
                    {
                        currentState = CombatStates.ANIMRIGHT;
                    }
                    break;
                case (CombatStates.ANIMATTACK):
                    if (currentTeamIsAlly)
                    {
                        if (allies[currentPlayer] != null)
                        {
                            allies[currentPlayer].Attaque();
                        }
                    }
                    else
                    {
                        if (ennemies[currentPlayer] != null)
                        {
                            ennemies[currentPlayer].Attaque();
                        }
                    }
                    currentState = CombatStates.ATTACK;
                    break;
                case (CombatStates.ANIMLEFT):
                    if (currentTeamIsAlly)
                    {
                        if (allies[currentPlayer] != null)
                        {
                            allies[currentPlayer].MoveLeft();
                        }
                        currentState = CombatStates.ANIMATTACK;
                    }
                    else
                    {
                        if (ennemies[currentPlayer] != null)
                        {
                            ennemies[currentPlayer].MoveLeft();
                        }
                        currentState = CombatStates.NEXTPLAYER;
                    }
                    break;
                case (CombatStates.ANIMRIGHT):
                    if (currentTeamIsAlly)
                    {
                        if (allies[currentPlayer] != null)
                        {
                            allies[currentPlayer].MoveRight();
                        }
                        currentState = CombatStates.NEXTPLAYER;
                    }
                    else
                    {
                        if (ennemies[currentPlayer] != null)
                        {
                            ennemies[currentPlayer].MoveRight();
                        }
                        currentState = CombatStates.ANIMATTACK;
                    }
                    break;
                case (CombatStates.ATTACK):

                    if (currentTeamIsAlly)
                    {
                        if (allies[currentPlayer] != null)
                        {
                            if(combatUI.GetComponent<CombatUI>().selectedSpell != null)
                            {
                                if (combatUI.GetComponent<CombatUI>().selectedSpell.type == "AZ")
                                {
                                    if(combatUI.GetComponent<CombatUI>().selectedSpell.id == "H4")
                                    {
                                        allies[currentPlayer].dealDamage(-(GetDamage(allies[currentPlayer], combatUI.GetComponent<CombatUI>().selectedSpell.valeur)));
                                    }
                                    Attack_AOI(GetDamage(allies[currentPlayer], combatUI.GetComponent<CombatUI>().selectedSpell.valeur), ennemies);
                                }
                                else if (combatUI.GetComponent<CombatUI>().selectedSpell.type == "AS")
                                {
                                    Attack(GetDamage(allies[currentPlayer], combatUI.GetComponent<CombatUI>().selectedSpell.valeur), combatUI.GetComponent<CombatUI>().selectedEnemy);
                                }
                                else if (combatUI.GetComponent<CombatUI>().selectedSpell.type == "GR")
                                {
                                    Attack(combatUI.GetComponent<CombatUI>().selectedSpell.valeur, combatUI.GetComponent<CombatUI>().selectedEnemy);
                                }
                                else if (combatUI.GetComponent<CombatUI>().selectedSpell.type == "AD" || combatUI.GetComponent<CombatUI>().selectedSpell.type == "AF")
                                {
                                    Buff(combatUI.GetComponent<CombatUI>().selectedSpell.type, combatUI.GetComponent<CombatUI>().selectedSpell.valeur, combatUI.GetComponent<CombatUI>().selectedEnemy);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ennemies[currentPlayer] != null)
                        {
                            int randomNumber = random.Next(-1, ennemies[currentPlayer].sorts.Count); // Choisir un spell

                            if (ennemies[currentPlayer].sorts[randomNumber].type == "GR")
                            {
                                Attack(ennemies[currentPlayer].sorts[randomNumber].valeur, RandomPersonnage(ennemies));
                            }
                            else if(ennemies[currentPlayer].sorts[randomNumber].type == "AS")
                            {
                                Attack(GetDamage(ennemies[currentPlayer], ennemies[currentPlayer].sorts[randomNumber].valeur), RandomPersonnage(allies));
                            }
                            else if (ennemies[currentPlayer].sorts[randomNumber].type == "AZ")
                            {
                                Attack_AOI(GetDamage(ennemies[currentPlayer], ennemies[currentPlayer].sorts[randomNumber].valeur), allies);
                            }
                            else if (ennemies[currentPlayer].sorts[randomNumber].type == "AD" || ennemies[currentPlayer].sorts[randomNumber].type == "AF")
                            {
                                Buff(ennemies[currentPlayer].sorts[randomNumber].type, ennemies[currentPlayer].sorts[randomNumber].valeur, RandomPersonnage(ennemies));
                            }
                        }
                    }

                    combatUI.GetComponent<CombatUI>().HideMenu();
                    Clean_The_Board();
                    Update_Stats();

                    if (currentTeamIsAlly)
                    {
                        currentState = CombatStates.ANIMRIGHT;
                    }
                    else
                    {
                        currentState = CombatStates.ANIMLEFT;
                    }
                    break;
                case (CombatStates.NEXTPLAYER):
                    Next_Turn();
                    break;
                case (CombatStates.WIN):
                    Combat_WIN();
                    break;
                case (CombatStates.LOSE):
                    Combat_Lose();
                    break;
                case (CombatStates.ANIMEND):
                    //Le personnage peut bouger
                    PlayerMovment.inCombat = false;
                    PlayerMovment.canMove = true;
                    currentState = CombatStates.NOTINCOMBAT;
                    break;
                case (CombatStates.NOTINCOMBAT):
                    break;
            }
        }
    }

    void Combat_Start()
    {
        Initialize_Component();
        Define_Turn();
        combatUI.GetComponent<CombatUI>().Start_Init_UI();
    }

    IEnumerator Fade()
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        anim = true;
        yield return StartCoroutine(sf.FadeToBlack());
        anim = false;

        yield return StartCoroutine(sf.FadeToClear());
    }

    void Quit(Transform target)
    {
        combatUI.SetActive(false);

        //Hero retourne ou il doit etre après le combat.
        hero.transform.position = target.position;

        //Repositionne la caméra sur le personnage.
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = target.position;

        //Camera retourne sur le personnage
        CameraMovment.inCombat = false;
        CameraMovment.target_Combat = null;

        CombatTurn.selecting = false;

        for (int i = 0; i < hpTextAlly.Count; i++)
        {
            hpTextAlly[i].gameObject.SetActive(true);
            hpBarAlly[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < hpTextEnemy.Count; i++)
        {
            hpTextEnemy[i].gameObject.SetActive(true);
            hpBarEnemy[i].gameObject.SetActive(true);
        }

        combatUI.GetComponent<CombatUI>().Reset_BTN();

        currentState = CombatStates.ANIMEND;
    }

    void Combat_WIN()
    {
        string sql = "UPDATE Personnage SET vaincue = 'O' where idPersonnage = ";

        if (id_enemy1 != 0)
        {
            AccesBD bd = new AccesBD();
            bd.insert(sql + id_enemy1);
            bd.Close();
        }
        if (id_enemy2 != 0)
        {
            AccesBD bd = new AccesBD();
            bd.insert(sql + id_enemy2);
            bd.Close();
        }
        if (id_enemy3 != 0)
        {
            AccesBD bd = new AccesBD();
            bd.insert(sql + id_enemy3);
            bd.Close();
        }
        if (id_enemy4 != 0)
        {
            AccesBD bd = new AccesBD();
            bd.insert(sql + id_enemy4);
            bd.Close();
        }

        Quit(target_win);
        LevelUp LvlMenu = GameObject.FindGameObjectWithTag("Hero").GetComponent<LevelUp>();
        LvlMenu.addSouls(soolsAfterWin);
        LvlMenu.UpdateUI();
        lvlMenu.GetComponent<PauseMenu>().Open_LevelUp();
    }

    void Combat_Lose()
    {
        Quit(target_loose);
    }

    void Initialize_Component()
    {
        random = new System.Random();
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

        CombatUI ui = combatUI.GetComponent<CombatUI>();

        ui.listAllySprites = new List<Sprite>();
        ui.listAllySprites.Add(null);
        ui.listAllySprites.Add(null);
        ui.listAllySprites.Add(null);
        ui.listAllySprites.Add(null);

        SpriteRenderer sprite;

        if (Personnage_Is_In_Team(1) && target_Ally_1 != null)
        {
            allies[0] = new Personnage(GameObject.FindGameObjectWithTag("HeroCombat"), 1);
            allies[0].gameObject.GetComponent<Rigidbody2D>().position = target_Ally_1.position;
            allies[0].deplacement.Init_Position();
            sprite = allies[0].gameObject.GetComponent<SpriteRenderer>();
            ui.listAllySprites[0] = sprite.sprite;
            allies[0].deplacement.isDying = false;
        }
        if (Personnage_Is_In_Team(2) && target_Ally_2 != null)
        {
            allies[1] = new Personnage(GameObject.FindGameObjectWithTag("ForestAlly"), 2);
            allies[1].gameObject.GetComponent<Rigidbody2D>().position = target_Ally_2.position;
            allies[1].deplacement.Init_Position();
            sprite = allies[1].gameObject.GetComponent<SpriteRenderer>();
            ui.listAllySprites[1] = sprite.sprite;
            allies[1].deplacement.isDying = false;
        }
        if (Personnage_Is_In_Team(3) && target_Ally_3 != null)
        {
            allies[2] = new Personnage(GameObject.FindGameObjectWithTag("FireAlly"), 3);
            allies[2].gameObject.GetComponent<Rigidbody2D>().position = target_Ally_3.position;
            allies[2].deplacement.Init_Position();
            sprite = allies[2].gameObject.GetComponent<SpriteRenderer>();
            ui.listAllySprites[2] = sprite.sprite;
            allies[2].deplacement.isDying = false;
        }
        if (Personnage_Is_In_Team(4) && target_Ally_4 != null)
        {
            allies[3] = new Personnage(GameObject.FindGameObjectWithTag("IceAlly"), 4);
            allies[3].gameObject.GetComponent<Rigidbody2D>().position = target_Ally_4.position;
            allies[3].deplacement.Init_Position();
            sprite = allies[3].gameObject.GetComponent<SpriteRenderer>();
            ui.listAllySprites[3] = sprite.sprite;
            allies[3].deplacement.isDying = false;
        }

        ui.listAllies = allies;
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
            bd.Close();
        }
        catch (SqliteException e)
        {
            bd.Close();
            Debug.Log(e);
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
            ennemies[0].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            ennemies[0].deplacement.isDying = false;
        }
        if (go_enemy2 != null && id_enemy2 != 0)
        {
            ennemies[1] = new Personnage(go_enemy2, id_enemy2);
            ennemies[1].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            ennemies[1].deplacement.isDying = false;
        }
        if (go_enemy3 != null && id_enemy3 != 0)
        {
            ennemies[2] = new Personnage(go_enemy3, id_enemy3);
            ennemies[2].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            ennemies[2].deplacement.isDying = false;
        }
        if (go_enemy4 != null && id_enemy4 != 0)
        {
            ennemies[3] = new Personnage(go_enemy4, id_enemy4);
            ennemies[3].gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            ennemies[3].deplacement.isDying = false;
        }
    }

    //Define_Turn
    //Détermine qu'elle équipe selon la ripidité qui commence.
    void Define_Turn()
    {
        if (Team_Speed(allies) >= Team_Speed(ennemies))
        {
            currentTeamIsAlly = true;
            currentPlayer = 0;
        }
        else
        {
            combatUI.GetComponent<CombatUI>().HideMenu();
            currentTeamIsAlly = false;
            currentPlayer = 0;
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

    void Next_Turn()
    {
        if (currentPlayer < 3)
        {
            currentPlayer++;
        }
        else
        {
            currentPlayer = 0;
            currentTeamIsAlly = !currentTeamIsAlly;
        }

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
            currentState = CombatStates.STARTATTACK;
        }
        Debug.Log(currentTeamIsAlly.ToString() + currentPlayer);
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
                if (!personnage.defeated)
                {
                    defeted = false;
                }
            }
        }
        return defeted;
    }

    void InitUI()
    {
        combatUI.SetActive(true);
        pnlAlly = GameObject.Find("PNL_TeamHp");
        pnlEnemy = GameObject.Find("PNL_Enemy");
        pnlButton = GameObject.Find("PNL_Button");
        ListBtn = new List<Button>(pnlButton.GetComponentsInChildren<Button>());
        hpTextAlly = new List<Text>(pnlAlly.GetComponentsInChildren<Text>());
        hpTextEnemy = new List<Text>(pnlEnemy.GetComponentsInChildren<Text>());
        hpBarAlly = new List<Slider>(pnlAlly.GetComponentsInChildren<Slider>());
        hpBarEnemy = new List<Slider>(pnlEnemy.GetComponentsInChildren<Slider>());

        ListBtn[2].onClick.AddListener(QuitButton);

        Update_Stats();
    }

    void Draw_Spell_And_Target()
    {
        CombatUI ui = combatUI.GetComponent<CombatUI>();
        SpriteRenderer sprite;
        combatUI.GetComponent<CombatUI>().ShowMenu();
        ui.AfficherSpells(allies[currentPlayer]);

        try
        {
            ui.listEnemySprites = new List<Sprite>();
            ui.listEnemySprites.Add(null);
            ui.listEnemySprites.Add(null);
            ui.listEnemySprites.Add(null);
            ui.listEnemySprites.Add(null);

            ui.listEnnemies = ennemies;

            if (go_enemy1 != null)
            {
                sprite = go_enemy1.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[0] = sprite.sprite;
            }
            if (go_enemy2 != null)
            {
                sprite = go_enemy2.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[1] = sprite.sprite;
            }
            if (go_enemy3 != null)
            {
                sprite = go_enemy3.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[2] = sprite.sprite;
            }

            if (go_enemy4 != null)
            {
                sprite = go_enemy4.GetComponent<SpriteRenderer>();
                ui.listEnemySprites[3] = sprite.sprite;
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void Update_Stats()
    {
        for (int i = 0; i < hpTextAlly.Count; i++)
        {
            if (allies[i] != null)
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
        for (int i = 0; i < hpTextEnemy.Count; i++)
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
        Quit(target_win);
        //if (Escape())
        //{
        //    Quit(target_win);
        //}
        //else
        //{
        //    //combatUI.GetComponent<CombatUI>().closeMenu();
        //    //StartCoroutine(combatUI.GetComponent<CombatUI>().ShowMessage("Vous ne m'échapperez pas !!!", 1));
        //    currentState = CombatStates.NEXTPLAYER;
        //}
    }

    bool currentPlayerIsMoving()
    {
        bool moving = false;
        if (allies != null && ennemies != null)
        {
            if (currentTeamIsAlly)
            {
                if (allies[currentPlayer] != null)
                {
                    if (allies[currentPlayer].deplacement.anim.GetBool("iswalking") || allies[currentPlayer].deplacement.anim.GetBool("isAttacking") || allies[currentPlayer].deplacement.anim.GetBool("isDying"))
                    {
                        moving = true;
                    }
                }
            }
            else
            {
                if (ennemies[currentPlayer] != null)
                {
                    if (ennemies[currentPlayer].deplacement.anim.GetBool("iswalking") || ennemies[currentPlayer].deplacement.anim.GetBool("isAttacking") || ennemies[currentPlayer].deplacement.anim.GetBool("isDying"))
                    {
                        moving = true;
                    }
                }
            }
        }
        return moving;
    }

    void Attack(int damage, int idPersonnage)
    {
        foreach (Personnage perso in allies)
        {
            if (perso != null)
            {
                if (perso.id == idPersonnage)
                {
                    perso.dealDamage(-(damage));
                }
            }
        }

        foreach (Personnage perso in ennemies)
        {
            if (perso != null)
            {
                if (perso.id == idPersonnage)
                {
                    perso.dealDamage(-(damage));
                }
            }
        }
    }

    void Attack_AOI(int damage,List<Personnage> personnages)
    {
        foreach (Personnage perso in personnages)
        {
            if (perso != null)
            {
                perso.dealDamage(-(damage));
            }
        }
    }

    void Buff(string type, int valeur, int idPerso)
    {
        Personnage personnage = null;

        foreach (Personnage perso in allies)
        {
            if (perso != null)
            {
                if (perso.id == idPerso)
                {
                    personnage = perso;
                }
            }
        }

        foreach (Personnage perso in ennemies)
        {
            if (perso != null)
            {
                if (perso.id == idPerso)
                {
                    personnage = perso;
                }
            }
        }

        if(personnage != null)
        {
            if (type == "AD")
            {
                int temp = personnage.BattleDef;
                personnage.BattleDef += valeur;
                int bost = personnage.BattleDef - temp;
                if (bost == 0)
                {
                    StartCoroutine(combatUI.GetComponent<CombatUI>().ShowMessage(personnage.name + ": Défense maximale", 2));
                }
                else
                {
                    StartCoroutine(combatUI.GetComponent<CombatUI>().ShowMessage(personnage.name + ": Défense + " + (personnage.BattleDef - temp).ToString(), 2));
                }
            }
            if (type == "AF")
            {
                int temp = personnage.BattleStr;
                personnage.BattleStr += valeur;
                int bost = personnage.BattleStr - temp;
                if (bost == 0)
                {
                    StartCoroutine(combatUI.GetComponent<CombatUI>().ShowMessage(personnage.name + ": Force maximale", 2));
                }
                else
                {
                    StartCoroutine(combatUI.GetComponent<CombatUI>().ShowMessage(personnage.name + ": Force + " + (personnage.BattleStr - temp).ToString(), 2));
                }
            }
        }
    }

    void Clean_The_Board()
    {
        for (int i = 0; i < allies.Count; i++)
        {
            if (allies[i] != null)
            {
                if (allies[i].defeated)
                {
                    allies[i].Die();
                    allies[i] = null;
                }
            }
        }

        for (int i = 0; i < ennemies.Count; i++)
        {
            if (ennemies[i] != null)
            {
                if (ennemies[i].defeated)
                {
                    ennemies[i].Die();
                    ennemies[i] = null;
                }
            }
        }
    }

    bool Escape()
    {
        bool escape = false;

        if ((Team_Speed(allies) + random.Next(1, 100)) >= (Team_Speed(ennemies) + random.Next(1, 100)))
        {
            escape = true;
        }

        return escape;
    }

    int RandomPersonnage(List<Personnage> listPerso)
    {
        int id = random.Next(-1, listPerso.Count);
        while (listPerso[id] == null)
        {
            id = random.Next(-1, 4);
        }
        return listPerso[id].id;
    }

    int GetDamage(Personnage perso, int baseDamage)
    {
        int damage = baseDamage;

        if(perso.strength > 0)
        {
            damage += (baseDamage * perso.strength / 100);
        }

        return damage;   
    }
}