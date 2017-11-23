using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{

    public string dialogue;
    private DialogueManager dMan;
    public string[] dialogueLines;
    public GameObject ennemy;
    bool inCombat;
    public bool instantTalk;
    public bool hasBeenTalked;



    // Use this for initialization
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
        inCombat = false;
        hasBeenTalked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ennemy != null)
        {
            if (ennemy.GetComponent<CombatTurn>() != null)
            {
                if (ennemy.GetComponent<CombatTurn>().currentState == CombatTurn.CombatStates.NOTINCOMBAT)
                {
                    inCombat = false;
                }
                else
                {
                    inCombat = true;
                }
            }
        }

        if (!inCombat)
        {
            if (collision.gameObject.name == "Hero" || collision.gameObject.name == "Boat")
            {
                if (!instantTalk)
                {
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        if (!dMan.dialogActive)
                        {
                            if (ennemy != null)
                            {
                                dMan.SetEnnemy(ennemy);
                            }
                            else
                            {
                                dMan.SetEnnemy(null);
                            }
                            dMan.dialogLines = dialogueLines;
                            dMan.currentLine = -1;
                            dMan.ShowDialogue();
                        }
                    }
                }
                else
                {
                    if (!hasBeenTalked)
                    {
                        if (!dMan.dialogActive)
                        {
                            if (ennemy != null)
                            {
                                dMan.SetEnnemy(ennemy);
                            }
                            else
                            {
                                dMan.SetEnnemy(null);
                            }
                            dMan.dialogLines = dialogueLines;
                            dMan.currentLine = 0;
                            dMan.ShowDialogue();
                            hasBeenTalked = true;
                        }
                    }
                }

                //     CECI SERA INTÉRESSANT POUR EMPECHER DE BOUGER UN OBJET(MERCENAIRE?) LORSQU'ON DISCUTE AVEC LUI.    
                //         if (transform.parent.GetComponent<ObjectMovement>() != null)
                //          {
                //              transform.parent.GetComponent<ObjectMovement>().canMove = false;
                //          }
                //    IL FAUDRAIT PROGRAMMER UN SCRIPT OBJECTMOVEMENT ET FAIRE LA PROG DE CELUI-CI https://www.youtube.com/watch?v=KWNzLT46w9Q
            }
        }
    }
}
