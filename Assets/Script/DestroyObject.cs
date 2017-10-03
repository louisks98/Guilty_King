using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public GameObject theEnemy;
    public GameObject LvlMenu;
    public int soulsPerKill;
    private PlayerMovment thePlayer;

    // Use this for initialization
    void Start () {
        soulsPerKill = 0;
        thePlayer = FindObjectOfType<PlayerMovment>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(theEnemy);
            LvlMenu.SetActive(true);
  //          thePlayer.canMove = false;
        }
    }
}
