using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {


    Vector3 spawnPosition;
    [SerializeField]
    float respawnTime = 5f;


	// Use this for initialization
	void Start () {
        spawnPosition = transform.position;	
	}
	
	public void MoveObjectToSpawnPosition()
    {
        transform.position = spawnPosition;
    }

    public void MoveObjectToPosition(Vector3 positionToMove)
    {
        transform.position = positionToMove;
    }

    public void RespawnAfterDelay()
    {
        Invoke("MoveObjectToSpawnPosition", respawnTime);
    }
}
