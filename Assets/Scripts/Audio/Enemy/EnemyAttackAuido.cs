using System;
using System.Collections;
using UnityEngine;

public class EnemyAttackAuido : MonoBehaviour
{
    public Sound[] sounds;
    AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    EnemyController enemyController;


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
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
        if (enemyController.isPlayerInRange == true &&
             audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }

    public void Play(string name)
    {
        Debug.Log("Audio Name : " + name);

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
       // StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1f);
    }


}
