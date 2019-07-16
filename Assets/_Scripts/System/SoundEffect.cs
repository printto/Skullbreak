using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This class needs to be attached to the game object along with AudioSource component.
 * 
 */
public class SoundEffect : MonoBehaviour
{

    //Audio sources
    AudioSource audio;
    public AudioClip[] BGMSounds;
    public AudioClip[] LaneSounds;
    public AudioClip[] JumpSounds;
    public AudioClip[] HitSounds;
    public AudioClip[] CoinSounds;
    public AudioClip[] TeleportSounds;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    /*
     * Put in the audio source in SoundEffect class
     */
    public void PlaySound(AudioClip[] sounds)
    {
            if (sounds.Length != 0)
            {
                int randomed = Random.Range(0, sounds.Length);
                audio.PlayOneShot(sounds[randomed]);
            }
    }

}
