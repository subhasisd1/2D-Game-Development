using UnityEngine.Audio;
using UnityEngine;
using System;

public class BackGroundMusic : MonoBehaviour
{
    public Sound[] sounds;
    AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            audioSource = s.source;

        }
       // FindObjectOfType<BackGroundMusic>().Play("MusicGamePlay");
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Debug.Log("Music Stoped");
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public void Play(string name)
    {
        Debug.Log("Audio Name : " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
