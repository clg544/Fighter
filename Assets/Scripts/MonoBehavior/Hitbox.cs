using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hitbox : MonoBehaviour
{
    public enum DAMAGE_TYPE { STANDARD }

    public struct HitboxDescriptor
    {
        /* Descriptor Data */
        public int number;
        public float length;
        public float width;
        public float rotation;
        public int duration;
        public int damage;
        public DAMAGE_TYPE damageType;

        /* Put non critical qualities into a dictionary to be read */
        public LinkedList<string> optionalTraitKeys;
        public LinkedList<string> optionalTraitValues;
    }

    private HitboxDescriptor myDescriptor;
    private BoxCollider2D myCollider;
    private int frameCount;
    
    /* Later, we could return the result of the attack */
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Break();

        /* Ignore Hitbox Creator */
        if (coll.transform == this.transform.parent)
            return;

        /* Then, react to damage (Note the friendly fire here)*/
        if (coll.gameObject.name == "Enemy" || (coll.gameObject.name == "Player"))
        {
            coll.gameObject.GetComponent<PlayerBehavior>().HitRegister(this);
        }
    }

    // Use this for initialization
    void Awake()
    {
        Debug.Break();

        myCollider = this.GetComponent<BoxCollider2D>();

        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Break();

        if (frameCount >= myDescriptor.duration)
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
        return myDescriptor.duration;
    }
    /**
     *  setKillfreame - Set the number of frames before this hitbox is destroyed
     */
    public void setKillframe(int kill)
    {
        myDescriptor.duration = kill;
        return;
    }
    public int getDamage()
    {
        return myDescriptor.damage;
    }
    public BoxCollider2D getCollider()
    {
        return this.myCollider;
    }
}
