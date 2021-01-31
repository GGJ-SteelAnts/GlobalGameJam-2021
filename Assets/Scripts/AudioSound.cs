using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch = 0f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
