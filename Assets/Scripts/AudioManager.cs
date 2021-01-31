using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audiomanager : MonoBehaviour
{
    public [] clips;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (AudioClip c in clips)
        {
            s.source = gameObject.AddComponent<AudionSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;


        }
    }

    // Update is called once per frame
    void Play(string name)
    {
        Sound s = Array.Find(clips, sound => sound.name == name);
        s.source.Play()
    }
}
