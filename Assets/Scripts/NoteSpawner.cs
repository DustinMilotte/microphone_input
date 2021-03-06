﻿using UnityEngine;
using System.Collections;

public class NoteSpawner : MonoBehaviour {

    public GameObject audioInputObject;
    public float threshold = 1.0f; // threshold to spawn object
    public GameObject objectToSpawn;
    MicrophoneInput micIn;
	bool canSpawn = true;
	public float spawnRate = .5f;
	float timeSinceLastSpawn = 0f;
    public float thrust = 1f;
    public float currentNote;
	bool playingNote;
	public float noteMonitor;
	int notePlayedIndex = 0;
	public Material [] noteMaterials;
	public NoteFinder noteFinder;
	public Material newNoteMat;
	bool noteBufferOpen;
	public float noteBufferLength = 3;
	float timeSinceNoteBufferStarted;
	public string noteBuffer = "";
	public Spawner spawner;

    void Start() {
        if (objectToSpawn == null)
            Debug.LogError("You need to set a prefab Object To Spawn -parameter in the editor!");
        if (audioInputObject == null)
            audioInputObject = GameObject.Find("MicMonitor");
        micIn = (MicrophoneInput) audioInputObject.GetComponent("MicrophoneInput");
    }

    void FixedUpdate () {
		timeSinceLastSpawn += Time.deltaTime;
		noteMonitor = micIn.frequency;
		float loudness = micIn.loudness;

		if(timeSinceLastSpawn >= spawnRate && !playingNote){
			canSpawn = true;
		} else if(timeSinceLastSpawn >= spawnRate && playingNote){
			// if current note changes, spawn
			if( noteMonitor > currentNote + 10f || noteMonitor < currentNote - 10f){
				canSpawn = true;
			}
		} else {
			canSpawn = false;
		}
     
        if (loudness > threshold && canSpawn) {
            SpawnNote(loudness);
        }
		if(loudness < threshold && playingNote) {
			playingNote = false;
		}
		if (Input.GetAxis("LeftController") > 0 && !noteBufferOpen){
			noteBufferOpen = true;
				noteBuffer = "";
			print("buffer open");
		} else if(noteBufferOpen){
			timeSinceNoteBufferStarted += Time.deltaTime;
			if(timeSinceNoteBufferStarted >= noteBufferLength){
				noteBufferOpen = false;
				print("buffer closed");
				timeSinceNoteBufferStarted = 0;
			}
		}
    }


    private void SpawnNote(float loudness) { 
		// print("notefinder.foundNote " + noteFinder.foundNote);

		if(
			micIn.frequency != 181 && 
			micIn.frequency != 175 && 
			micIn.frequency > 20
			){
			Vector3 scale = new Vector3(loudness, loudness, loudness);
			
			foreach(Material mat in noteMaterials){
				// print(mat.name);
				if(mat.name == noteFinder.foundNote){
					newNoteMat = mat;
					print("found mat. newNoteMat =  " + newNoteMat);
				} else {
					print("no mat found");
				}
			}
			GameObject newNote = (GameObject)Instantiate(objectToSpawn, this.transform.position, this.transform.rotation);
			newNote.transform.localScale += scale;
			newNote.GetComponent<Renderer>().material = newNoteMat;
			Rigidbody rb = newNote.GetComponent<Rigidbody>();
			rb.AddRelativeForce(Vector3.forward * thrust * loudness);
			timeSinceLastSpawn = 0f;
			currentNote = micIn.frequency;
			playingNote = true;
			// print("current note [" + notePlayedIndex +"] " + currentNote);
			notePlayedIndex++;
			if(noteBufferOpen){
				noteBuffer += noteFinder.foundNote;
			}
			if(noteBuffer.Contains("C4E4G4")){
				print("C major chord");
				spawner.majorChordPlayed = true;
			}
		}
    }
}