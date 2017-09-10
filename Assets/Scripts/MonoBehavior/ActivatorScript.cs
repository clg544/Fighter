using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatorScript : MonoBehaviour
{

    private LinkedList<Collider2D> activatables;

    // Maintain collider List
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Activatables"))
            activatables.AddFirst(coll);
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Removed Coll\n");
        if (coll.CompareTag("Activatables"))
            activatables.Remove(coll);
    }

    /**
     * Activate() - Called by playerBehavior to activate any applicable 
     *              objects.  In future, apply facing right and 
     *              proximity logic.
     **/
    public void Activate()
    {
        Collider2D curColl;

        for(int i = 0; i < activatables.Count; i++)
        {
            curColl = activatables.First.Value;
            curColl.gameObject.SendMessage("Activate");
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        activatables = new LinkedList<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
