using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour {

    float pickupTime = 0.0f;

    [SerializeField]
    float pickupTimeLimit = 2.5f;

    private void Update()
    {
        pickupTime = pickupTime + Time.deltaTime;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("GunWall"))
        {
            HUDScript.HUDSingleton.EnablePickupWeaponText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("GunWall"))
        {
            HUDScript.HUDSingleton.DisablePickupWeaponText();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("GunWall"))
        {
            if (Input.GetAxis("EquipWeapon") > 0  && pickupTime > pickupTimeLimit)
            {
                pickupTime = 0.0f;
                GunParameters gunToEquip = other.gameObject.GetComponent<GunScript>().GetGunParameters();
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript>().AddWeaponToInventory(gunToEquip);
                if (other.gameObject.CompareTag("Gun"))
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
