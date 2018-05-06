using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GunCriteriaToRandomise { GunType, Damage, MaxAmmo, RateOfFire, ReloadSpeed, HitscanRange, MeshObject}

public enum ProjectileCriteriaToRandomise { ProjectileType, Damage, Fuse, Force, Radius}

public class WeaponSpawner : MonoBehaviour {

    [SerializeField]
    float gunSpawnerInterval = 120f;

    [SerializeField]
    float countPeriod = 0.1f;

    public float currentTimer = 0.0f;

    [SerializeField]
    float gunDamageVariationModifier = 0.5f;

    [SerializeField]
    float gunMaxAmmoVariationModifier = 0.5f;

    [SerializeField]
    float gunRateOfFireVariationModifier = 0.5f;

    [SerializeField]
    float gunReloadSpeedVariationModifier = 0.5f;

    [SerializeField]
    float gunHitscanRangeVariationModifier = 0.5f;

    [SerializeField]
    List<GameObject> listOfGunObjects = new List<GameObject>();

    [Range(0f,1f)]
    float generateNewMeshProbability = 0.1f;

    [SerializeField]
    float projectileDamageModifier = 0.5f;

    [SerializeField]
    float projectileFuseModifier = 0.5f;

    [SerializeField]
    float projectileForceModifier = 0.5f;

    [SerializeField]
    float projectileRadiusModifier = 0.5f;

    [SerializeField]
    float projectileSpeedModifier = 0.5f;

    bool weaponSpawned = false;

    bool timerActive = false;

    [SerializeField]
    TextMeshPro counterText;
	
	// Update is called once per frame
	void Update () {
		if(weaponSpawned == false && timerActive == false)
        {
            if(currentTimer > gunSpawnerInterval)
            {
                StartCoroutine(CreateNewGun());
                
                counterText.text = "Weapon spawned";
            }
            else {
                StartCoroutine(Countdown());
                counterText.text = "New weapon spawning: " + (int)(gunSpawnerInterval - currentTimer);
            }            
        }         
	}

    IEnumerator Countdown()
    {
        timerActive = true;
        yield return new WaitForSeconds(countPeriod);
        currentTimer += countPeriod;
        timerActive = false;
    }

    IEnumerator CreateNewGun()
    {
        GunParameters newGunParamters = GenerateNewGunParameters();
        SpawnNewGun(newGunParamters);
        yield return null;
    }

    GunParameters GenerateNewGunParameters()
    {
        GameObject inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        GunParameters gunParameters1;
        GunParameters gunParameters2;
        GunParameters newParameters = null;

        if (inventoryObject != null)
        {
            InventoryScript inventory = inventoryObject.GetComponent<InventoryScript>();
            List<GunParameters> listOfGunParameters = inventory.GetWeaponsInInventory();
            int numOfEquippedGuns = 0;

            foreach(GunParameters gp in listOfGunParameters)
            {
                if(gp != null)
                {
                    numOfEquippedGuns += 1;
                }
            }

            if (numOfEquippedGuns >= 2)
            {
                gunParameters1 = listOfGunParameters[0];
                gunParameters2 = listOfGunParameters[1];

                GunType newGunType = GenerateNewGunType();
                int newDamage = GenerateNewGunDamage(gunParameters1.damage, gunParameters2.damage);
                int newMaxAmmo = GenerateNewGunMaxAmmo(gunParameters1.maxAmmo, gunParameters2.maxAmmo);
                float newRateOfFire = GenerateNewGunRateOfFire(gunParameters1.rateOfFire, gunParameters2.rateOfFire);
                float newReloadSpeed = GenerateNewGunReloadSpeed(gunParameters1.reloadSpeed, gunParameters2.reloadSpeed);
                float newHitscanRange = GenerateNewGunHitscanRange(gunParameters1.hitscanRange, gunParameters2.hitscanRange);
                GameObject newGameObject = GenerateNewGameObject(gunParameters1.currentGunObject, gunParameters2.currentGunObject);
                ProjectileParameters newProjectileParams = GenerateNewProjectileParameters(gunParameters1.projectileParameters, gunParameters2.projectileParameters);
                newParameters = new GunParameters(newGunType, newDamage, newMaxAmmo, newRateOfFire, newReloadSpeed, newHitscanRange, newGameObject, newProjectileParams);
            }
            else if (numOfEquippedGuns == 1)
            {
                foreach(GunParameters gp in listOfGunParameters)
                {
                    if(gp != null)
                    {
                        newParameters = gp;
                    }
                }

            }
        } 

        return newParameters;
    }

    ProjectileParameters GenerateNewProjectileParameters(ProjectileParameters pp1, ProjectileParameters pp2)
    {
        ProjectileParameters newParams;
        ProjectileType newType = GenerateNewProjectileType();
        float newFuse = GenerateNewProjectileFuse(pp1.fuse, pp2.fuse);
        float newSpeed = GenerateNewProjectileSpeed(pp1.speed, pp2.speed);
        float newForce = GenerateNewProjectileForce(pp1.detonationForce, pp2.detonationForce);
        float newRadius = GenerateNewProjectileRadius(pp1.radius, pp2.radius);
        int newDamage = GenerateNewProjectileDamage(pp1.damage, pp2.damage);

        newParams = new ProjectileParameters(newType, newFuse, newSpeed, newForce, newRadius, newDamage);

        return newParams;
    }

