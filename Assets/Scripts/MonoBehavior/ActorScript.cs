using UnityEngine;
using System.Collections;

public class ActorScript : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D myBody;
    [SerializeField]
    private BoxCollider2D playerBody;
    [SerializeField]
    private Vector3 playerVelocity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        /* Nullify player x velocity on impact */
        if (myBody.IsTouching(playerBody)){
            playerVelocity = Vector3.zero;
        }

	}
}
