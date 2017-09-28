using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using Assets.Script;

public class PlayerMovment : MonoBehaviour {

    Rigidbody2D rdbody;
    Animator anim;
    float speed;
    AccesBD bd;

    // Use this for initialization
    void Start () {
        rdbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        float value = 0.0f;
        bd = new AccesBD();

        //string conn = "URI=file:" + Application.dataPath + "/BD.db"; //Path to database.
        //IDbConnection dbconn;
        //dbconn = (IDbConnection)new SqliteConnection(conn);
        //dbconn.Open(); //Open connection to the database.
        //IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT Vitesse FROM Stats where idStats = 1";
        
        //dbcmd.CommandText = sqlQuery;
        IDataReader reader = bd.select(sqlQuery);
        //dbcmd.ExecuteReader();
        while (reader.Read())
        {
            value = reader.GetFloat(0);
            Debug.Log("value= " + value);
        }
        reader.Close();
        reader = null;

        //dbcmd.Dispose();
        //dbcmd = null;
        //dbconn.Close();
        //dbconn = null;
        bd.Close();
        speed = value;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 movment = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movment != Vector2.zero)
        {
            anim.SetBool("iswalking", true);
            anim.SetFloat("axe_x", movment.x);
            anim.SetFloat("axe_y", movment.y);
        }
        else
        {
            anim.SetBool("iswalking", false);
        }

        rdbody.MovePosition(rdbody.position + movment * Time.deltaTime * speed);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Chests"))
        {
            bd = new AccesBD();
            string query = "insert into InventaireItem values(4, 3, 1, 1)";
            bd.insert(query);
        }
    }
}
