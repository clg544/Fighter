using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    bool open;

    SpriteRenderer mySprite;
    BoxCollider2D myCollider;

    /**
     * Activate() - Used by the player to activate this object.
     */
    void Activate()
    {
        open = !(open);
        mySprite.enabled = !(open);
        myCollider.isTrigger = open;

        return;
    } 
      
	// Use this for initialization
	void Start ()
    {
        open = false;

        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
