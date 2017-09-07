using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class AttackDictionary : MonoBehaviour {

    private LinkedList<attackDescriptor> AttackList;
        
    public class attackDescriptor
    {
        public string attackName;
        public SortedList<Hitbox, int> attackList;
    }


    private int readAttackFile(string filePath)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.PreserveWhitespace = true;
        myDoc.Load(filePath);

        XmlNode curNode = myDoc.FirstChild;

        Debug.Log(curNode.Name);

        // Fetch all of the top level children
        XmlNodeList XmlAttackList = curNode.ChildNodes;
        
        // For each attack...
        foreach(XmlNode curAttack in XmlAttackList)
        {
            // Get the attack descriptor ready
            attackDescriptor newAttack = new attackDescriptor();
            this.AttackList.AddLast(newAttack);
            newAttack.attackList = new SortedList<Hitbox, int>();

            // Name the attack with the Name Value
            newAttack.attackName = curAttack.Name;

            // Get Element list to extract information
            XmlNodeList HitboxList = curAttack.SelectNodes("Hitbox");
            
            foreach (XmlNode curHitbox in HitboxList)
            {
                Hitbox newHitbox = new Hitbox();
                XmlNodeList valueList = curHitbox.ChildNodes;

                foreach(XmlNode value in valueList)
                {
                    Debug.Log(value.Name);
                    Debug.Log(value.InnerText);

                    switch (value.Name)
                    {
                        case ("Number"):
                            newHitbox.number = int.Parse(value.InnerText);
                            break;

                        case ("Length"):
                            newHitbox.length = int.Parse(value.InnerText);
                            break;

                        case ("Width"):
                            newHitbox.width = int.Parse(value.InnerText);
                            break;

                        case ("Rotation"):
                            newHitbox.rotation = int.Parse(value.InnerText);
                            break;

                        case ("Duration"):
                            newHitbox.number = int.Parse(value.InnerText);
                            break;

                        case ("DamageAmount"):
                            newHitbox.damage = int.Parse(value.InnerText);
                            break;

                        case ("DamageType"):
                            newHitbox.damageType = (Hitbox.DAMAGE_TYPE)int.Parse(value.InnerText);
                            break;

                        case ("isPoisonous"):
                            newHitbox.isPoisonous = bool.Parse(value.InnerText);
                            break;
                    }
                }
            }
        }

        return 0;
    }



	// Use this for initialization
	void Awake () {
        AttackList = new LinkedList<attackDescriptor>();

        try
        {
            readAttackFile(".\\Assets\\Data\\AttackList.xml");
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
