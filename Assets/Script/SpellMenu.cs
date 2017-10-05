using Assets.Script;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SpellMenu : MonoBehaviour {

    public GameObject spellMenuCanva;
    public Text spellName;
    public Text spellType;
    public Text spellDescription;

    // Use this for initialization
    void Start () {
        hero_spell_1();
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void DrawSpellInfo(string spellId)
    {
        string name = "";
        string description = "";
        string type = "";
        string acquis = "";

        try
        {
            AccesBD bd = new AccesBD();
            IDataReader reader = bd.select("SELECT Sort.Nom,Sort.Description,TypeSort.Nom,Sort.Valeur,Sort.Acquis FROM Sort INNER JOIN TypeSort ON Sort.Type = TypeSort.id where Sort.id ='" + spellId + "'");
            while (reader.Read())
            {
                name = reader.GetString(0);
                description = reader.GetString(1);
                type = reader.GetString(2) + " (X): " + reader.GetDecimal(3).ToString();
                acquis = reader.GetString(4);
                Debug.Log("GetSort(" + name + "," + description + "," + type + "," + acquis +  ")");
            }
            reader.Close();
            reader = null;
            bd.Close();

            if (acquis.Equals("O"))
            {
                spellName.text = name;
                spellDescription.text = description;
                spellType.text = type;
            }
            else
            {
                spellName.text = "Sort inconnu"; 
                spellDescription.text = "Vous n'avez pas encore découvert ce sort.";
                spellType.text = "";
            }
        }
        catch (Exception e)
        {
            spellName.text = "Oups! Le sort a été volé";
            spellDescription.text = "Revenez plus tard il y sera peut-être :)"; 
        }
    }

    public void hero_spell_1()
    {
        DrawSpellInfo("H1");
    }

    public void hero_spell_2()
    {
        DrawSpellInfo("H2");
    }

    public void hero_spell_3()
    {
        DrawSpellInfo("H3");
    }

    public void hero_spell_4()
    {
        DrawSpellInfo("H4");
    }

    public void fire_spell_1()
    {
        DrawSpellInfo("F1");
    }

    public void fire_spell_2()
    {
        DrawSpellInfo("F2");
    }

    public void winter_spell_1()
    {
        DrawSpellInfo("G1");
    }

    public void winter_spell_2()
    {
        DrawSpellInfo("G2");
    }

    public void earth_spell_1()
    {
        DrawSpellInfo("T1");
    }

    public void earth_spell_2()
    {
        DrawSpellInfo("T2");
    }
}
