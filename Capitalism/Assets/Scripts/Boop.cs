using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boop : MonoBehaviour
{
    AudioSource a;
    public AudioClip sound;
    static bool once = false;
    private void Start()
    {
        a = gameObject.AddComponent<AudioSource>();
        a.clip = sound;
    }
    public void BoopTheSnoot()
    {
        if (CameraController.self.state == CameraController.State.Default)
        {
            a.Play();
            if (once == false)
            {
                once = true;
                Player.stress--;
            }
        }
    }
}
