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
	public static bool canMove;
    public static bool inCombat;
    public GameObject boat;
    public GameObject hero;
    public static bool isBoat;
    public static bool isTransition;
    bool heroActive; //hero.active hero.activeInHierarchy and hero.selfactive ne fonctionne pas sory !!!
    bool boatActive;

    // Use this for initialization
    void Start () {
        isTransition = false;
		canMove = true;
        inCombat = false;
        rdbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        heroActive = !isBoat;
        boatActive = isBoat;
    }

    // Update is called once per frame
    void Update() {

        if (!canMove || PauseMenu.paused)
        {
            rdbody.velocity = Vector2.zero;
            if(!inCombat)
            {
                Time.timeScale = 0;
            }
            return;
        }

        if (!isTransition)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = walkSpeed * runModifier;
            }
            else
            {
                speed = walkSpeed;
            }
        }
        else
        {
            speed = 0;
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
