using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
   
    public ProjectileParameters(ProjectileType _type, float _fuse, float _speed, float _detonateForce, float _radius, int _damage)
    {
        type = _type;
        fuse = _fuse;
        speed = _speed;
        detonationForce = _detonateForce;
        radius = _radius;
        damage = _damage;

        ImportProjectileDefaults();
    }

    private void Awake()
    {
        
    }

    void ImportProjectileDefaults()
    {
        kineticProjectile = Resources.Load("prefabs/kineticprojectile") as GameObject;
        explosiveProjectile = Resources.Load("prefabs/explosiveprojectile") as GameObject;
        implosiveProjectile = Resources.Load("prefabs/explosiveprojectile") as GameObject;
    }

    public GameObject GetProjectile()
    {

        ImportProjectileDefaults();

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
