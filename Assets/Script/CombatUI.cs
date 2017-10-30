﻿using System.Collections;
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

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void onClickAttack()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(true);
    }
    public void onClickCancel()
    {
        pnlEnemySelect.SetActive(false);
        pnlMenuBtn.SetActive(true);
        pnlAttakBtn.SetActive(false);
    }

    public void onClickSpell()
    {
        pnlEnemySelect.SetActive(true);
        pnlMenuBtn.SetActive(false);
        pnlAttakBtn.SetActive(false);
        AfficherEnemy();
    }

    public void AfficherEnemy()
    {
        List<Button> lstBtn = new List<Button>(pnlEnemySelect.GetComponentsInChildren<Button>());
        for (int i = 0; i < lstBtn.Count; i++)
        {
            if (listEnemySprites[i] != null)
            {
                lstBtn[i].image.sprite = listEnemySprites[i];
                Debug.Log("Afficher enemy sprite" + i);
            }
            else
                lstBtn[i].gameObject.SetActive(false);
        }
    }
        
    public void AfficherSpells(CombatTurn.CombatStates state)
    {
        List<Button> lstBtn = new List<Button>(pnlAttakBtn.GetComponentsInChildren<Button>());

        switch(state)
        {
            case CombatTurn.CombatStates.ALLY1:
                for (int i = 0; i < lstBtn.Count; i++)
                {
                    if (listSpellHero[i] != null)
                        lstBtn[i].image.sprite = listSpellHero[i];
                    else
                        lstBtn[i].gameObject.SetActive(false);
                }
                break;

            case CombatTurn.CombatStates.ALLY2:
                for (int i = 0; i < lstBtn.Count; i++)
                {
                    if (listSpellHero[i] != null)
                        lstBtn[i].image.sprite = listSpellFire[i];
                    else
                        lstBtn[i].gameObject.SetActive(false);
                }
                break;

            case CombatTurn.CombatStates.ALLY3:
                for (int i = 0; i < lstBtn.Count; i++)
                {
                    if (listSpellHero[i] != null)
                        lstBtn[i].image.sprite = listSpellEarth[i];
                    else
                        lstBtn[i].gameObject.SetActive(false);
                }
                break;

            case CombatTurn.CombatStates.ALLY4:
                for (int i = 0; i < lstBtn.Count; i++)
                {
                    if (listSpellHero[i] != null)
                        lstBtn[i].image.sprite = listSpellIce[i];
                    else
                        lstBtn[i].gameObject.SetActive(false);
                }
                break;
        }
    }
}
