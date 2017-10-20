using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

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

    public GameObject gm_enemy1 = null;
    public GameObject gm_enemy2 = null;
    public GameObject gm_enemy3 = null;
    public GameObject gm_enemy4 = null;

    List<Personnage> ennemies;
    List<Personnage> allies;


    public CombatStates currentState { get; set; }

	void Start () {
        //currentState = CombatStates.NOTINCOMBAT;
        currentState = CombatStates.START;
    }
	
	void Update () {
        //Debug.Log(currentState);
		switch(currentState)
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
                break;
            case (CombatStates.LOSE):
                break;
            case (CombatStates.NOTINCOMBAT):
                break;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState = CombatStates.START;
    }

    void Combat_Start()
    {
        Change_Camera_Position();
        Stop_Player_Movement();
        Initialize_Component();
        Define_Turn();
    }

    void Combat_Ally1_Turn()
    {
        Ally_Turn(0);
        Next_Turn(CombatStates.ALLY2);
    }

    void Combat_Ally2_Turn()
    {
        Ally_Turn(1);
        Next_Turn(CombatStates.ALLY3);
    }

    void Combat_Ally3_Turn()
    {
        Ally_Turn(2);
        Next_Turn(CombatStates.ALLY4);
    }

    void Combat_Ally4_Turn()
    {
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

    void Win()
    {
        //Animation de victoire 
        //Set les variable indiquant la victoire
        Quit();
    }

    void Loose()
    {
        //Animation de défaite
        //Reset les variables de combat point de vie etc
        //déplacer le personnage au début du jeu
        Quit();
    }

    void Quit()
    {
       //ferme le canva 
       //reperment au personnage de bouger 
       //remet la camera sur le personnage
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
        
    }

    void Combat_Lose()
    {

    }

    void Change_Camera_Position()
    {

    }

    void Stop_Player_Movement()
    {

    }

    void Initialize_Component()
    {
        Init_Personnages();
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
            allies.Insert(0, new Personnage(gm_enemy1, id_enemy1));
        }
        if (Personnage_Is_In_Team(2))
        {
            allies.Insert(1, new Personnage(gm_enemy1, id_enemy1));
        }
        if (Personnage_Is_In_Team(3))
        {
            allies.Insert(2, new Personnage(gm_enemy1, id_enemy1));
        }
        if (Personnage_Is_In_Team(4))
        {
            allies.Insert(3, new Personnage(gm_enemy1, id_enemy1));
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

        return isKnown;
    }

    void Init_Ennemy()
    {
        ennemies = new List<Personnage>();
        ennemies.Add(null);
        ennemies.Add(null);
        ennemies.Add(null);
        ennemies.Add(null);
        if (gm_enemy1 != null && id_enemy1 != 0)
        {
            ennemies.Insert(0,new Personnage(gm_enemy1, id_enemy1));
        }
        if (gm_enemy2 != null && id_enemy2 != 0)
        {
            ennemies.Insert(1, new Personnage(gm_enemy2, id_enemy2));
        }
        if (gm_enemy3 != null && id_enemy3 != 0)
        {
            ennemies.Insert(2, new Personnage(gm_enemy3, id_enemy3));
        }
        if (gm_enemy4 != null && id_enemy4 != 0)
        {
            ennemies.Insert(3, new Personnage(gm_enemy4, id_enemy4));
        }
    }
    
    //Regarder les speed des players pour savoir qui qui commence.
    void Define_Turn()
    {
        currentState = CombatStates.ALLY1;
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

    void isFinish()
    {
        if (isLoose())
        {
            currentState = CombatStates.LOSE;
        }
        else if(isWin())
        {
            currentState = CombatStates.WIN;
        }
    }

    bool isLoose()
    {
        //Regarder si la partie est perdue
        return false;
    }

    bool isWin()
    {
        //Regarder si la partie est gagner
        return false;
    }
}
