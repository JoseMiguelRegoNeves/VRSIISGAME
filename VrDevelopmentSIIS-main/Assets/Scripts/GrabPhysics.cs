using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GrabPhysics : MonoBehaviour
{
    public InputActionProperty grabInputSource;
    public float radius = 0.1f;
    public LayerMask grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;
    private Collider[] handColliders;


    void Start()
    {
        // Get all colliders attached to the hand
        handColliders = GetComponentsInChildren<Collider>();
    }


    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.5f;

        if (isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

            if (nearbyColliders.Length > 0)
            {
                // Find the closest collider to the hand
                float minDistance = Mathf.Infinity;
                Collider closestCollider = null;
                foreach (Collider collider in nearbyColliders)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestCollider = collider;
                    }
                }

                Rigidbody nearbyRigidbody = closestCollider.attachedRigidbody;

                // Disable the colliders on the hand
                foreach (Collider collider in handColliders)
                {
                    collider.enabled = false;
                }

                // Disable the Rigidbody component of the input source game object
                Rigidbody inputSourceRigidbody = GetComponent<Rigidbody>();
                inputSourceRigidbody.detectCollisions = false;

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if (nearbyRigidbody)
                {
                    fixedJoint.connectedBody = nearbyRigidbody;
                    fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position;
                }

                isGrabbing = true;
            }
        }
        else if (!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;

            // Re-enable the colliders on the hand
            foreach (Collider collider in handColliders)
            {
                collider.enabled = true;
            }

            // Re-enable the Rigidbody component of the input source game object
            Rigidbody inputSourceRigidbody = GetComponent<Rigidbody>();
            inputSourceRigidbody.detectCollisions = true;

            if (fixedJoint)
            {
                Destroy(fixedJoint);
            }
        }
    }
}
