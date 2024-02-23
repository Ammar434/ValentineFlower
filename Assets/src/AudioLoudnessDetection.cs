using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int samplesWindows = 64;
    private AudioClip microphoneClip;


    public void MicrophoneToAudioCLip()
    {
        if (Microphone.devices.Length > 0)
        {
            string device = Microphone.devices[0];
            microphoneClip = Microphone.Start(device, true, 10, AudioSettings.outputSampleRate);

        }
        else
        {
            Debug.Log("No microphone devices found.");
        }

    }
    public float GetLoudnessFromMicrophone()
    {
        if (microphoneClip == null)
        {
            Debug.LogError("Microphone clip is not initialized. Make sure to call MicrophoneToAudioCLip first.");
            return 0;
        }
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);

    }
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - samplesWindows;


        float[] samples = new float[samplesWindows];
        clip.GetData(samples, startPosition);
        float sum = 0;
        for (int i = 0; i < samplesWindows; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        return sum / samplesWindows;
    }
    private void OnDestroy()
    {
        Microphone.End(null);
    }
}
