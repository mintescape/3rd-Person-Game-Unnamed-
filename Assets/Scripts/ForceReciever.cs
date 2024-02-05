using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    private float verticalVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private float drag;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
        if(impact == Vector3.zero && Agent != null)
        {
            Agent.enabled = true;
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if(Agent != null)
        {
            Agent.enabled = false;
        }
    }
}
