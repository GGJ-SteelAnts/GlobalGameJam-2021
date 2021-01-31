using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        public static AudioClip BoxDrop01, BoxDrop02, ItemDrop01, ItemDrop02, ItemPickup01, ItemPickup02, Jump01, Jump02, Jump03, Lose, Push01, Theme, Win;
    static AudioSource audioSrc;

    ThemeSong = Audio.load<AudioClip>("Theme")
          BoxDrop01Sound = Audio.load<AudioClip>("BoxDrop01")
          BoxDrop02Sound = Audio.load<AudioClip>("BoxDrop02")
          ItemDrop01Sound = Audio.load<AudioClip>("ItemDrop01")
          ItemDrop02Sound = Audio.load<AudioClip>("ItemDrop02")
          ItemPickup01Sound = Audio.load<AudioClip>("ItemPickup01")
          ItemPickup02Sound = Audio.load<AudioClip>("ItemPickup02")
          Jump01Sound = Audio.load<AudioClip>("Jump01")
          Jump02Sound = Audio.load<AudioClip>("Jump02")
          Jump03Sound = Audio.load<AudioClip>("Jump03")
          LoseSound = Audio.load<AudioClip>("Lose")
          Push01Sound = Audio.load<AudioClip>("Push01")
         WinSound = Audio.load<AudioClip>("Win")

        audiosrc = GetComponent<audiosource>();
    }

// Update is called once per frame
void Update()
{

}
public static void PlaySound(string clip)
{
    switch (clip)
    {
        case "BoxDrop":
            audioSrc.PlayOneShot(Dropbox01 || Dropbox02 || Dropbox03);
            break;
        case "ItemDrop":
            audioSrc.PlayOneShot(ItemDrop01 || ItemDrop02);
            break;
        case "ItemPickup":
            audioSrc.PlayOneShot(ItemPickup01 || ItemPickup02);
            break;
        case "ItemDrop01":
            audioSrc.PlayOneShot(ItemDrop01 || ItemDrop02);
            break;
        case "Jump":
            audioSrc.PlayOneShot(Jump01 || Jump02);
            break;
        case "Lose":
            audioSrc.PlayOneShot(Lose);
            break;
        case "win":
            audioSrc.PlayOneShot(Win);
            break;

    }
}
