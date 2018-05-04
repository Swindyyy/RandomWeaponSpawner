using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    public static HUDScript HUDSingleton;
    public GameObject pickupWeapon;

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
    void Update () {
		
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
