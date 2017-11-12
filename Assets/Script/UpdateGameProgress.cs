using Assets.Script;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameProgress : MonoBehaviour
{
    static public bool doVerification;
    public GameObject[] tabEnemy;

    // Use this for initialization
    void Start()
    {
        doVerification = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doVerification == true)
        {
            AccesBD bd = new AccesBD();
            SqliteDataReader reader;
           

            for (int i = 0; i < tabEnemy.Length; i++)
            {
                estVaincu = "N";
                string sqlEstVaincu = "select vaincue from personnage where idPersonnage = " + (i + 2); // car le Id personnage commence à 1 (+1) et le héro ne doit pas disparaitre (+1).
                reader = bd.select(sqlEstVaincu);
                string estVaincu = "N";
                while (reader.Read())
                {
                    estVaincu = reader.GetString(0);
                }
                if (estVaincu == "O")
                {
                    tabEnemy[i].SetActive(false);
                }

                reader.Close();
                reader = null;
            }
            bd.Close();
            doVerification = false;
        }  
    }
}
