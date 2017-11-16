using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Script;

public class StartGame : MonoBehaviour {

	public void LoadGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NewGame(int sceneIndex)
    {
        AccesBD bd = new AccesBD();
        bd.ClearBD();
        SceneManager.LoadScene(sceneIndex);
    }
}
