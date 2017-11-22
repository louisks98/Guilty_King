using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Script;

public class StartGame : MonoBehaviour {

    public GameObject Btn_Diff;

	public void LoadGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NewGame()
    {
        AccesBD bd = new AccesBD();
        bd.ClearBD();
        GameObject.Find("Container_Btn_Main").SetActive(false);
        Btn_Diff.SetActive(true);
        //SceneManager.LoadScene(sceneIndex);
    }

    public void chooseDiff(int i)
    {
        AccesBD bd = new AccesBD();
        switch(i)
        {
            case 0:
                bd.insert("Update Personnage set Niveau = 1 where idPersonnage = 1");
                SceneManager.LoadScene(1);
                break;
            case 1:
                bd.insert("Update Personnage set Niveau = 2 where idPersonnage = 1");
                SceneManager.LoadScene(1);
                break;
            case 2:
                bd.insert("Update Personnage set Niveau = 3 where idPersonnage = 1");
                SceneManager.LoadScene(1);
                break;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
