using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public enum MenuStates { None, Main, Inventory}
    MenuStates states;

    public GameObject pauseMenu;
    public GameObject inventoryMenu;

    

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
                break;

            case MenuStates.Main:
                pauseMenu.SetActive(true);
                inventoryMenu.SetActive(false);
                break;

            case MenuStates.Inventory:
                pauseMenu.SetActive(false);
                inventoryMenu.SetActive(true);
                break;
        }
        //if (isPaused)
        //{
        //    pauseMenu.SetActive(true);
        //    Time.timeScale = 0f;
        //}
        //else
        //{
        //    pauseMenu.SetActive(false);
        //    Time.timeScale = 1f;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (states == MenuStates.Main)
                states = MenuStates.None;

            if (states == MenuStates.None)
                states = MenuStates.Main;
        }

        //if(isInventaire)
        //{
        //    //pauseMenu.SetActive(false);
        //    inventaire.isOpen = true;
        //    isPaused = false;
        //    //inventaireMenu.SetActive(true);

        //}
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Open_Inventory()
    {
        states = MenuStates.Inventory;
    }

    public void Retour_Menu_Inventory()
    {
        states = MenuStates.Main;
    }
}
