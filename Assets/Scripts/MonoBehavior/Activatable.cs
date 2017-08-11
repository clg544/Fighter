using UnityEngine;
using System.Collections;

public class Activatable : MonoBehaviour {

    [SerializeField]
    private Component myActivate;

    public void Activate()
    {
        Debug.Break();
        SendMessage("Activate", myActivate);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
