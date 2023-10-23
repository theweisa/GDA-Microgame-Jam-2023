using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : UnitySingleton<AudioManager>
{
    // public AudioSource 
    public List<AudioChild> sounds = new List<AudioChild>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in this.transform)
        {
            sounds.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string id)
    {
        foreach (AudioChild child in sounds)
        {
            if (child.id == id)
            {
                child.Play();
                return;
            }
        }
    }
}

[System.Serializable]
public class AudioChild {
    public string id;
    public AudioSource sound;

    AudioChild(string newId, AudioSource newSound, float newVolume) {
        id = newId;
        sound = newSound;
        sound.volume = newVolume;
    }

    public void Play() {
        this.sound.Play();
    }
}