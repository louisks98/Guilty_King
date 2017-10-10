using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public enum MenuStates { None, Main, Inventory, Team, Stats, Skill, Level}
    private MenuStates states;
    

    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject teamMenu;
    public GameObject statsMenu;
    public GameObject skillMenu;
    public GameObject levelMenu;

    public static bool paused;
    

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
                Debug.Log("TimeScale 1");
                if (paused)
                {
                    Time.timeScale = 1f;
                    paused = false;

                }
                break;

            case MenuStates.Main:
                pauseMenu.SetActive(true);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                levelMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Inventory:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(true);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Team:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(true);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Stats:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(true);
                paused = true;
                break;

            case MenuStates.Skill:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                statsMenu.SetActive(false);
                skillMenu.SetActive(true);
                paused = true;
                break;

            case MenuStates.Level:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                teamMenu.SetActive(false);
                statsMenu.SetActive(false);
                skillMenu.SetActive(false);
                levelMenu.SetActive(true);
                paused = true;
                break;
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (states == MenuStates.Main)
                states = MenuStates.None;

            else if (states != MenuStates.Main)
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
        SpellMenu.update = true;
    }

    public void Open_LevelUp()
    {
        states = MenuStates.Level;
    }
}
