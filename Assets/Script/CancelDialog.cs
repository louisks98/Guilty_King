using Assets.Script;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelDialog : MonoBehaviour {

    public GameObject Mom;
    public GameObject Rich;
    public GameObject MordraxCollider;
    public GameObject FinalZoneCollider;
    public static bool mordraxDead;
    public static bool forestDead;
    public static bool fireDead;
    public static bool iceDead;

    // Use this for initialization
    void Start () {

        AccesBD bd = new AccesBD();
        SqliteDataReader reader;

        string forestBossEstVaincu = "N";
        string fireBossEstVaincu = "N";
        string iceBossEstVaincu = "N";
        string mordraxEstVaincu = "N";

        string sqlForestEstVaincu = "select vaincue from personnage where idPersonnage = 2";
        string sqlFireEstVaincu = "select vaincue from personnage where idPersonnage = 3";
        string sqlIceEstVaincu = "select vaincue from personnage where idPersonnage = 4";
        string sqlMordraxEstVaincu = "select vaincue from personnage where idPersonnage = 23";

        reader = bd.select(sqlForestEstVaincu);

        while (reader.Read())
        {
            forestBossEstVaincu = reader.GetString(0);
        }
        if (forestBossEstVaincu == "O")
        {
            forestDead = true;
        }
        else
        {
            forestDead = false;
        }

        reader = bd.select(sqlFireEstVaincu);

        while (reader.Read())
        {
            fireBossEstVaincu = reader.GetString(0);
        }
        if (fireBossEstVaincu == "O")
        {
            fireDead = true;
        }
        else
        {
            fireDead = false;
        }

        reader = bd.select(sqlIceEstVaincu);

        while (reader.Read())
        {
            iceBossEstVaincu = reader.GetString(0);
        }
        if (iceBossEstVaincu == "O")
        {
            iceDead = true;
        }
        else
        {
            iceDead = false;
        }

        reader = bd.select(sqlMordraxEstVaincu);

        while (reader.Read())
        {
            mordraxEstVaincu = reader.GetString(0);
        }
        if (mordraxEstVaincu == "O")
        {
            mordraxDead = true;
        }
        else
        {
            mordraxDead = false;
        }

        reader.Close();
        reader = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (forestDead)
        {
            Mom.GetComponent<DialogHolder>().hasBeenTalked = true;
            Rich.GetComponent<DialogHolder>().hasBeenTalked = true;
        }
  
        if (mordraxDead)
        {
            MordraxCollider.GetComponent<DialogHolder>().hasBeenTalked = true;
        }

        if (forestDead && fireDead && iceDead)
        {
            FinalZoneCollider.GetComponent<DialogHolder>().hasBeenTalked = true;
            Destroy(FinalZoneCollider);
        }
    }
}
