using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class NetworkGrab : NetworkBehaviour
{
    [SerializeField]
    private XRGrabInteractable xrGrabInteractable; // Reference to XRGrabInteractable

    private Rigidbody rb; // Reference to the rigidbody of the object being grabbed

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true; // Ensure that the object's Rigidbody is initially kinematic
        }

        if (xrGrabInteractable != null)
        {
            // Attach XRGrabInteractable to this GameObject if it's not already attached
            if (xrGrabInteractable.gameObject != gameObject)
            {
                xrGrabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            }

            // Subscribe to XRGrabInteractable events using the new event system
            xrGrabInteractable.selectEntered.AddListener(OnSelectEnterWrapper);
            xrGrabInteractable.selectExited.AddListener(OnSelectExitWrapper);
        }
        else
        {
            Debug.LogError("XRGrabInteractable is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any custom logic here if needed
    }

    // Called when the object is selected/grabbed
    private void OnSelectEnterWrapper(SelectEnterEventArgs args)
    {
        OnSelectEnter();
    }

    // Called when the object is released/ungrabbed
    private void OnSelectExitWrapper(SelectExitEventArgs args)
    {
        OnSelectExit();
    }

    // Your original OnSelectEnter and OnSelectExit methods
    private void OnSelectEnter()
    {
        // Check if this object is owned by the local player before allowing interaction
        if (IsOwner)
        {
            // Disable the object's Rigidbody kinematic property to allow physics interaction
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void OnSelectExit()
    {
        // Check if this object is owned by the local player before allowing interaction
        if (IsOwner)
        {
            // Re-enable the object's Rigidbody kinematic property to disable physics interaction
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    // Other network-related methods and properties can be implemented here
}
