using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHitboxSelector : MonoBehaviour {
    
    private Dropdown myDropdown;

    public void UpdateHitboxDropdown()
    {
        myDropdown.ClearOptions();

        GameObject[] hitboxFetch = GameObject.FindGameObjectsWithTag("Hitbox");
        Hitbox[] hitboxList = new Hitbox[hitboxFetch.Length];

        for(int i = 0; i < hitboxFetch.Length; i++)
        {
            hitboxList[i] = hitboxFetch[i].GetComponent<Hitbox>(); ;
        }

        foreach(Hitbox h in hitboxList)
        {
            Dropdown.OptionData newOpt = new Dropdown.OptionData();
            newOpt.text = h.getDescriptor().number.ToString();
            myDropdown.options.Add(newOpt);
        }

    }

    public void chooseNewHitbox(int newValue)
    {
        //Update Label Values, Highlight hitbox
    }

    // Use this for initialization
    void Start () {
        myDropdown.onValueChanged.AddListener(chooseNewHitbox);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myDropdown = GetComponentInParent<Dropdown>();
    }
}
