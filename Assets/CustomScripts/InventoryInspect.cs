using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryInspect : MonoBehaviour {

    [SerializeField]
    public TextMeshProUGUI weaponType;

    [SerializeField]
    public TextMeshProUGUI damage;

    [SerializeField]
    public TextMeshProUGUI fireRate;

    [SerializeField]
    public TextMeshProUGUI reloadSpeed;

    [SerializeField]
    public TextMeshProUGUI range;

    public GunParameters gunParams;

    // Update is called once per frame
    void Update () {

        if(gunParams == null)
        {
            return;
        }

        weaponType.text = gunParams.gunType.ToString();
        
        if(gunParams.gunType == GunType.Hitscan)
        {
            damage.text = "Damage: " + gunParams.damage;
            range.text = "Range: " + (int)gunParams.hitscanRange;
        } else
        {
            damage.text = "Damage: " + gunParams.projectileParameters.damage;
            range.text = "Range: " + (int)gunParams.projectileParameters.radius;
        }

        fireRate.text = "Rate of fire: " + gunParams.rateOfFire;

        reloadSpeed.text = "Reload speed: " + gunParams.reloadSpeed;

	}
}
