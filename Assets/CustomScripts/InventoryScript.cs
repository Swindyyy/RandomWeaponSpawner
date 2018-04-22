using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    List<GameObject> weaponsInInventory = new List<GameObject>();

    GameObject currentWeapon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<GameObject> GetWeaponsInInventory()
    {
        return weaponsInInventory;
    }


    public void AddWeaponToInventory(GameObject obj)
    {
            
    }
}
