using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterFight : MonoBehaviour {

    public GameObject leCanvas;

    public Text gameResult;

    public Text nbSouls;

    public Text newSort;

    public Text details;

    public bool AfficherMenu;
    private bool estAfficher;

    public bool lastFight;

	// Use this for initialization
	void Start () {
        AfficherMenu = false;
        estAfficher = false;
        lastFight = false;
	}

    // Update is called once per frame
    void Update() {
        if (AfficherMenu)
        {
            if (!estAfficher)
            {
                leCanvas.SetActive(true);
                estAfficher = true;
                PlayerMovment.inCombat = true;
                PlayerMovment.canMove = false;

            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {

                AfficherMenu = false;
                estAfficher = false;
                leCanvas.SetActive(false);
                PlayerMovment.inCombat = false;
                PlayerMovment.canMove = true;
                if (lastFight)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
      
    }
    public void SetSoulsText(string leText)
    {
        nbSouls.text = leText;
    }
    public void SetSortText(string leText)
    {
        newSort.text = leText;
    }
    public void SetDetailsText(string leText)
    {
        details.text = leText;
    }
    public void SetGameResultText(string leText)
    {
        gameResult.text = leText;
    }
}
