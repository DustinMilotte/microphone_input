using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public float horizontalSpeed;
    public float verticalSpeed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, horizontalSpeed * Time.deltaTime);
        transform.Rotate(Vector3.left, verticalSpeed * Time.deltaTime);
	}
}
