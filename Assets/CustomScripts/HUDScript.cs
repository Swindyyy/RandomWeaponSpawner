using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    public static HUDScript HUDSingleton;
    public GameObject pickupWeapon;
    public GameObject hud;
    public GameObject inventory;
    bool isInventoryActive = false;

    bool isOpenInvAxisInUse = false;
    private void Awake()
    {
        if(HUDSingleton == null)
        {
            HUDSingleton = this;
        } else
        {
            Destroy(this);
        }
    }


    // Update is called once per frame
    void FixedUpdate () {
		if(Input.GetAxis("OpenInventory") > 0)
        {
            if (isOpenInvAxisInUse == false)
            {
                isInventoryActive = !isInventoryActive;
                hud.SetActive(!isInventoryActive);
                inventory.SetActive(isInventoryActive);
                isOpenInvAxisInUse = true;
            }
        }

        if(Input.GetAxis("OpenInventory") == 0)
        {
            isOpenInvAxisInUse = false;
        }
	}

    public void EnablePickupWeaponText()
    {
        pickupWeapon.SetActive(true);
    }

    public void DisablePickupWeaponText()
    {
        pickupWeapon.SetActive(false);
    }


}
