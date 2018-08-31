using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunControl : MonoBehaviour {
	public float sunRotationMin;
	public float sunRotationMax;
	public MicrophoneInput micInput;
	float sunHeight;

	// Use this for initialization
	void Start () {
		sunHeight = this.transform.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		float freq = micInput.frequency;
		
		if(freq > 200){
			float newSunHeight = GetSunHeight(freq);
			this.transform.localEulerAngles = new Vector3(
				// Mathf.Clamp(
		 		// Mathf.Lerp(sunHeight,newSunHeight, Time.deltaTime), 
				//  	sunRotationMin, 
				// 	sunRotationMax),
				Mathf.Lerp(sunHeight,newSunHeight -40, Time.deltaTime),
		 		this.transform.rotation.y,
		 		this.transform.rotation.z);
			sunHeight = this.transform.localEulerAngles.x;
		}
		
	}

	float GetSunHeight(float freq){
		
		float sunCalc = ((freq / 527) * 40);

		return sunCalc;
	}
}
