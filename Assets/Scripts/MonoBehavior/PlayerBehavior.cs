using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerBehavior : MonoBehaviour
{
    private Vector3 SHEILD_VECTOR;

    /* Movement Variables */
    [SerializeField]
    private float SHEILD_ADJ;
    [SerializeField]
    private float MOVEMENT;
    [SerializeField]
    private float MAX_SPEED;
    [SerializeField]
    private float SLOW_AMT;     // Speed divided by this each frame that player is not moving

    [SerializeField]
    private bool canJump;
    [SerializeField]
    private int JUMP_SPEED;
    [SerializeField]
    private int JUMP_FRAMES;
    private int ascending;


    /* Local Attack Variables */
    [SerializeField]
    private float STAB_DISTANCE;
    [SerializeField]
    private int STAB_COOL;
    [SerializeField]
    private int STAB_ENERGY;
    [SerializeField]
    private float SLASH_SPEED;
    [SerializeField]
    private int SLASH_COOL;
    [SerializeField]
    private int SLASH_ENERGY;
    private Vector3 STAB_VECTOR;
    private Vector3 SLASH_VECTOR;
    
    /* Statistic Variables */
    [SerializeField]
    private int MAX_HEALTH;
    [SerializeField]
    private int MAX_ENERGY;

    /* Combat Management */ 
    [SerializeField]
    private int atkCooldown;
    private LinkedListNode<Vector3> curAttackNode;
    private int curAttackPos;
    [SerializeField]
    private int hitRecoil;
    
    /* Player Direction 
     * 1 - Right
     * 0 - Left
     */
    private bool faceRight;
    [SerializeField]
    private bool canMoveForward;
    private Vector3 SWITCH_DIR;

    /*  Player Stats */
    private int health;
    private int energy;
    
    /* Objects that belong to this player */
    public GameObject player;           /* Holds our player */
    public GameObject attack;           /* Holds attack hitbox */
    public GameObject sheild;           /* Holds our Shield */
    public ActivatorScript activate;         /* Holds player activator */
    private SpriteRenderer mySprite;
    private Rigidbody2D playerBody;


    /* Sheild Manipulation */
    public void SheildUp()
    {
        sheild.transform.localPosition += SHEILD_VECTOR;
    }

    public void SheildDown()
    {
        sheild.transform.localPosition -= SHEILD_VECTOR;
    }

    /* Player Movement */
    public void MoveLeft()
    {
        if (faceRight)
        {
            faceRight = false;
            canMoveForward = true;  // Until we hit a wall...
            FaceLeft();
        }

        // Need special case for turning around
        if (Mathf.Abs(playerBody.velocity.x) <= MAX_SPEED)
        {
            playerBody.velocity = new Vector2(-MAX_SPEED * (canMoveForward?1:0), playerBody.velocity.y);
        }

        if (faceRight)
        {
            faceRight = false;
            FaceLeft();
        }
    }
    public void MoveRight()
    {
        // Need special case for turning around
        if (Mathf.Abs(playerBody.velocity.x) <= MAX_SPEED)
        {
            playerBody.velocity = new Vector2(MAX_SPEED * (canMoveForward?1:0), playerBody.velocity.y);
        }

        if (!faceRight)
        {
            faceRight = true;
            canMoveForward = true;  // Until we hit a wall...
            FaceRight();
        }
    }
    public void FaceRight()
    {
        Vector3 newV = new Vector3(-sheild.transform.localPosition.x, sheild.transform.localPosition.y, 0);
        sheild.transform.localPosition = newV;
    }
    public void FaceLeft()
    {
        Vector3 newV = new Vector3(-sheild.transform.localPosition.x, sheild.transform.localPosition.y, 0);
        sheild.transform.localPosition = newV;
    }
    public void Slow()
    {
        float newX = playerBody.velocity.x;
        newX /= SLOW_AMT;
        playerBody.velocity = new Vector2(newX, playerBody.velocity.y);
    }
    public void HitWall()
    {
        canMoveForward = false;
    }

    /* Player Jumping */
    public void setCanJump(bool newVal)
    {
        canJump = newVal;
    }

    public void Jump()
    {
        if(canJump)
            ascending = JUMP_FRAMES;
    }
    /**
     * Apply the given vector to this Player.  Logic will be applied to avoid unsavory behavior.
     */ 
    public void LaunchPlayer(Vector2 launch)
    {
        playerBody.AddForce(launch);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
    }

    /* Player Attacks */
    public void Stab()
    {
        /* Abort if in cooldown */
        if ((atkCooldown > 0) || (energy < 0))
            return;

        /* Create an inversable X vector */
        Vector3 inverseX = new Vector3(faceRight?1:-1, 1, 1);
        /* The Spawn distance from Player anchor */ 
        Vector3 attackPos = new Vector3(1, 0, 0);
        /* Find initial position for hit */
        Vector3 pos = Vector3.Scale(attackPos, inverseX) + this.transform.position;

        GameObject newAtk = Instantiate(attack, pos, Quaternion.identity) as GameObject;

        newAtk.transform.parent = this.transform;

        atkCooldown += STAB_COOL;
        this.energy -= STAB_ENERGY;
    }
    public void Slash()
    {
        /* Abort if in cooldown */
        if ((atkCooldown > 0) || (energy < 0))
            return;

        Vector3 inverseX = new Vector3(faceRight ? 1 : -1, 1, 1);
        Vector3 attackPos = new Vector3(0, 1, 0);
        Vector3 pos = Vector3.Scale(attackPos, inverseX) + this.transform.position;

        GameObject newAtk = Instantiate(attack, pos, Quaternion.identity) as GameObject;
        newAtk.transform.parent = this.transform;

        newAtk.GetComponent<Rigidbody2D>().velocity = Vector3.Scale(SLASH_VECTOR, inverseX);

        atkCooldown += SLASH_COOL;
        this.energy -= SLASH_ENERGY;
    }
    /**
     * SetAttackList - A general attack given via a Linked list.  Each frame will instantiate
     *      a hitbox at the given vector as a localPosition.
     * 
     * attack - The linked list of ordered localPositions
     */
    public void SetAttackList(System.Collections.Generic.LinkedList<Vector3> attackList)
    {
        /* Abort if in cooldown */
        if (atkCooldown > 0)
            return;

        this.atkCooldown += 14;

        this.curAttackNode = attackList.First;
        this.curAttackPos = 0;
    }

    /**
     * hitboxAtLocation - Create a hitbox at the location, for one frame
     */
    private Hitbox hitboxAtLocation(Vector3 pos)
    {
        GameObject newAtk = Instantiate(attack, (pos + this.transform.position), Quaternion.identity) as GameObject;
        newAtk.transform.parent = this.transform;
        newAtk.GetComponent<Hitbox>().setKillframe(1);

        return newAtk.GetComponent<Hitbox>();
    }

    /* React to Damage */
    public void HitRegister(Hitbox hitBy)
    {
        if (hitBy.getCollider().IsTouching(this.sheild.GetComponent<BoxCollider2D>()))
        {
            hitBy.GetComponentInParent<PlayerBehavior>().SetAttackList(null);
            hitBy.GetComponentInParent<PlayerBehavior>().sheildRecoil();
            return;
        }

        /* If we aren't currently recoiling from a hit already... */
        if (this.hitRecoil == 0)
        {
            health -= hitBy.getDamage();

            hitRecoil += 14;
            this.playerBody.AddForce(new Vector3(10, 10, 0));
        }
    }

    /* Bouncing off an enemy sheild */
    public void sheildRecoil()
    {
        atkCooldown = 15;
        curAttackNode = null;
        curAttackPos = 0;
    }

    private void setSpriteValues()
    {
        if (atkCooldown > 0)
        {
            mySprite.color = new Color(1f, 1f, 1f, .5f);
        }

        if(hitRecoil % 4 >= 2)
        {
            mySprite.color = new Color(1f, 1f, 1f, 0f);
        }
    }


    /**
     * PlayerActivate() 
     * */
    public void PlayerActivate()
    {
        activate.Activate();
    }


    // Use this for initialization
    void Start ()
    {
        /* Create Runtime Vectors */
        SWITCH_DIR = new Vector3(-1,0,0); 
        SHEILD_VECTOR = new Vector3(0, SHEILD_ADJ, 0);
        //STAB_VECTOR = new Vector3(STAB_DISTANCE, 0, 0);
        SLASH_VECTOR = new Vector3(SLASH_SPEED, -SLASH_SPEED/2, 0);

        /* Get Relevent Components */
        playerBody = this.GetComponent<Rigidbody2D>();
        mySprite = this.GetComponent<SpriteRenderer>();
        activate = this.GetComponentInChildren<ActivatorScript>();

        /* Sprite Direction */
        faceRight = true;
        canJump = true;

        /* Statistics */
        health = MAX_HEALTH;
        energy = MAX_ENERGY;
    }
	
	// Update is called once per frame
	void Update () {

        /* Reduce Attack Cooldown */ 
        if (atkCooldown > 0)
            atkCooldown -= 1;

        /* Reduce hitRecoil */
        if (hitRecoil > 0)
            hitRecoil -= 1;

        /* Ascend at JUMP_SPEED for JUMP_FRAMES */
        if (ascending > 0)
        {
            ascending -= 1;
            playerBody.velocity = new Vector2(playerBody.velocity.x, JUMP_SPEED);
        }

        if (energy < MAX_ENERGY)
            energy++;


        mySprite.color = new Color(1f, 1f, 1f, 1f);
        setSpriteValues();
        
        /* Iterate through an active attack at each frame */
        if(curAttackNode != null)
        {
            Vector3 inverseX = new Vector3(faceRight ? 1 : -1, 1, 1);

            Hitbox curBox = hitboxAtLocation(Vector3.Scale(inverseX, curAttackNode.Value));
            if (curAttackNode.Next != null)
            {
                curAttackNode = curAttackNode.Next;
            }
            else
            {
                curAttackNode = null;
            }
        }
    }
    
    /* Getters and Setters */
    public bool isFacingRight()
    {
        return faceRight;
    }

    public float getMaxSpeed()
    {
        return MAX_SPEED;
    }

    public int getMaxHealth()
    {
        return MAX_HEALTH;
    }
    public int getMaxEnergy()
    {
        return MAX_ENERGY;
    }
    public int getHealth()
    {
        return health;
    }
    public int getEnergy()
    {
        return energy;
    }
}
