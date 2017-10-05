using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class PlayerMovment : MonoBehaviour {

    Rigidbody2D rdbody;
    Animator anim;
    float speed;
    public float walkSpeed = 4;
    public float runModifier = 1.5f;
	public bool canMove;
    public GameObject boat;
    public GameObject hero;
    public static bool isBoat;
    bool heroActive; //hero.active hero.activeInHierarchy and hero.selfactive ne fonctionne pas sory !!!
    bool boatActive;

    // Use this for initialization
    void Start () {
		canMove = true;
        rdbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        heroActive = !isBoat;
        boatActive = isBoat;
    }
	
	// Update is called once per frame
	void Update () {
		
		if (!canMove)
        {
            rdbody.velocity = Vector2.zero;
            return;
        }
		
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed * runModifier;
        }
        else
        {
            speed = walkSpeed;
        }

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

        if (isBoat)
        {
            boat.SetActive(true); boatActive = true;
            if (heroActive)
            {
                boat.GetComponent<Rigidbody2D>().position = hero.GetComponent<Rigidbody2D>().position;
                Debug.Log("Boat to position Hero");
            }
            hero.SetActive(false); heroActive = false;
            boat.GetComponent<Rigidbody2D>().MovePosition(rdbody.position + movment * Time.deltaTime * speed);
        }
        else
        {
            hero.SetActive(true); heroActive = true;
            if (boatActive)
            {
                hero.transform.position = boat.GetComponent<Rigidbody2D>().position;
                Debug.Log("Hero to boat position");
            }
            boat.SetActive(false); boatActive = false;
            hero.GetComponent<Rigidbody2D>().MovePosition(rdbody.position + movment * Time.deltaTime * speed);
        }
    }
}
