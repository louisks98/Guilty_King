using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Assets.Script;



public class LevelUp : MonoBehaviour {

    public GameObject Menu;

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

        AccesBD bd = new AccesBD();
        IDataReader reader;
        reader = bd.select("select * from Stats where Stats.idStats = 1;");

        while (reader.Read())
        {
            forceValue = reader.GetInt32(2);
            DefenseValue = reader.GetInt32(3);
            SpeedValue = reader.GetInt32(4);
            soulsNumber = reader.GetInt32(5);
        }

        // Get de la bd
        //forceValue = 2;
        //DefenseValue = 3;
        //SpeedValue = 1;
        //soulsNumber = 2;
        UpdateUI();

        buttonForce.onClick.AddListener(onForceClick);
        buttonDef.onClick.AddListener(onDefenseClick);
        buttonSpeed.onClick.AddListener(onSpeedClick);
        exitButton.onClick.AddListener(exitLevelMenu);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void UpdateUI()
    {
        textRemainingSouls.text = soulsNumber.ToString();

        sliderForce.value = forceValue;
        sliderDef.value = DefenseValue;
        sliderSpeed.value = SpeedValue;
    }

    public void onForceClick()
    {
        if (forceValue < 600 && soulsNumber > 0)
        {
            forceValue += 100;
            soulsNumber--;
            UpdateUI();
        }
    }

    private void onDefenseClick()
    {
        if (DefenseValue < 600 && soulsNumber > 0)
        {
            DefenseValue += 100;
            soulsNumber--;
            UpdateUI();
        }
    }

    private void onSpeedClick()
    {
        if (SpeedValue < 600 && soulsNumber > 0)
        {
            SpeedValue += 100;
            soulsNumber--;
            UpdateUI();
        }
    }

    public void OpenLevelMenu()
    {
        Menu.SetActive(true);
    }

    private void exitLevelMenu()
    {
        Menu.SetActive(false);
        // Mettre à jour la bd...
        // Fermer la bd.
    }

    public void addSouls(int theSouls)
    {
        soulsNumber += theSouls;
    }
}
