using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hitbox : MonoBehaviour {
    
    [SerializeField]
    private int KILLFRAME;
    [SerializeField]
    private int DAMAGE_AMT;

    private BoxCollider2D collider;
    private int frameCount;

    // Use this for initialization
    void Start()
    {
        DAMAGE_AMT = 10;
        collider = this.GetComponent<BoxCollider2D>();
        frameCount = 0;
    }

    /* Later, we could return the result of the attack */
    void OnTriggerEnter2D(Collider2D coll)
    {
        /* Ignore Hitbox Creator */ 
        if (coll.transform == this.transform.parent)
            return;
        
        /* Then, react to damage (Note the friendly fire here)*/
        if (coll.gameObject.name == "Enemy" || (coll.gameObject.name == "Player"))
        {
            coll.gameObject.GetComponent<PlayerBehavior>().HitRegister(this);
        }
    }

    // Update is called once per frame
    void Update () {

        if(frameCount >= KILLFRAME)
        {
            GameObject.Destroy(gameObject);
        }

        frameCount++;
    }

    /**
     *  getKillfreame - Returns the number of frames before this hitbox is destroyed
     */
    public int getKillframe()
    {
        return KILLFRAME;
    }
    /**
     *  setKillfreame - Set the number of frames before this hitbox is destroyed
     */
    public void setKillframe(int kill)
    {
        KILLFRAME = kill;
        return;
    }
    public int getDamage()
    {
        return this.DAMAGE_AMT;
    }
    public BoxCollider2D getCollider()
    {
        return this.collider;
    }
}