    void SpawnNewGun(GunParameters parametersToUse)
    {
        if (parametersToUse != null)
        {
            GameObject gunToSpawn = Instantiate(parametersToUse.currentGunObject, Vector3.zero, Quaternion.identity);
            gunToSpawn.transform.parent = this.gameObject.transform;
            gunToSpawn.transform.localPosition = Vector3.zero;
            gunToSpawn.GetComponent<GunScript>().SetGunParameters(parametersToUse);
            weaponSpawned = true;
        } else
        {
            Debug.Log("NULL NEW PARAMTERS, CANNOT CREATE NEW GUN");
            weaponSpawned = false;
        }
    }

    GunType GenerateNewGunType()
    {
        float rand = Random.Range(0f, 1f);
        if(rand >= 0.5f)
        {
            return GunType.Hitscan;
        } else
        {
            return GunType.Projectile;
        }
    }

    int GenerateNewGunDamage(int currentValue1, int currentValue2)
    {
        int damage1 = (int)GenerateNewValueBasedOffValueAndModifier(currentValue1, gunDamageVariationModifier);
        int damage2 = (int)GenerateNewValueBasedOffValueAndModifier(currentValue2, gunDamageVariationModifier);

        int newDamage = (damage1 + damage2) / 2;
        return newDamage;
    }

    int GenerateNewGunMaxAmmo(int currentValue1, int currentValue2)
    {
        int maxAmmo1 = (int)GenerateNewValueBasedOffValueAndModifier(currentValue1, gunMaxAmmoVariationModifier);
        int maxAmmo2 = (int)GenerateNewValueBasedOffValueAndModifier(currentValue2, gunMaxAmmoVariationModifier);

        int newMaxAmmo = (maxAmmo1 + maxAmmo2) / 2;
        return newMaxAmmo;
    }

    float GenerateNewGunRateOfFire(float currentValue1, float currentValue2)
    {
        float rateOfFire1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, gunRateOfFireVariationModifier);
        float rateOfFire2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, gunRateOfFireVariationModifier);

        float newRateOfFire = (rateOfFire1 + rateOfFire2) / 2f;
        return newRateOfFire;
    }

    float GenerateNewGunReloadSpeed(float currentValue1, float currentValue2)
    {
        float reloadSpeed1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, gunReloadSpeedVariationModifier);
        float reloadSpeed2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, gunReloadSpeedVariationModifier);

        float newReloadSpeed = (reloadSpeed1 + reloadSpeed2) / 2;
        return newReloadSpeed;
    }

    float GenerateNewGunHitscanRange(float currentValue1, float currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, gunHitscanRangeVariationModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, gunHitscanRangeVariationModifier);

        float newRange = (range1 + range2) / 2;
        return newRange;
    }

    float GenerateNewValueBasedOffValueAndModifier(float currentValue, float modifier)
    {
        float randomRange = currentValue * modifier;
        float rand = Random.Range(-randomRange, randomRange);
        float newValue = currentValue + rand;
        return newValue;
    }

    GameObject GenerateNewGameObject(GameObject go1, GameObject go2)
    {
        float probability = 1 - Random.Range(0f, 1f);
        if(generateNewMeshProbability > probability)
        {
            float rand = Random.Range(0f, listOfGunObjects.Count - 1);

            return listOfGunObjects[(int)rand];
        }
        else {
            float chooseBetweenCurrent = Random.Range(0f, 1f);

            if(chooseBetweenCurrent >= 0.5f)
            {
                return go1;
            }
            else {
                return go2;
            }
        }

    }

    ProjectileType GenerateNewProjectileType()
    {
        float rand = Random.Range(1f, 3.99f);
        ProjectileType newType = ProjectileType.Kinetic;

        if(rand <= 2f)
        {
            newType = ProjectileType.Explosive;
        } else if(rand > 2 && rand <= 3f)
        {
            newType = ProjectileType.Implosive;
        } else
        {
            newType = ProjectileType.Kinetic;
        }

        return newType;
    }

    float GenerateNewProjectileFuse(float currentValue1, float currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, projectileFuseModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, projectileFuseModifier);

        float newRange = (range1 + range2) / 2;
        return newRange;
    }

    float GenerateNewProjectileForce(float currentValue1, float currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, projectileForceModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, projectileForceModifier);

        float newRange = (range1 + range2) / 2;
        return newRange;
    }

    float GenerateNewProjectileRadius(float currentValue1, float currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, projectileRadiusModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, projectileRadiusModifier);

        float newRange = (range1 + range2) / 2;
        return newRange;
    }

    float GenerateNewProjectileSpeed(float currentValue1, float currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, projectileSpeedModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, projectileSpeedModifier);

        float newRange = (range1 + range2) / 2;
        return newRange;
    }

    int GenerateNewProjectileDamage(int currentValue1, int currentValue2)
    {
        float range1 = GenerateNewValueBasedOffValueAndModifier(currentValue1, projectileRadiusModifier);
        float range2 = GenerateNewValueBasedOffValueAndModifier(currentValue2, projectileRadiusModifier);

        float newRange = (range1 + range2) / 2;
        return (int)newRange;
    }
}
