﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public enum MenuStates { None, Main, Inventory, Team}
    MenuStates states;

    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject teamMenu;

    

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        switch(states)
        {
            case MenuStates.None:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                break;

            case MenuStates.Main:
                pauseMenu.SetActive(true);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                break;

            case MenuStates.Inventory:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(true);
                teamMenu.SetActive(false);
                break;

            case MenuStates.Team:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(true);
                break;
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (states == MenuStates.Main)
                states = MenuStates.None;

            else if (states == MenuStates.None)
                states = MenuStates.Main;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Open_Inventory()
    {
        states = MenuStates.Inventory;
    }

    public void Open_Team()
    {
        states = MenuStates.Team;
    }

    public void Retour_Menu()
    {
        states = MenuStates.Main;
    }
}
