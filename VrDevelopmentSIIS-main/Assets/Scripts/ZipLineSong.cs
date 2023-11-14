using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLineSong : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        audio.PlayOneShot(clip);
    }

    private void OnCollisionExit(Collision collision)
    {

        //audio.Stop();
    }
}
