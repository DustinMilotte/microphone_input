using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	public float shrinkFactor;

	private void Update() {
		ShrinkOverTime();
	}

    private void ShrinkOverTime()
    {
        this.transform.localScale *= shrinkFactor ;
    }
}
