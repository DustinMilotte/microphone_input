using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
    public float sensitivity = 100;
    public float loudness = 0;
	AudioSource audioSource;
	public string audioInputDevice;
    int sampleRate;
    public float frequency;
    public Text freqText;
    public FFTWindow myFFTWindow;

    void Start() {
        // TODO replace with UI menu
        string [] mics = Microphone.devices;
            foreach(string mic in mics){print(mic);}
        myFFTWindow = FFTWindow.BlackmanHarris;
        sampleRate = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
		audioInputDevice = Microphone.devices[1].ToString();
		print("audio input device = " + audioInputDevice);
        audioSource.clip = Microphone.Start(audioInputDevice, true, 1, 44100);
        audioSource.loop = true; 
        // reduce latency by setting position to 0
        while (!(Microphone.GetPosition(audioInputDevice) > 0)){}
        audioSource.Play(); // Play the audio source!
    }

    void Update(){
        loudness = GetAveragedVolume() * sensitivity;
		frequency = GetFundamentalFrequency();
        freqText.text = frequency.ToString();
    }

    float GetAveragedVolume(){
        { 
            float[] data = new float[256];
            float a = 0;
            audioSource.GetOutputData(data,0);
            foreach(float s in data)
            {
                a += Mathf.Abs(s);
            }
            return a/256;
        }
    }

    float GetFundamentalFrequency(){
        float fundamentalFrequency = 0f;
        float [] data = new float[8192];
        audioSource.GetSpectrumData(data,0, myFFTWindow);
        float s = 0f;
        int i = 0;
        for(int j = 1; j < 8192; j++){
            if (s < data[j]){
                s = data[j];
                i = j;
            }
        }
        fundamentalFrequency = i * sampleRate / 8192;
        return fundamentalFrequency;
    }
}