using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startHologram : MonoBehaviour
{
    public GameObject hologramStand;
    public GameObject hologram;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hologram.GetComponent<SkinnedMeshRenderer>().enabled = true;
            hologramStand.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hologram.GetComponent<SkinnedMeshRenderer>().enabled = false;
            hologramStand.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Animator>().Rebind();
            gameObject.GetComponent<Animator>().enabled = false;
            //gameObject.GetComponent<AudioSource>().Pause();
        }
    }
}
