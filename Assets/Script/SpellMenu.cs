using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenu : MonoBehaviour {

    public bool ispaused;
    public GameObject spellMenuCanva;
    public Text spellName;
    public Text spellType;
    public Text spellDescription;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (ispaused)
        {
            spellMenuCanva.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            spellMenuCanva.SetActive(false);
            Time.timeScale = 1f;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ispaused = !ispaused;
        }
    }

    public void Quit()
    {
        ispaused = false;   
    }

    public void Open()
    {
        ispaused = true;
    }


    public void DrawSpellInfo(string name)
    {
        string spelldesc = "";

        spellName.text = name;

        try
        {
            string conn = "URI=file:" + Application.dataPath + "/BD.db"; //Path to database.
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "select Description from sort where Nom = '"+ name +"'";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                spelldesc = reader.GetString(0);
                Debug.Log("Spell description= " + spellDescription);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;

            //if flag = n sort inconnu
            spellName.text = name;
            spellDescription.text = spelldesc;
        }
        catch (Exception e)
        {
            spellName.text = "Oups! Le sort à été volé";
            spellDescription.text = "Revenez plus tard il y sera peutêtre :)"; 
        }
    }

    public void hero_spell_1()
    {
        DrawSpellInfo("Spell hero 1");
    }

    public void hero_spell_2()
    {
        DrawSpellInfo("Spell hero 2");
    }

    public void hero_spell_3()
    {
        DrawSpellInfo("Spell hero 3");
    }

    public void hero_spell_4()
    {
        DrawSpellInfo("Spell hero 4");
    }

    public void fire_spell_1()
    {
        DrawSpellInfo("Fire Breath");
    }

    public void fire_spell_2()
    {
        DrawSpellInfo("Explosion");
    }

    public void winter_spell_1()
    {
        DrawSpellInfo("Ice Spikes");
    }

    public void winter_spell_2()
    {
        DrawSpellInfo("Cone of Frost");
    }

    public void earth_spell_1()
    {
        DrawSpellInfo("Bite");
    }

    public void earth_spell_2()
    {
        DrawSpellInfo("Heal");
    }
}
