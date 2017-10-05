using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour {

    public GameObject theEnemy;
    public GameObject Menus;
    public int soulsPerKill;
    private PlayerMovment thePlayer;
    PauseMenu theMenus;

    public Text textRemainingSouls;

    public Slider sliderForce;
    public Slider sliderDef;
    public Slider sliderSpeed;

    public Button buttonForce;
    public Button buttonDef;
    public Button buttonSpeed;
    public Button exitButton;

    private int forceValue;
    private int DefenseValue;
    private int SpeedValue;
    private int soulsNumber;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerMovment>();
        theMenus = Menus.GetComponent<PauseMenu>();

        // Get de la bd
        forceValue = 2;
        DefenseValue = 3;
        SpeedValue = 1;
        soulsNumber = 2;
        textRemainingSouls.text = soulsNumber.ToString();
        InitialiserLevels();

        buttonForce.onClick.AddListener(onForceClick);
        buttonDef.onClick.AddListener(onDefenseClick);
        buttonSpeed.onClick.AddListener(onSpeedClick);
        //exitButton.onClick.AddListener(exitLevelMenu);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        buttonForce.onClick.AddListener(onForceClick);
        buttonDef.onClick.AddListener(onDefenseClick);
        buttonSpeed.onClick.AddListener(onSpeedClick);
        if (Input.GetKey(KeyCode.Space))
        {
            Destroy(theEnemy);
            soulsNumber += soulsPerKill;
            textRemainingSouls.text = soulsNumber.ToString();
            InitialiserLevels();
            theMenus.Open_LevelUp();
        }
    }

    private void InitialiserLevels()
    {
        sliderForce.value = forceValue;
        sliderDef.value = DefenseValue;
        sliderSpeed.value = SpeedValue;
    }

    private void onForceClick()
    {
        if (forceValue < 6 && soulsNumber > 0) 
        {
            forceValue++;
            soulsNumber--;
            textRemainingSouls.text = soulsNumber.ToString();
            InitialiserLevels();
        }
    }

    private void onDefenseClick()
    {
        if (DefenseValue < 6 && soulsNumber > 0) 
        {
            DefenseValue++;
            soulsNumber--;
            textRemainingSouls.text = soulsNumber.ToString();
            InitialiserLevels();
        }
    }

    private void onSpeedClick() 
    {
        if (SpeedValue < 6 && soulsNumber > 0)
        {
            SpeedValue++;
            soulsNumber--;
            textRemainingSouls.text = soulsNumber.ToString();
            InitialiserLevels();
        }
    }

    //private void exitLevelMenu()
    //{
    //    LvlMenu.SetActive(false);
    //    // Mettre à jour la bd...
    //    // Fermer la bd.
    //}
}
