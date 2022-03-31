using System;
using UnityEngine;

public class ButtonAudioController : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

    }

    // Update is called once per frame
    public void Play(SoundType soundType)
    {
        Debug.Log(soundType);
        Sound s = Array.Find(sounds, sound => sound.soundType == soundType);
        s.source.Play();
    }
}
