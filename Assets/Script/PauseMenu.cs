using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    

    private MenuInventaire script;

    // Use this for initialization
    void Start() {
        SoundManager.instance.musicSource.Play();
        script = inventoryMenu.GetComponent<MenuInventaire>();
    }

    // Update is called once per frame
    void Update() {
        switch(states)
        {
            case MenuStates.None:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = false;
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                if (paused)
                {
                    Time.timeScale = 1f;
                    paused = false;

                }
                break;

            case MenuStates.Main:
                pauseMenu.SetActive(true);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = false;
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                levelMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Inventory:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(true);
                //script.Afficher();
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = true;
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Team:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = false;
                teamMenu.SetActive(true);
                skillMenu.SetActive(false);
                statsMenu.SetActive(false);
                paused = true;
                break;

            case MenuStates.Stats:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = true;
                teamMenu.SetActive(false);
                skillMenu.SetActive(false);
                statsMenu.SetActive(true);
                paused = true;
                break;

            case MenuStates.Skill:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = false;
                teamMenu.SetActive(false);
                statsMenu.SetActive(false);
                skillMenu.SetActive(true);
                paused = true;
                break;

            case MenuStates.Level:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(false);
                //inventoryMenu.GetComponent<MenuInventaire>().enabled = false;
                teamMenu.SetActive(false);
                statsMenu.SetActive(false);
                skillMenu.SetActive(false);
                levelMenu.SetActive(true);
                paused = true;
                break;
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("Hero").GetComponent<LevelUp>().exitLevelMenu();
            if (states == MenuStates.Main)
            {
                states = MenuStates.None;
            }
            else if(states != MenuStates.Main)
            {
                states = MenuStates.Main;
            }
        }
    }

    public void Quit()
    {
        SoundManager.instance.musicSource.Stop();
        SceneManager.LoadScene(0);
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
