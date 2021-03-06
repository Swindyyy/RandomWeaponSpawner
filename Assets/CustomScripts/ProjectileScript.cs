﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    ProjectileType type;

    int detonationDamage;

    float projectileFuse;

    float detonationForce;

    float detonationRadius;

    [SerializeField]
    GameObject explosionEffect;

    [SerializeField]
    AudioSource explosionNoise;


    bool fuseSet = false;

    private void Start()
    {
        explosionNoise = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("type: " + type.ToString());
        if (type == ProjectileType.Kinetic)
        {
            try
            {
                coll.gameObject.GetComponent<Health>().DealDamage(detonationDamage);
            } catch
            {
                Debug.Log("NO HEALTH SCRIPT");
            }
            Destroy(gameObject);
            Debug.Log("DESTROYING PROJECTILE");
        }
    }

    IEnumerator SetFuse()
    {
        yield return new WaitForSeconds(projectileFuse);

        Detonate();
    }

    void Detonate()
    {
        explosionNoise.Play();
        GameObject explosionEffectInstance = Instantiate(explosionEffect, transform.position, transform.rotation);
        ParticleSystem[] explosionParticleSystems = explosionEffectInstance.GetComponents<ParticleSystem>();
        ParticleSystem[] childExplosionParticleSystems = explosionEffectInstance.GetComponentsInChildren<ParticleSystem>();
        float duration = explosionParticleSystems[0].main.duration;

        foreach(ParticleSystem system in explosionParticleSystems)
        {
            system.Play();
        }

        foreach(ParticleSystem system in childExplosionParticleSystems)
        {
            system.Play();
        }

        Destroy(explosionEffectInstance, duration);


        Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, detonationRadius);

        foreach(Collider objectHit in collidersToDamage)
        {
            Health health = objectHit.gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.DealDamage(detonationDamage);
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, detonationRadius);

        foreach (Collider objectHit in collidersToMove)
        {
            Rigidbody rb = objectHit.gameObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(detonationForce, transform.position, detonationRadius);
            }
        }

        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }

    public void SetParameters(ProjectileType typeToSet, float fuseToSet, float forceToSet, float radiusToSet, int damageToSet)
    {
        type = typeToSet;
        projectileFuse = fuseToSet;
        detonationForce = forceToSet;
        detonationRadius = radiusToSet;
        detonationDamage = damageToSet;

        if (!fuseSet && type != ProjectileType.Kinetic)
        {
            StartCoroutine(SetFuse());
            fuseSet = true;
        }
    }
}
