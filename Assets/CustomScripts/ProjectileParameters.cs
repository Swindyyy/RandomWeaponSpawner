using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Kinetic, Explosive, Implosive }

[System.Serializable]
[CreateAssetMenu(fileName = "Projectile parameters", menuName = "Gun/Projectile Parameters")]
public class ProjectileParameters : ScriptableObject {

    public GameObject kineticProjectile;
    public GameObject explosiveProjectile;
    public GameObject implosiveProjectile;
    public ProjectileType type;
    public float fuse = 5.0f;
    public float speed = 15f;
    public float shotForce = 5f;
    public float detonationForce = 5f;
    public float radius = 10f;
    public int damage = 100;

    public GameObject GetProjectile()
    {
        switch (type)
        {
            case ProjectileType.Explosive:
                return explosiveProjectile;
            case ProjectileType.Implosive:
                return implosiveProjectile;
            case ProjectileType.Kinetic:
                return kineticProjectile;
            default:
                return null;
        }
    }
    
}
