using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NoteFinder : MonoBehaviour {
  public GameObject audioInputObject;
  public float threshold = 1.0f;
  public string text;
  public Text noteText;
  MicrophoneInput micIn;

  // Use this for initialization
  void Start () {
    if (audioInputObject == null)
      audioInputObject = GameObject.Find("MicMonitor");
    micIn = (MicrophoneInput) audioInputObject.GetComponent("MicrophoneInput");
  }
  
  // Update is called once per frame
  void Update () {
    int f = (int)micIn.frequency; // Get the frequency from our MicrophoneInput script
    if (f >= 261 && f <= 265) // Compare the frequency to known value, take possible rounding error in to account
    {
      noteText.text="Middle-C played!";
    }
    else
    {
      noteText.text="Play another note...";
    }
  }
}