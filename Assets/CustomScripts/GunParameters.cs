using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunParameters {


    int damage { get; set; }
   
    int maxAmmo { get; set; }

    float rateOfFire { get; set; }

    float reloadSpeed { get; set; }

    public GunParameters(int _damage, int _maxAmmo, float _rateOfFire, float _reloadSpeed)
    {
        damage = _damage;
        maxAmmo = _maxAmmo;
        rateOfFire = _rateOfFire;
        reloadSpeed = _reloadSpeed;
    }


}
