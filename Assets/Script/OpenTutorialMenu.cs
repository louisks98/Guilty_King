using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutorialMenu : MonoBehaviour {

    public GameObject leCanvas;
    public static bool AfficherMenu;

    // Use this for initialization
    void Start () {
        AfficherMenu = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (AfficherMenu)
        {
            leCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            AfficherMenu = false;
            leCanvas.SetActive(false);
        }
    }
}
