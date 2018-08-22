using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
	public float lifetime = 5;
	float timeSinceBirth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceBirth += Time.deltaTime;
		if(timeSinceBirth >= lifetime){
			Destroy(gameObject);
		}	
	}
}
