using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Assets.Script;
using System;

public class OpenLevelUpHint : MonoBehaviour {

    public GameObject levelUpButton;

    // Use this for initialization
    void Start () {
        AfficherLevelHint();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AfficherLevelHint()
    {
        AccesBD bd = new AccesBD();
        SqliteDataReader reader;

        string sqlGetNbSouls = "select nbAmes from Stats where idStats = 1";
        int nbSouls = 0;

        reader = bd.select(sqlGetNbSouls);

        while (reader.Read())
        {
            nbSouls = reader.GetInt32(0);
        }

        if (nbSouls > 0)
        {
            levelUpButton.SetActive(true);
        }
        else
        {
            levelUpButton.SetActive(false);
        }
        reader.Close();
        reader = null;
        bd.Close();
    }
}
