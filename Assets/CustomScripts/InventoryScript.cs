using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    public int inventorySize = 2;

    [SerializeField]
    List<GunParameters> weaponsInInventory = new List<GunParameters>();

    [SerializeField]
    GameObject currentWeapon;

    int currentWeaponIndex = 0;

	// Use this for initialization
	void Start () {
		if(currentWeapon == null)
        {
            EquipWeapon();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("WeaponSlot1") > 0 && currentWeaponIndex != 0)
        {
            currentWeaponIndex = 0;
            GunParameters gunToEquip = weaponsInInventory[currentWeaponIndex];

            if (gunToEquip != null)
            {
                EquipWeapon();
            }
        }

        if (Input.GetAxis("WeaponSlot2") > 0 && currentWeaponIndex != 1)
        {
            currentWeaponIndex = 1;
            GunParameters gunToEquip = weaponsInInventory[currentWeaponIndex];

            if (gunToEquip != null)
            {
                EquipWeapon();
            }
        }

        if(Input.GetAxis("UnequipWeapon") > 0)
        {
            UnequipCurrentWeapon();
        }
    }

    public List<GunParameters> GetWeaponsInInventory()
    {
        return weaponsInInventory;
    }

    public void AddWeaponToInventory(GunParameters weaponToAdd)
    {
        for(int i = 0; i < weaponsInInventory.Count; i++)
        {
            if(weaponsInInventory[i] == null)
            {
                weaponsInInventory[i] = weaponToAdd;
                if(i == currentWeaponIndex)
                {
                    EquipWeapon();
                }

                return;
            }
        }

        if (weaponsInInventory.Count >= inventorySize)
        {
            UnequipCurrentWeapon();
            weaponsInInventory[currentWeaponIndex] = weaponToAdd;
            EquipWeapon();
            
        } else
        {
            weaponsInInventory.Add(weaponToAdd);
        }
    }

    public void EquipWeapon()
    {
        try
        {
            GameObject currentWeaponGameObject = gameObject.transform.GetChild(0).gameObject;
            Destroy(currentWeaponGameObject);
        } catch
        {
            Debug.Log("No weapon equipped, cannot destroy child object");
        }

        GunParameters weaponToEquip = weaponsInInventory[currentWeaponIndex];
        if (weaponToEquip != null)
        {
            GameObject weaponToEquipObject = Instantiate(weaponToEquip.currentGunObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            currentWeapon = weaponToEquipObject;
            weaponToEquipObject.GetComponent<GunScript>().Equip();
            gameObject.transform.localPosition = currentWeapon.GetComponent<GunScript>().GetGunParameters().camOffset;
        } else
        {
            Debug.Log("No weapon to equip");
        }
        
    }

    public void UnequipCurrentWeapon()
    {
        currentWeapon.GetComponent<GunScript>().Unequip();
        try
        {
            weaponsInInventory[currentWeaponIndex] = null;
        } catch
        {
            Debug.Log("No weapon in slot");
        }
    }


}
