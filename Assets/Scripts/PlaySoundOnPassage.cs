using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnPassage : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        // Get the AudioSource component on the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object has a specific tag (optional)
        if (other.CompareTag("Player"))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                // Play the audio clip
                audioSource.Play();
            }
        }
    }
}
