using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletWhileActive : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public AudioSource sound;

    private GameObject currentBullet;
    private GameObject newBullet;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(StartFiring);
        grabbable.deactivated.AddListener(StopFiring);
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFiring(ActivateEventArgs arg)
    {
        currentBullet = Instantiate(bulletPrefab);
        currentBullet.transform.position = spawnPoint.position;
        currentBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        InvokeRepeating("FireBullet", 0.1f, 0.1f);
    }

    public void StopFiring(DeactivateEventArgs arg){
        CancelInvoke();
        Destroy(currentBullet, 5);

    }

    public void FireBullet()
    {
        sound.Play();
        Destroy(currentBullet,5);
        newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = spawnPoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        currentBullet = newBullet;
    }
}
