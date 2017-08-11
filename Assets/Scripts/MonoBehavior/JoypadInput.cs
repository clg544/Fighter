using UnityEngine;
using System.Collections;

public class JoypadInput : MonoBehaviour {
    
    private PlayerBehavior playerBehavior;

    public enum sheildPos { UP, CENTRE, DOWN };
    sheildPos sheildState;
    private bool faceRight;
    private bool joyReset;
    

    // Use this for initialization
    void Start () {

        sheildState = sheildPos.CENTRE;
        playerBehavior = gameObject.GetComponent<PlayerBehavior>();
        faceRight = true;
    }
	
	// Update is called once per frame
	void Update () {
        float leftX = Input.GetAxis("Left Horizontal");
        float leftY = Input.GetAxis("Left Vertical");
        float rightX = Input.GetAxis("Right Horizontal");
        float rightY = Input.GetAxis("Right Vertical");
        
        if (leftY > .5F && (sheildState != sheildPos.UP))
        {
            playerBehavior.SheildUp();
            sheildState = sheildPos.UP;
        }
        else if (leftY < -.5F && (sheildState != sheildPos.DOWN))
        {
            playerBehavior.SheildDown();
            sheildState = sheildPos.DOWN;
        }
        else
        {
            if (leftY < .5F && (sheildState == sheildPos.UP))
            {
                playerBehavior.SheildDown();
                sheildState = sheildPos.CENTRE;
            }
            else if (leftY > -.5F && (sheildState == sheildPos.DOWN))
            {
                playerBehavior.SheildUp();
                sheildState = sheildPos.CENTRE;
            }
        }

        /* Player Movement */
        if (leftX < -.5F)
        {
            faceRight = false;
            playerBehavior.MoveLeft();
        }
        else if (leftX > .5)
        {
            faceRight = true;
            playerBehavior.MoveRight();
        }

        /* Player Attacks */
        if (!joyReset)
        {
            if (faceRight)
            {
                if (rightY > .5F)
                {
                    playerBehavior.Slash();
                }
                else if (rightX > .5)
                {
                    playerBehavior.Stab();
                }
            }
            else /* ! faceRight */
            {
                if (rightY > .5F)
                {
                    playerBehavior.Slash();
                }
                else if (rightX < -.5)
                {
                    playerBehavior.Stab();
                }
            }
        }
        
        /* Reset the Joystick */
        if (this.joyReset)
        {
            if((Mathf.Abs(rightX) < .5f) && (Mathf.Abs(rightY) < .5f))
            {
                joyReset = false;
            }
        }
        else
        {

            if ((Mathf.Abs(rightX) > .5f) || (Mathf.Abs(rightY) > .5f))
            {
                joyReset = true;
            }
        }
    }
}
