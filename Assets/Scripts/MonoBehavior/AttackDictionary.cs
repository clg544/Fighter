using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackDictionary : MonoBehaviour {

    public LinkedList<Vector3> PLAYER_OVERHEAD_SLASH;
    public LinkedList<Vector3> PLAYER_STAB;

    [SerializeField]
    private int STAB_DURATION = 9;


	// Use this for initialization
	void Awake () {
        PLAYER_OVERHEAD_SLASH = new LinkedList<Vector3>();

        PLAYER_OVERHEAD_SLASH.AddLast(new Vector3(0, 2));
        PLAYER_OVERHEAD_SLASH.AddLast(new Vector3(1, 1));
        PLAYER_OVERHEAD_SLASH.AddLast(new Vector3(2, 0));

        /* PLAYER_STAB */
        PLAYER_STAB = new LinkedList<Vector3>();
        for (int i = 0; i < STAB_DURATION; i++)
        {
            PLAYER_STAB.AddLast(new Vector3(1,0,0));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
