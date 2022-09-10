using UnityEngine;
using System;

public class PlayerAudioController : MonoBehaviour
{
    public Sound[] sounds;
    AudioSource audioSource;
    private AudioClip audioClip;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            audioSource = s.source;
        }
    }

    private void Update()
    {
        if (playerController.horizontal != 0 &&
            playerController.isPGrounded == true  
            && audioSource.isPlaying == false )
        {
           audioSource.Play();
        }      
    }


    // Update is called once per frame
    public void Play(SoundType soundType)
    {
        Sound s = Array.Find(sounds, sound => sound.soundType == soundType);
        s.source.Play();
    }
}
