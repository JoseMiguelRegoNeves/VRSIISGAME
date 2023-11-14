using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnDetach : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    public void ResetPosition()
    {
        Vector3 originalPosition = player.transform.position;
        gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
        gameObject.transform.position = originalPosition;
    }
}
