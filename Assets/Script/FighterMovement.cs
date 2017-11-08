using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    Rigidbody2D rdbody;
    public Animator anim;
    public bool isAlly;
    public bool movingLeft;
    public bool movingRight;

    // Attacking
    public bool isAttacking;
    public float attackTime;
    private float attacTimeCounter;
    private bool isCounting = false;

    // Dying
    public bool isDying = false;

    // Use this for initialization
    void Start()
    {
        rdbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Init_Position();
    }

    public void Init_Position()
    {
        pointA = rdbody.position;
        if (isAlly)
        {
            pointB.x = pointA.x - 1;
        }
        else
        {
            pointB.x = pointA.x + 1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isAlly)
        {
            Vector2 movLeft = new Vector2(-2f, 0f);
            Vector2 movRight = new Vector2(2f, 0f);
            if (movingLeft)
            {
                if (movLeft != Vector2.zero)
                {
                    anim.SetBool("iswalking", true);
                    anim.SetFloat("axe_x", movLeft.x);
                }
                rdbody.MovePosition(rdbody.position + movLeft * Time.deltaTime * 1);
                if (rdbody.position.x <= pointB.x)
                {
                    movingLeft = false;
                    anim.SetBool("iswalking", false);
                    //             movingRight = true; // À RETIRER
                }
            }
            if (movingRight)
            {
                if (movRight != Vector2.zero)
                {
                    anim.SetBool("iswalking", true);
                    anim.SetFloat("axe_x", movRight.x);
                }
                rdbody.MovePosition(rdbody.position + movRight * Time.deltaTime * 1);
                if (rdbody.position.x >= pointA.x)
                {
                    movingRight = false;
                    anim.SetBool("iswalking", false);
                }
            }

            if (isDying)
            {

                rdbody.velocity = Vector2.zero;
                anim.SetBool("isDying", true);
            }
            else
            {
                anim.SetBool("isDying", false);
            }

            if (isAttacking)
            {
                if (!isCounting)
                {
                    attacTimeCounter = attackTime;
                    isCounting = true;
                }
                rdbody.velocity = Vector2.zero;
                anim.SetBool("isAttacking", true);

            }

            if (attacTimeCounter > 0)
            {
                attacTimeCounter -= Time.deltaTime;
            }

            if (attacTimeCounter <= 0)
            {
                isAttacking = false;
                isCounting = false;
                anim.SetBool("isAttacking", false);
            }

           
        }
        else
        {
            Vector2 movLeft = new Vector2(-2f, 0f);
            Vector2 movRight = new Vector2(2f, 0f);
            if (movingRight)
            {
                if (movRight != Vector2.zero)
                {
                    anim.SetBool("iswalking", true);
                    anim.SetFloat("axe_x", movRight.x);
                }
                rdbody.MovePosition(rdbody.position + movRight * Time.deltaTime * 1);
                if (rdbody.position.x >= pointB.x)
                {
                    movingRight = false;
                    anim.SetBool("iswalking", false);
                    //            movingLeft = true; // À RETIRER
                }
            }
            if (movingLeft)
            {
                if (movLeft != Vector2.zero)
                {
                    anim.SetBool("iswalking", true);
                    anim.SetFloat("axe_x", movLeft.x);
                }
                rdbody.MovePosition(rdbody.position + movLeft * Time.deltaTime * 1);
                if (rdbody.position.x <= pointA.x)
                {
                    movingLeft = false;
                    anim.SetBool("iswalking", false);
                }
            }

            if (isDying)
            {

                rdbody.velocity = Vector2.zero;
                anim.SetBool("isDying", true);
            }
            else
            {
                anim.SetBool("isDying", false);
            }

            if (isAttacking)
            {
                if (!isCounting)
                {
                    attacTimeCounter = attackTime;
                    isCounting = true;
                }
                rdbody.velocity = Vector2.zero;
                anim.SetBool("isAttacking", true);

            }

            if (attacTimeCounter > 0)
            {
                attacTimeCounter -= Time.deltaTime;
            }

            if (attacTimeCounter <= 0)
            {
                isAttacking = false;
                isCounting = false;
                anim.SetBool("isAttacking", false);
            }
        }
    }
}