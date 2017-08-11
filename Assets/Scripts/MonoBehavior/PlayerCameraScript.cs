using UnityEngine;
using System.Collections;

public class PlayerCameraScript : MonoBehaviour {

    private GameObject myCamera;
    [SerializeField]
    private GameObject myTarget;

    /* Vectors to do trivial adjustments */
    Vector3 ZAdjustVect;
    Vector3 PlayerYAdjustVect;
    Vector3 CameraSpeedVect;

    /* Subtract these to account for the Z Vector */ 
    [SerializeField]
    private float Z_ADJ;
    [SerializeField]
    private float PLAYER_Y_ADJ;
    /* Camera moves at 1 / CAMERA_SPEED */
    [SerializeField]
    private float CAMERA_SPEED;

    public GameObject MyTarget
    {
        get
        {
            return myTarget;
        }

        set
        {
            myTarget = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        myCamera = this.gameObject;

        ZAdjustVect = new Vector3(0, 0, Z_ADJ);
        PlayerYAdjustVect = new Vector3(0, PLAYER_Y_ADJ, 0);
        CameraSpeedVect = new Vector3(1 / CAMERA_SPEED, 1 / CAMERA_SPEED, 1 / CAMERA_SPEED);

        /* Initiate Camera */
        myCamera.transform.position = MyTarget.transform.position;
    }


    private Vector3 CameraTracking()
    {
        return MyTarget.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Moving Camera...");

        myCamera.transform.position = CameraTracking();
        myCamera.transform.position += ZAdjustVect;
        myCamera.transform.position += PlayerYAdjustVect;
    }
}
