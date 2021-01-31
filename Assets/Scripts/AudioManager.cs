using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Audiomanager : MonoBehaviour
{
    public AudioSound[] clips;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (AudioSound c in clips)
        {
            c.source = gameObject.AddComponent<AudionSource>();
            c.source.clip = c.clip;
            c.source.volume = c.volume;
            c.source.pitch = c.pitch;
        }
    }

    // Update is called once per frame
    void Play(string name)
    {
        AudioSound s = Array.Find(clips, sound => sound.name == name);
        s.source.Play();
    }
}
