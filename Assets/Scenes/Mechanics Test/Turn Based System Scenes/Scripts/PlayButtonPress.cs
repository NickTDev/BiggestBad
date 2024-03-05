//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPress : MonoBehaviour
{
    [SerializeField] AudioClip _buttonSound;

    public void PlayButtonSound()
    {
        GameObject.Find("SoundManager").GetComponent<AudioSource>().PlayOneShot(_buttonSound);
    }
}
