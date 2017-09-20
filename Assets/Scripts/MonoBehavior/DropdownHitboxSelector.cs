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

    private SortedList<int, Hitbox.HitboxDescriptor> curList;
    Hitbox.HitboxDescriptor curHitbox;

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
    [SerializeField]
    private Dropdown  Type_Dropdown;
    [SerializeField]
    private Dropdown Add_Tag_Dropdown;
    [SerializeField]
    private Dropdown Add_Value_Dropdown;
    [SerializeField]
    private Dropdown Remove_Tag_Dropdown;



    public void UpdateHitboxDropdown(SortedList<int, Hitbox.HitboxDescriptor> newList)
    {
        myDropdown.ClearOptions();
        
        AttackDictionary.AttackDescriptor curAttack = atkDictionary.FindAttack(attackSelector.captionText.text);
        curList = curAttack.attackList;

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
        if (!(curList.TryGetValue(newValue, out curHitbox)))
            throw new System.NullReferenceException();
        

        XPos_Textbox.text = curHitbox.x.ToString();
        YPos_Textbox.text = curHitbox.y.ToString();
        Length_Textbox.text = curHitbox.length.ToString();
        Width_Textbox.text = curHitbox.width.ToString();
        Rotation_Textbox.text = curHitbox.rotation.ToString();
        Duration_Textbox.text = curHitbox.duration.ToString();
        Damage_Textbox.text = curHitbox.damage.ToString();

        Type_Dropdown.value = (int)curHitbox.damageType;




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
