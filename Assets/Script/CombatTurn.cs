using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject ally1;
    public GameObject ally2;
    public GameObject ally3;
    public GameObject ally4;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public string id_ally1;
    public string id_ally2;
    public string id_ally3;
    public string id_ally4;

    public string id_enemy1;
    public string id_enemy2;
    public string id_enemy3;
    public string id_enemy4;




    public CombatStates currentState { get; set; }

	// Use this for initialization
	void Start () {
        currentState = CombatStates.NOTINCOMBAT;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
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
        if (true) //Ally 1 exist
        {
            Ally_Turn(ally1,id_ally1);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ALLY2);
    }

    void Combat_Ally2_Turn()
    {
        if (true) //Ally 2 exist
        {
            Ally_Turn(ally2,id_ally2);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ALLY3);
    }

    void Combat_Ally3_Turn()
    {
        if (true) //Ally 3 exist
        {
            Ally_Turn(ally3,id_ally3);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ALLY4);
    }

    void Combat_Ally4_Turn()
    {
        if (true) //Ally 4 exist
        {
            Ally_Turn(ally4,id_ally4);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ENEMY1);
    }

    void Combat_Enemy1_Turn()
    {
        if (true) //Enemy 1 exist
        {
            Enemy_Turn(enemy1,id_enemy1);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ENEMY2);
    }

    void Combat_Enemy2_Turn()
    {
        if (true) //Enemy 2 exist
        {
            Enemy_Turn(enemy2,id_enemy2);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ENEMY3);
    }

    void Combat_Enemy3_Turn()
    {
        if (true) //Enemy 3 exist
        {
            Enemy_Turn(enemy3,id_enemy3);  //Est t'il passe en reférence ou en param ???   
        }
        Next_Turn(CombatStates.ENEMY4);
    }

    void Combat_Enemy4_Turn()
    {
        if (true) //Enemy 4 exist
        {
            Enemy_Turn(enemy4,id_enemy4);  //Est t'il passe en reférence ou en param ???   
        }
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

    void Ally_Turn(GameObject ally, string id_ally)
    {
        //selectionner l'oposant à attaquer
        //sélectionner l'attaque
        //faire l'animation
        //faire de dégat
    }

    void Enemy_Turn(GameObject enemy, string id_ennemy)
    {
        //Choisir un target aléatoire 
        //Choisir une ataque aléatoire 
        //faire l'animation
        //faire le dégat
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
