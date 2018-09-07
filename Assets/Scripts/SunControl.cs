using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunControl : MonoBehaviour {
	public float sunRotationMin;
	public float sunRotationMax;
	public MicrophoneInput micInput;
	public float speed;
	float sunRotX;

	// Use this for initialization
	void Start () {
		sunRotX = this.transform.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		float freq = micInput.frequency;
		
		if(freq > 200){
			float currentSunRotX = Mathf.Lerp(sunRotX, GetSunRotX(freq), Time.deltaTime * speed);
			this.transform.localEulerAngles = new Vector3(
				// Mathf.Clamp(
		 		// Mathf.Lerp(sunHeight,newSunHeight, Time.deltaTime), 
				//  	sunRotationMin, 
				// 	sunRotationMax),
				// Mathf.Lerp(sunRotX, currentSunRotX, Time.deltaTime),
				currentSunRotX,
		 		330f,
		 		this.transform.rotation.z
			);
			sunRotX = currentSunRotX;
		}

	}

	float GetSunRotX(float freq){
		// take frequency and make it affect x rotation from max to min;
		// 215 = dark, 100 = light

		// will yield  from 0 to 1;
		float sunCalc = (freq / 263f) -1f;
		sunCalc = Mathf.Clamp(sunCalc, 0 , 1f);
		float adjustedSunHeight = 215f - (115F * sunCalc);

		return adjustedSunHeight;
	}
}
