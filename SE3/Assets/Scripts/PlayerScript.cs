﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{


    public GameObject tallChar;
    public GameObject medChar;
    public GameObject shortchar;

    Vector3 lead;
    Vector3 left;
    Vector3 right;

    public BoardManager level;

    public float cool = 0.25f;
    public float coolTimer;
    public bool canMove;

    public float swapCool = 0.1f;
    public float swapTimer = 0;
    public bool canSwap;

    public float tooLong = 4;
    public float tooLongTimer = 4;


    public int front = 2;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(level.startingTile.transform.position.x, transform.position.y, level.startingTile.transform.position.z);
        coolTimer = 0;

        left = tallChar.transform.localPosition;
        right = shortchar.transform.localPosition;
        lead = medChar.transform.localPosition;
    }

    void switchCharsLeft()
    {
        if (front == 1)
        {
            front = 2;
            tallChar.transform.localPosition = left;
            medChar.transform.localPosition = lead;
            shortchar.transform.localPosition = right;

        }
        else if (front == 2)
        {

            front = 3;
            tallChar.transform.localPosition = right;
            medChar.transform.localPosition = left;
            shortchar.transform.localPosition = lead;

        }
        else if (front == 3)
        {

            front = 1;
            tallChar.transform.localPosition = lead;
            medChar.transform.localPosition = right;
            shortchar.transform.localPosition = left;
        }

    }

    void switchCharsRight()
    {

        if (front == 1)
        {


            front = 3;
            tallChar.transform.localPosition = right;
            medChar.transform.localPosition = left;
            shortchar.transform.localPosition = lead;

        }
        else if (front == 2)
        {
            front = 1;
            tallChar.transform.localPosition = lead;
            medChar.transform.localPosition = right;
            shortchar.transform.localPosition = left;

        }
        else if (front == 3)
        {

            front = 2;
            tallChar.transform.localPosition = left;
            medChar.transform.localPosition = lead;
            shortchar.transform.localPosition = right;


        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!canMove)
        {

            coolTimer -= Time.deltaTime;
            if (coolTimer <= 0)
            {
                canMove = true;

            }

        }

        //MOTION
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.W) && transform.position.z + .98f < 16.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .98f);
                checkTile();
            }
            else if (Input.GetKeyDown(KeyCode.A) && transform.position.x - .98f > -8.5f)
            {
                transform.position = new Vector3(transform.position.x - .98f, transform.position.y, transform.position.z);
                checkTile();

            }
            else if (Input.GetKeyDown(KeyCode.S) && transform.position.z - .98f > 0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .98f);
                checkTile();
            }
            else if (Input.GetKeyDown(KeyCode.D) && transform.position.x + .98f < 8f)
            {
                transform.position = new Vector3(transform.position.x + .98f, transform.position.y, transform.position.z);
                checkTile();
            }

            level.showTiles(front);

            coolTimer = cool;
            canMove = false;
        }

        if (!canSwap)
        {
            swapTimer -= Time.deltaTime;
            if (swapTimer <= 0)
            {

                canSwap = true;

            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (canSwap)
            {
                switchCharsLeft();
                canSwap = false;
                swapTimer = swapCool;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (canSwap)
            {
                switchCharsRight();
                canSwap = false;
                swapTimer = swapCool;
            }

        }


        tooLongTimer -= Time.deltaTime;
        if(tooLongTimer <= 0)
        {

            Die();

        }

    }

    void checkTile()
    {
        tooLongTimer = tooLong;
        Ray r = new Ray(transform.position, Vector3.down);

        RaycastHit rHit = new RaycastHit();
        Physics.Raycast(r, out rHit);

        TileSelector tS = rHit.transform.gameObject.GetComponent<TileSelector>();
        if (tS != null && !tS.isSafe)
        {

            //die
            Die();

        }
        

    }

    void Die()
    {
        Debug.Log("DEATH");
        transform.position = new Vector3(level.startingTile.transform.position.x, transform.position.y, level.startingTile.transform.position.z);

    }
}
