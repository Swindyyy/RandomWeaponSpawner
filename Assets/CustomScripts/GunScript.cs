using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour {

    [SerializeField]
    GunParameters gunParameters;

    [SerializeField]
    bool isReloading = false;

    float nextTimeToFire = 0.0f;

    bool isEquiped = false;

    float unequipThrow = 250f;
    
	// Use this for initialization
	public void Equip()
    {
        isReloading = false;
        isEquiped = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Unequip()
    {
        isEquiped = false;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * unequipThrow);
    }

    // Update is called once per frame
    void Update() {
       nextTimeToFire = nextTimeToFire + Time.deltaTime;

        if (isReloading || !isEquiped)
        {
            return;
        }

        if (gunParameters.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

		if(Input.GetButton("Fire1") && (nextTimeToFire > gunParameters.rateOfFire))
        {
            Fire();
            nextTimeToFire = 0f;
        }
	}

    public void Fire()
    {
        if (!isReloading)
        {
            gunParameters.currentAmmo -= 1;

            if (gunParameters.gunType == GunType.Hitscan)
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
        RaycastHit hit;

        bool didRaycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, gunParameters.hitscanRange);

        if(didRaycastHit)
        {
            Health healthScript = hit.collider.gameObject.GetComponent<Health>();

            if(healthScript != null)
            {
                healthScript.DealDamage(gunParameters.damage);
            }

            ParticleSystem muzzleFlash = GetComponentInChildren<ParticleSystem>();

            if(muzzleFlash != null)
            {
                muzzleFlash.Play();
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(Camera.main.transform.forward * (gunParameters.damage * gunParameters.bulletImpactModifier));
            }
        }
    }

    void FireProjectile()
    {
        GameObject projectileSpawnObject = gameObject.transform.Find("ProjectileSpawn").gameObject;        
        ProjectileParameters projectileParams = gunParameters.projectileParameters;

        if (projectileSpawnObject != null)
        {
            Vector3 spawn = projectileSpawnObject.transform.position;
            GameObject projectile = Instantiate(projectileParams.GetProjectile(), spawn, Quaternion.Euler(Camera.main.transform.forward));
            projectile.transform.rotation = Quaternion.Euler(Camera.main.transform.forward);
            Rigidbody rb = projectile.gameObject.GetComponent<Rigidbody>();
            
            if(rb != null)
            {
                rb.AddForce(transform.forward * projectileParams.shotForce);
                ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
                if(projectileScript != null)
                {
                    projectileScript.SetParameters(projectileParams.type, projectileParams.fuse, projectileParams.detonationForce, projectileParams.radius, projectileParams.damage);
                } else
                {
                    Destroy(projectile);
                    Debug.Log("No projectile script attached, destroying projectile");
                }
            }
        }
        
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(gunParameters.reloadSpeed);

        if (isEquiped)
        {
            gunParameters.currentAmmo = gunParameters.maxAmmo;
            isReloading = false;
        }
    }

    public void SetGunParameters(GunParameters parametersToSet)
    {
        gunParameters = parametersToSet;
    }

    public GunParameters GetGunParameters()
    {
        return gunParameters;
    }


}
