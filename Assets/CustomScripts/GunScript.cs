using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour {

    GunParameters params = new GunParameters();

    [SerializeField]
    bool isHitscan = true;

    [SerializeField]
    int currentAmmo;

    [SerializeField]
    bool isProjectileExplosive;

    [SerializeField]
    float projectileFuse = 5.0f;

    [SerializeField]
    float projectileSpeed = 15f;

    [SerializeField]
    float projectileForce = 5f;

    bool isReloading = false;

    [SerializeField]
    GameObject explosiveProjectile;

    [SerializeField]
    GameObject implosiveProjectile;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool GetIsGunHitscan()
    {
        return isHitscan;
    }

    /// <summary>
    /// Use this function to set parameters for  weapons
    /// </summary>
    /// <param name="_maxAmmo"></param>
    /// <param name="_rateOfFire"></param>
    /// <param name="_reloadSpeed"></param>
    void SetWeaponParameters()
    {

    }

    void SetProjectileParameters(bool _isProjectiveExplosive, float _projectileFuse, float _projectileSpeed, float _projectileForce)
    {
        isProjectileExplosive = _isProjectiveExplosive;
        projectileFuse = _projectileFuse;
        projectileSpeed = _projectileSpeed;
        projectileForce = _projectileForce;
    }

    public void Fire()
    {
        if (!isReloading)
        {
            if(isHitscan)
            {
                FireRaycast();
            } else
            {
                FireProjectile();
            }
        }
    }

    void FireRaycast()
    {

    }


    void FireProjectile()
    {
        if(isProjectileExplosive)
        {

        } else
        {

        }
    }

     GetWeaponParameters




}
