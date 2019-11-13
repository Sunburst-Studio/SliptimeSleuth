using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_AudioManager : MonoBehaviour
{
    public static s_AudioManager instance;

    // all of the sounds!
    // make accessible in inspectors but not scripts
    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");

        }
        else instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + " " + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    // will loop through all of the sounds and play it
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager : The following sound not found in list of sounds: " + _name);
    }
}

[System.Serializable]
public class Sound
{
    // name of the sound to easily refer to the sound to play
    public string name;
    // the actual audio file
    public AudioClip clip;
    // reference to an object in the game that can play this
    private AudioSource source;

    // volume can only go 0 to 1
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    // if needed, random shid
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    // feed it an audio source reference
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    // play the clip
    public void Play()
    {
        //if needed, randomize line
        //source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));

        // if needed, randomize pitch
        //source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));

        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}