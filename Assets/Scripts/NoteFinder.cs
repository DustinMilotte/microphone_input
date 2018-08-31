using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NoteFinder : MonoBehaviour {
  public GameObject audioInputObject;
  public float threshold = 1.0f;
  public string text;
  public Text noteText;
  MicrophoneInput micIn;
  public string foundNote;

  // Use this for initialization
  void Start () {
    if (audioInputObject == null)
      audioInputObject = GameObject.Find("MicMonitor");
    micIn = (MicrophoneInput) audioInputObject.GetComponent("MicrophoneInput");
  }
  
  // Update is called once per frame
  void Update ()
    {
        int freq = (int)micIn.frequency; // Get the frequency from our MicrophoneInput script
        foundNote = DetermineNote(freq);
    }

    private string DetermineNote(float freq)
    {
        if(freq >= 254.3f && freq <= 269.4f)
        {
          foundNote = "C4";
        }
        else if(freq >= 269.5f && freq <= 285.4f)
        {
          foundNote = "C#Db4";
        }
        else if(freq >= 285.5f && freq <= 302.4f)
        {
          foundNote = "D4";
        }
        else if(freq >= 302.5f && freq <= 320.4f)
        {
          foundNote = "D#Eb4";
        }
        else if(freq >= 320.5f && freq <= 339.4f)
        {
          foundNote = "E4";
        }
        else if(freq >= 339.5f && freq <= 359.6f)
        {
          foundNote = "F4";
        }
        else if(freq >= 359.7f && freq <= 381f)
        {
          foundNote = "F#Gb4";
        }
        else if(freq >= 381.1f && freq <= 403.7f)
        {
          foundNote = "G4";
        }
        else if(freq >= 403.8f && freq <= 427.7f)
        {
          foundNote = "G#Ab4";
        }
        else if(freq >= 427.8f && freq <= 453.1f)
        {
          foundNote = "A4";
        }
        else if(freq >= 453.2f && freq <= 480f)
        {
          foundNote = "A#Bb4";
        }
        else if(freq >= 480.1f && freq <= 508.6f)
        {
          foundNote = "B4";
        }
        else if(freq >= 508.7f && freq <= 538.81f)
        {
          foundNote = "C5";
        }
        else
        {
          foundNote = "";
        }
        noteText.text = foundNote;
        return foundNote;
    }
}