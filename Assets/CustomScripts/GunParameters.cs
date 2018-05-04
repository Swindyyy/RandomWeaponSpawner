using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType { Hitscan, Projectile }

[System.Serializable]
[CreateAssetMenu(fileName = "Gun parameters", menuName = "Gun/Gun Parameters")]
public class GunParameters : ScriptableObject {

    public GunType gunType = GunType.Hitscan;
    public int damage;
    public int currentAmmo;
    public int maxAmmo;
    public float rateOfFire;
    public float reloadSpeed;
    public float hitscanRange;
    public float bulletImpactModifier = 15f;
    public GameObject currentGunObject;
    public Sprite gunIcon;
    public Vector3 camOffset;
    public ProjectileParameters projectileParameters;

    public GunParameters(GunType _type, int _damage, int _maxAmmo, float _rateOfFire, float _reloadSpeed, float _hitscanRange, GameObject _object, ProjectileParameters _projectile)
    {
        gunType = _type;
        damage = _damage;
        maxAmmo = _maxAmmo;
        rateOfFire = _rateOfFire;
        reloadSpeed = _reloadSpeed;
        hitscanRange = _hitscanRange;
        currentGunObject = _object;
        projectileParameters = _projectile;
    }


}
