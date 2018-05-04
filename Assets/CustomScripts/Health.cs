using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int startHealth = 200;
    public int health;

    public ParticleSystem onDeathEffect;

	// Use this for initialization
	void Start () {
        health = startHealth;
	}
	
	public void DealDamage(int damageToDeal)
    {
        Debug.Log("Dealing damage: " + damageToDeal);
        health -= damageToDeal;

        if(health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Respawn respawnScript = GetComponent<Respawn>();

        if (respawnScript != null)
        {
            health = startHealth;
            respawnScript.RespawnAfterDelay();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
