using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    private GameObject whatActivate;

    public void Activate()
    {
        whatActivate.SendMessage("Activate");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
