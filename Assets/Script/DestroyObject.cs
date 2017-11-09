using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour {

    public GameObject theEnemy;
    public GameObject Menus;
    public int soulsPerKill;
    private PlayerMovment thePlayer;
    private LevelUp LvlMenu;
    PauseMenu theMenus;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerMovment>();
        theMenus = Menus.GetComponent<PauseMenu>();
        LvlMenu = FindObjectOfType<LevelUp>();

        //LvlMenu.addSouls(10);
        LvlMenu.UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(theEnemy);
            LvlMenu.addSouls(soulsPerKill);
            LvlMenu.UpdateUI();
            theMenus.Open_LevelUp();
        }
    }


}
