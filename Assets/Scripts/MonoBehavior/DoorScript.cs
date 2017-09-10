using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
    [SerializeField]
    bool open;

    SpriteRenderer mySprite;
    BoxCollider2D myCollider;

    /**
     * Activate() - Used by the player to activate this object.
     */
    void Activate()
    {
        open = !(open);
        mySprite.enabled = open;
        myCollider.enabled = open;
        
        return;
    } 
      
	// Use this for initialization
	void Start ()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
