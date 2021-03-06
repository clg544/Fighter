﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class AttackDictionary : MonoBehaviour {

    private Dictionary<string, AttackDescriptor> AttackList;
        
    public class AttackDescriptor
    {
        public string attackName;
        public SortedList<int, Hitbox.HitboxDescriptor> attackList;

        public bool Comparator(AttackDescriptor a, AttackDescriptor b)
        {
            return (string.Compare(a.attackName, b.attackName) == 0);
        }
    }
    
    public override string ToString()
    {

        string s = "";

        char tab = '\t';
        string doubleTab = "\t\t";
        
        foreach(AttackDescriptor descriptor in AttackList.Values)
        {
            s += descriptor.attackName + ":\n";

            foreach (Hitbox.HitboxDescriptor hb in descriptor.attackList.Values)
            {
                s += tab + "HitBox " + hb.number + ":\n\n";
                s += tab + "Critical Traits:\n";

                Debug.Log("Rotation" + hb.rotation);

                Debug.Log(hb.number);
                s += doubleTab + "Number=" + hb.number + '\n';
                s += doubleTab + "Length=" + hb.length + '\n';
                s += doubleTab + "Width=" + hb.width + '\n';
                s += doubleTab + "Rotation=" + hb.rotation + '\n';
                s += doubleTab + "Duration=" + hb.duration + '\n';
                s += doubleTab + "DamageAmount=" + hb.damage + '\n';
                s += doubleTab + "DamageType=" + hb.damageType + '\n';

                s += tab + "Optional Traits:\n";

                LinkedListNode<string> k = hb.optionalTraitKeys.First;
                LinkedListNode<string> v = hb.optionalTraitValues.First;
                while(k != null)
                {
                    s += doubleTab + k.Value + '=' + v.Value + '\n';
                    k = k.Next;
                }

                s += '\n';
            }
        }
        
        return s;
    }

    private int readAttackFile(string filePath)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.PreserveWhitespace = false;
        myDoc.Load(filePath);

        XmlNode curNode = myDoc.FirstChild;

        // Fetch all of the top level children
        XmlNodeList XmlAttackList = curNode.SelectNodes("Attack");
        
        // For each attack...
        foreach(XmlNode curAttack in XmlAttackList)
        {

            // Get the attack descriptor ready
            AttackDescriptor newAttack = new AttackDescriptor();

            // Create the data 
            newAttack.attackName = curAttack.SelectSingleNode("Name").InnerText;
            newAttack.attackList = new SortedList<int, Hitbox.HitboxDescriptor>();

            // Add to the list
            this.AttackList.Add(newAttack.attackName, newAttack);

            // Get Element list to extract information
            XmlNodeList HitboxList = curAttack.SelectNodes("Hitbox");
            
            foreach (XmlNode curHitbox in HitboxList)
            {

                /* Set Up the new Hitbox for use right away */
                Hitbox.HitboxDescriptor newHitbox = new Hitbox.HitboxDescriptor();
                newHitbox.optionalTraitKeys = new LinkedList<string>();
                newHitbox.optionalTraitValues = new LinkedList<string>();

                /* Prioritize the hitbox number */
                newHitbox.number = int.Parse(curHitbox.SelectSingleNode("Number").InnerText);

                XmlNodeList valueList = curHitbox.ChildNodes;

                foreach(XmlNode value in valueList)
                {
                    switch (value.Name)
                    {
                        /* Non-Default cases are critical, and stored directly in the class */
                        case ("X"):
                            newHitbox.x = float.Parse(value.InnerText);
                            break;

                        case ("Y"):
                            newHitbox.y = float.Parse(value.InnerText);
                            break;

                        case ("Number"):
                            newHitbox.number = int.Parse(value.InnerText);
                            break;

                        case ("Length"):
                            newHitbox.length = float.Parse(value.InnerText);
                            break;

                        case ("Width"):
                            newHitbox.width = float.Parse(value.InnerText);
                            break;

                        case ("Rotation"):
                            newHitbox.rotation = float.Parse(value.InnerText);
                            break;

                        case ("Duration"):
                            newHitbox.duration = int.Parse(value.InnerText);
                             break;

                        case ("DamageAmount"):
                            newHitbox.damage = int.Parse(value.InnerText);
                            break;

                        case ("DamageType"):
                            newHitbox.damageType = (Hitbox.DAMAGE_TYPE)int.Parse(value.InnerText);
                           break;

                        /* Non-critical traits are stored in a list to support future growth */
                        default:
                            newHitbox.optionalTraitKeys.AddLast(value.Name);
                            newHitbox.optionalTraitValues.AddLast(value.InnerText);
                            break;
                    }
                }
                /* Insert the new hitbox based on it's number */
                newAttack.attackList.Add(newHitbox.number, newHitbox);
            }
        }

        return 0;
    }

    public AttackDescriptor FindAttack(string s)
    {
        AttackDescriptor foundVal;

        if (AttackList.TryGetValue(s, out foundVal))
        {
            return foundVal;
        }
        else
        {
            return null;
        }
    }
    public Dictionary<string, AttackDescriptor> getAttackList()
    {
        return this.AttackList;
    }

    // Use this for initialization
    void Awake ()
    {
        AttackList = new Dictionary<string, AttackDescriptor>();

        /* Create the AttackDictionary from an XML File */
        readAttackFile(".\\Assets\\Data\\AttackList.xml");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
