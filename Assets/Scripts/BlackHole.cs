using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

  	const float G = 667.4f;
	Rigidbody rb;

  	private void Start() {
		  rb = GetComponent<Rigidbody>();
  	}
	
	private void FixedUpdate() {
		Note [] notes = FindObjectsOfType<Note>();
		foreach(Note note in notes){
			Attract(note);
		}

	}
	
  	void Attract (Note note){
	  Rigidbody rbToAttract = note.GetComponent<Rigidbody>();
	  Vector3 direction = rb.position - rbToAttract.position;
	  float distance = direction.magnitude;

	  if (distance == 0f) return;

	  float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
	  Vector3 force = direction.normalized * forceMagnitude;

	  rbToAttract.AddRelativeForce(force);
  	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Note"){
			Destroy(other.gameObject);
		}
	}
	
}
