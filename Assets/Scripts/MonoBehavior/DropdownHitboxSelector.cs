using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHitboxSelector : MonoBehaviour {

    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private Dropdown attackSelector;
    
    private AttackDictionary atkDictionary;
    private Dropdown myDropdown;

    private LinkedList<Hitbox.HitboxDescriptor> curList;

    [SerializeField]
    private InputField XPos_Textbox;
    [SerializeField]
    private InputField YPos_Textbox;
    [SerializeField]
    private InputField Length_Textbox;
    [SerializeField]
    private InputField Width_Textbox;
    [SerializeField]
    private InputField Rotation_Textbox;
    [SerializeField]
    private InputField Duration_Textbox;
    [SerializeField]
    private InputField Damage_Textbox;

    public void UpdateHitboxDropdown()
    {
        myDropdown.ClearOptions();
        
        Debug.Log("CaptionText = " + attackSelector.captionText.text);
        
        AttackDictionary.AttackDescriptor curAttack = atkDictionary.FindAttack(attackSelector.captionText.text);
        {
            int i = 1;
            {
                foreach (Hitbox.HitboxDescriptor hitbox in curAttack.attackList.Values)
                {
                    Dropdown.OptionData newOpt = new Dropdown.OptionData();
                    newOpt.text = i.ToString();
                    myDropdown.options.Add(newOpt);

                    i++;
                }
            }
        }
    }

    public void chooseNewHitbox(int newValue)
    {
        //Update Label Values, Highlight hitbox

    }

    // Use this for initialization
    void Start () {
        myDropdown = this.GetComponent<Dropdown>();
        atkDictionary = manager.GetComponent<AttackDictionary>();

        myDropdown.onValueChanged.AddListener(chooseNewHitbox);
    }
	
	// Update is called once per frame
	void Update ()
    {
        myDropdown = GetComponentInParent<Dropdown>();
    }
}
