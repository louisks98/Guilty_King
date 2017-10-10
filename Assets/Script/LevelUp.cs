using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LevelUp : MonoBehaviour {

    public GameObject Menu;

    public Text textRemainingSouls;

    public int souls;
    public Text soulsRemaining;

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



public class LevelUp : MonoBehaviour
{
    public GameObject Menu;

    public Text textRemainingSouls;
    public Button buttonForce;
    public Button buttonDef;
    public Button buttonSpeed;
    public Button exitButton;

    private int forceValue;
    private int DefenseValue;
    private int SpeedValue;
    private int soulsNumber;

    private bool bdModifier = false;

    // Use this for initialization
    void Start()
    {
        AccesBD bd = new AccesBD();

        try
        {  
            IDataReader reader;
            reader = bd.select("select * from Stats where Stats.idStats = 2"); // *voir quelle personnage sera le main*

            while (reader.Read())
            {
                forceValue = reader.GetInt32(2);
                DefenseValue = reader.GetInt32(3);
                SpeedValue = reader.GetInt32(4);
                soulsNumber = reader.GetInt32(5);
            }

            UpdateUI();

            buttonForce.onClick.AddListener(onForceClick);
            buttonDef.onClick.AddListener(onDefenseClick);
            buttonSpeed.onClick.AddListener(onSpeedClick);
            exitButton.onClick.AddListener(exitLevelMenu);
        }

        catch (SqliteException e)
        {
            bd.Close();
            Debug.Log(e);
        }
        finally
        {
            bd.Close();
            Debug.Log("bd fermer");
        }
    public void UpdateUI()
    {
        textRemainingSouls.text = soulsNumber.ToString();

        sliderForce.value = forceValue;
        sliderDef.value = DefenseValue;
        sliderSpeed.value = SpeedValue;
    }

    public void onForceClick()
        if (forceValue < 600 && soulsNumber > 0)
        {
            bdModifier = true;
            forceValue += 100;
            soulsNumber--;
            UpdateUI();
        }

    private void onDefenseClick()
    {
        if (DefenseValue < 600 && soulsNumber > 0)
        {
            bdModifier = true;
            DefenseValue += 100;
            soulsNumber--;
            UpdateUI();
        }
    }

    private void onSpeedClick()
    {
        if (SpeedValue < 600 && soulsNumber > 0)
        {
            bdModifier = true;
            SpeedValue += 100;
            soulsNumber--;
            UpdateUI();
        }
    }

    // voir si le escape met a jour la bd IMPORTANT
    private void exitLevelMenu()
    {
        if (bdModifier)
        {
            AccesBD bd = new AccesBD();

            try
            {
                string query = "update Stats Set Force = " + forceValue + ", Defence = " + DefenseValue + ", Vitesse = " + SpeedValue + ", nbAmes = " + soulsNumber + " where Stats.idStats = 2";
                bd.insert(query);
            }
            catch (SqliteException e)
            {
                bd.Close();
                Debug.Log(e);
                bdModifier = false;
            }
            finally
            {
                bd.Close();
                Debug.Log("bd fermer");
                bdModifier = false;
            }
        }
    }

    public void addSouls(int theSouls)
    {
        soulsNumber += theSouls;
    }