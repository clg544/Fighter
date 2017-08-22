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

        myHealthBar.maxValue = myPlayerBehavior.getMaxHealth();
        myEnergyBar.maxValue = myPlayerBehavior.getMaxEnergy();
    }
	
	// Update is called once per frame
	void Update () {
        myHealthBar.value = myPlayerBehavior.getHealth();
        myEnergyBar.value = myPlayerBehavior.getEnergy();
    }
}
