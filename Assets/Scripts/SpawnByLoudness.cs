using UnityEngine;
using System.Collections;

public class SpawnByLoudness : MonoBehaviour {

	// object that has our microphone input script
    public GameObject audioInputObject;
    public float threshold = 1.0f; // threshold to spawn object
    public GameObject objectToSpawn;
    MicrophoneInput micIn;
	bool canSpawn = true;
	public float spawnRate = .5f;
	float timeSinceLastSpawn = 0f;
    public float thrust = 1f;
    public float currentNote;

    void Start() {
        if (objectToSpawn == null)
            Debug.LogError("You need to set a prefab Object To Spawn -parameter in the editor!");
        if (audioInputObject == null)
            audioInputObject = GameObject.Find("MicMonitor");
        micIn = (MicrophoneInput) audioInputObject.GetComponent("MicrophoneInput");
    }

    void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		if(timeSinceLastSpawn >= spawnRate){
			canSpawn = true;
		} else {
			canSpawn = false;
		}

        float loudness = micIn.loudness;
        if (loudness > threshold && canSpawn)
        {
            SpawnNote(loudness);
        }


    }

    private void SpawnNote(float loudness)
    {
        Vector3 scale = new Vector3(loudness, loudness, loudness);
        GameObject newObject = (GameObject)Instantiate(objectToSpawn, this.transform.position, this.transform.rotation);
        newObject.transform.localScale += scale;
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * thrust * loudness);
        timeSinceLastSpawn = 0f;
        currentNote = micIn.frequency;
    }
}