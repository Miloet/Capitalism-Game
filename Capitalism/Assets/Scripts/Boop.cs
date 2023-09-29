using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boop : MonoBehaviour
{
    AudioSource a;
    public AudioClip sound;
    private void Start()
    {
        a = gameObject.AddComponent<AudioSource>();
        a.clip = sound;
    }
    public void BoopTheSnoot()
    {
        a.Play();
    }
}
