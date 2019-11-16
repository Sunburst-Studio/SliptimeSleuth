using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class s_SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMuiscLevel(float sliderValue)
    {
        // set the exposed parameter, MusicVol, to the specific slider value
        // so now the slider value is on a logarithmic scale to the base of 10, because decibles, and the x 20 brings it up for us to hear properly 
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetGeneralLevel(float sliderValue)
    {
        mixer.SetFloat("GeneralVol", Mathf.Log10(sliderValue) * 20);
    }
}
