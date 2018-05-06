using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour {

    public InventoryInspect slot1;

    public InventoryInspect slot2;
	
	// Update is called once per frame
	void Update () {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");

        if(inventory != null)
        {
            InventoryScript script = inventory.GetComponent<InventoryScript>();

            int slotCounter = 0;

            foreach(GunParameters gp in script.GetWeaponsInInventory())
            {
                slotCounter += 1;

                if(slotCounter == 1)
                {
                    if(gp != null)
                    {
                        slot1.gunParams = gp;
                    }
                } else if(slotCounter == 2)
                {
                    if(gp != null)
                    {
                        slot2.gunParams = gp;
                    }
                }
            }

        }
	}
}
