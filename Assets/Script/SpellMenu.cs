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
    public Image fire;
    public Image winter;
    public Image forest;

    public Sprite spFire;
    public Sprite spWinter;
    public Sprite spForest;
    public Sprite spQuestionMark;

    public static bool update;

    // Use this for initialization
    void Start () {
        hero_spell_1();
        update = true;
    }

    private void Update()
    {
        if(update)
        {
            DrawHeroKnown();
            update = false;
        }
    }

    public void DrawHeroKnown()
    {
        if (IsKnown(2))
        {
            forest.sprite = spForest;
        }
        else
        {
            forest.sprite = spQuestionMark;
        }

        if (IsKnown(3))
        {
            fire.sprite = spFire;
        }
        else
        {
            fire.sprite = spQuestionMark;
        }

        if (IsKnown(4))
        {
            winter.sprite = spWinter;
        }
        else
        {
            winter.sprite = spQuestionMark;
        }
    }

    public bool IsKnown(int heroId)
    {
        bool isKnown = false;
        AccesBD bd = new AccesBD();
        IDataReader reader = null;

        try
        {
            string rep = "";
            reader = bd.select("SELECT Personnage.vaincue FROM Personnage WHERE Personnage.idPersonnage =" + heroId.ToString());
            while (reader.Read())
            {
                rep = reader.GetString(0);
                //Debug.Log("Hero " + heroId + " vaincue:" + rep);
            }

            if (rep.Equals("O"))
            {
                isKnown = true;
            }
            else
            {
                isKnown = false;
            }
        }
        catch (Exception e)
        {
            isKnown = false;
            Debug.Log("Error when search for hero " + heroId);
        }
        finally
        {
            reader.Close();
            reader = null;
            bd.Close();
        }

        return isKnown;
    }

    public void DrawSpellInfo(bool isKnown,string spellId)
    {
        string name = "";
        string description = "";
        string type = "";
        string acquis = "";

        AccesBD bd = new AccesBD();
        IDataReader reader = null;

        try
        {
            reader = bd.select("SELECT Sort.Nom,Sort.Description,TypeSort.Nom,Sort.Valeur,Sort.Acquis FROM Sort INNER JOIN TypeSort ON Sort.Type = TypeSort.id where Sort.id ='" + spellId + "'");
            while (reader.Read())
            {
                name = reader.GetString(0);
                description = reader.GetString(1);
                type = reader.GetString(2) + " (X): " + reader.GetDecimal(3).ToString();
                acquis = reader.GetString(4);
                //Debug.Log("GetSort(" + name + "," + description + "," + type + "," + acquis + ")");
            }

            if (acquis.Equals("O") && isKnown)
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
            Debug.Log("Error when search for spell " + spellId);
        }
        finally
        {
            reader.Close();
            reader = null;
            bd.Close();
        }
    }

    public void hero_spell_1()
    {
        DrawSpellInfo(IsKnown(1),"H1");
    }

    public void hero_spell_2()
    {
        DrawSpellInfo(IsKnown(1),"H2");
    }

    public void hero_spell_3()
    {
        DrawSpellInfo(IsKnown(1),"H3");
    }

    public void hero_spell_4()
    {
        DrawSpellInfo(IsKnown(1),"H4");
    }

    public void fire_spell_1()
    {
        DrawSpellInfo(IsKnown(3),"F1");
    }

    public void fire_spell_2()
    {
        DrawSpellInfo(IsKnown(3),"F2");
    }

    public void winter_spell_1()
    {
        DrawSpellInfo(IsKnown(4),"G1");
    }

    public void winter_spell_2()
    {
        DrawSpellInfo(IsKnown(4),"G2");
    }

    public void earth_spell_1()
    {
        DrawSpellInfo(IsKnown(2),"T1");
    }

    public void earth_spell_2()
    {
        DrawSpellInfo(IsKnown(2),"T2");
    }
}
