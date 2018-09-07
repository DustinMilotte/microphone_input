using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] objectsToSpawn;
    public float spawnRate =3f;
    private float timeSinceLastSpawn;
    public bool majorChordPlayed = false;
    bool canSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(majorChordPlayed && canSpawn)
        {
            canSpawn = false;
            timeSinceLastSpawn = 0f;
            for(int i=0; i < 100; i++){
                GameObject obj = Instantiate(
                    objectsToSpawn[0],
                    transform.position,
                    Quaternion.identity
                );
            var rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.down * 1000);   
            }
        } else if (!canSpawn){
            timeSinceLastSpawn += Time.deltaTime;
        }
        if (timeSinceLastSpawn >= spawnRate){
            canSpawn = true;
            majorChordPlayed = false;
        }
	}
}
