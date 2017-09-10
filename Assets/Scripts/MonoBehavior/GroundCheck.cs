using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    [SerializeField]
    private PlayerBehavior myBehavior;
    [SerializeField]
    private BoxCollider2D myCollider;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Floor")
        {
            /*  */
            myBehavior.setCanJump(true);
            Debug.Log("GroundCheck:OnTrigger Hit Floor");
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name == "Floor")
        {
            myBehavior.setCanJump(false);
        }
    }

    // Use this for initialization
    void Start () {
        myCollider = gameObject.GetComponentInParent<BoxCollider2D>();
        myBehavior = gameObject.GetComponentInParent<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
