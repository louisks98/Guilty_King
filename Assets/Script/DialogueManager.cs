﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;
    public Text dText;

    public bool dialogActive;

    public string[] dialogLines;
    public int currentLine;

    GameObject ennemy;

    public bool inCombat;

	// Use this for initialization
	void Start () {
        dialogLines = new string[1]; // pour enlever l'exception mais inutile.
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogActive && Input.GetKeyUp(KeyCode.Space))
        {
            currentLine++;
        }
        if (currentLine >= dialogLines.Length)
        {
            dBox.SetActive(false);
            dialogActive = false;

            currentLine = 0;
            Time.timeScale = 1f;
            PlayerMovment.canMove = true;

            if (ennemy != null)
            {
                ennemy.GetComponent<CombatTurn>().currentState = CombatTurn.CombatStates.ANIMSTART;
            }
        }

        dText.text = dialogLines[currentLine];
    }

    public void ShowDialogue()
    {
        dialogActive = true;
        dBox.SetActive(true);
        PlayerMovment.canMove = false;
    }

    public void SetEnnemy(GameObject ennemy)
    {
        this.ennemy = ennemy;
    }
}
