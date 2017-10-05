using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public enum MenuStates { None, Main, Inventory, Team, Stats, Skill}
    MenuStates states;

    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject teamMenu;
    public GameObject statsMenu;
    public GameObject skillMenu;
    

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
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                break;

            case MenuStates.Main:
                pauseMenu.SetActive(true);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                break;

            case MenuStates.Inventory:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(true);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                break;

            case MenuStates.Team:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(true);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                break;

            case MenuStates.Stats:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(true);
                break;

            case MenuStates.Skill:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                statsMenu.SetActive(false);
                skillMenu.SetActive(true);
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

    public void Open_Stats()
    {
        states = MenuStates.Stats;
    }

    public void Retour_Menu()
    {
        states = MenuStates.Main;
    }

    public void Open_Skill()
    {
        states = MenuStates.Skill;
    }
}
