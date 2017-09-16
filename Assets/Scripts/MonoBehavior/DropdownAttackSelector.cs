using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropdownAttackSelector : MonoBehaviour {

    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private GameObject hitTemplate;
    [SerializeField]
    private GameObject player;

    AttackDictionary myAttacks;

    private Dropdown myDropdown;
    [SerializeField]
    private Dropdown hitboxDropdown;
    private DropdownHitboxSelector hitboxSelector;

    public void DisplayAttack(int newPos)
    {

        GameObject[] activeBox = GameObject.FindGameObjectsWithTag("HitBox");

        foreach(GameObject h in activeBox)
        {
            Destroy(h);
        }

        if (string.Compare(myDropdown.captionText.text, "") != 0)
        {
            AttackDictionary.AttackDescriptor curAttack = myAttacks.FindAttack(myDropdown.captionText.text);

            foreach (Hitbox.HitboxDescriptor curHitbox in curAttack.attackList.Values)
            {
                Vector3 myLocation = new Vector3(curHitbox.x, curHitbox.y, 10);

                GameObject h = Instantiate(hitTemplate, myLocation, new Quaternion()) as GameObject;

                h.transform.parent = player.transform;

                h.transform.localPosition = myLocation;
            }
        }

        hitboxSelector.UpdateHitboxDropdown();
    }

	// Use this for initialization
	void Start () {
        myAttacks = manager.GetComponent<AttackDictionary>();
        hitboxSelector = (DropdownHitboxSelector)hitboxDropdown.GetComponent<DropdownHitboxSelector>();
            
        myDropdown.onValueChanged.AddListener(DisplayAttack);
        Dropdown.OptionData curOption = new Dropdown.OptionData();
        
        /* Turn the Attack Dictionary list into an Option Menu */
        foreach (AttackDictionary.AttackDescriptor node in myAttacks.getAttackList().Values)
        {
            curOption = new Dropdown.OptionData();
            curOption.text = node.attackName;
            myDropdown.options.Add(curOption);
        }

        myDropdown.value = 0;
	}
	
	// Update is called once per frame
	void Update () {

    }
}
