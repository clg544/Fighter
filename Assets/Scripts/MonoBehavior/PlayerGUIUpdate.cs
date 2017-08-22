using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUIUpdate : MonoBehaviour {

    /* GUI Vars deciding full bar width */
    [SerializeField]
    private float HEALTH_LENGTH;
    [SerializeField]
    private float ENERGY_LENGTH;
    
    [SerializeField]
    private GameObject myPlayer;
    private PlayerBehavior myPlayerBehavior;
    
    [SerializeField]
    private Slider myHealthBar;
    [SerializeField]
    private Slider myEnergyBar;
    
    // Use this for initialization
    void Start () {
        myPlayerBehavior = myPlayer.GetComponent<PlayerBehavior>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(myPlayerBehavior.getHealthRatio().ToString());
        myHealthBar.value = myPlayerBehavior.getHealthRatio();
        myEnergyBar.value = myPlayerBehavior.getEnergyRatio();
    }
}
