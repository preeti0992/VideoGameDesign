using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class ZbsAudio : MonoBehaviour
{
    public ZbSSounds[] sounds;
    private AudioSource ads;

    void Awake()
    {
        foreach (ZbSSounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }

    }
    public void Play(string name)
    {
        //FindObjectOfType<AudioManager>().Play("ZbScream");  
        ZbSSounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
    // Update is called once per frame
    void Update()
    {

    }
}
