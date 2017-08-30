using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour {

    PlayerBehavior myPlayer;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Walls")
        {
            Debug.Log("Wall Check!");
            myPlayer.HitWall();
        }
    }

    // Use this for initialization
    void Start()
    {
        myPlayer = this.GetComponentInParent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
