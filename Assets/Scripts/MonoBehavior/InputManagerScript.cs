using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class InputManagerScript : MonoBehaviour {
    Vector3 SHEILD_VECTOR;

    private Rigidbody2D body;
    private PlayerBehavior playerBehavior;

    /* Public Data */
    private AttackDictionary attackDict;

    private void Awake()
    {
        SHEILD_VECTOR = new Vector3(0, 3F, 0);
        playerBehavior = gameObject.GetComponent<PlayerBehavior>();

        /* Public Data */
        attackDict = GameObject.FindGameObjectWithTag("Managers").GetComponent<AttackDictionary>();
    }

    private void Update ()
    {
        /*  Sheild Behavior */
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerBehavior.SheildUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerBehavior.SheildDown();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerBehavior.SheildDown();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerBehavior.SheildUp();
        }

        /* Player Movement */ 
        if (Input.GetKey(KeyCode.A))
        {
            playerBehavior.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerBehavior.MoveRight();
        }
        else
        {
            playerBehavior.Slow();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerBehavior.Jump();
        }

        /* Player Attacks */
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            playerBehavior.Slash();
        }
        /* Directional Sensitive Attacks */
        if (playerBehavior.isFacingRight())
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerBehavior.Stab();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //playerBehavior.SetAttackList(attackDict.PLAYER_STAB);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //playerBehavior.SetAttackList(attackDict.PLAYER_OVERHEAD_SLASH);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerBehavior.PlayerActivate();
        }
    }
}
