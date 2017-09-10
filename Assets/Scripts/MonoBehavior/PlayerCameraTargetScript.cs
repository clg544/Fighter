using UnityEngine;
using System.Collections;

public class PlayerCameraTargetScript : MonoBehaviour {

    [SerializeField]
    float ACCEL;
    float MAX_SPEED;
    Vector3 accel_scale;

    [SerializeField]
    float MAX_X;
    [SerializeField]
    float MAX_Y;
    Vector3 maxX;
    Vector3 maxY;

    Rigidbody2D parentMotion;


	// Use this for initialization
	void Start () {
        parentMotion = GetComponentInParent<Rigidbody2D>();

        MAX_SPEED = GetComponentInParent<PlayerBehavior>().getMaxSpeed();

        accel_scale = new Vector3(ACCEL, ACCEL, ACCEL);
        maxX = new Vector3(MAX_X, 0, 0);
        maxY = new Vector3(0, MAX_Y, 0);

    }

	
	// Update is called once per frame
	void Update () {
        
        if (Mathf.Sign(parentMotion.velocity.x) != Mathf.Sign(this.transform.localPosition.x))
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                Vector3.zero, ACCEL);
        
        if (parentMotion.velocity.x > 1)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                maxX, ACCEL);
        }
        else if (parentMotion.velocity.x < -1)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                -(maxX), ACCEL);
        }
        else
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition,
                    Vector3.zero, ACCEL);
        }
        
    }
}
