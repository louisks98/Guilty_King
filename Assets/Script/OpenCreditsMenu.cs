using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenCreditsMenu : MonoBehaviour {

    public bool AfficherMenu;
    private float speed = 50.0f;


    // Use this for initialization
    void Start () {
        AfficherMenu = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (AfficherMenu)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (gameObject.transform.position.y >= 2600.0f || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) )
        {
            AfficherMenu = false;
            SceneManager.LoadScene(0);
        }
    }
}
