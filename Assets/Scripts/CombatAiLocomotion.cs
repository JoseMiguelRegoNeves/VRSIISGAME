using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatAiLocomotion : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    //public float rotationSpeed = 5.0f; // Adjust this value as needed
    //public Vector3 minDistance = new Vector3(2, 2, 2);

    NavMeshAgent agent;
    Animator animator;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
                if (sqDistance > maxDistance * maxDistance)
                {
                    agent.destination = playerTransform.position;
                }
                timer = maxTime;
            }
        

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
