using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 impact;
    private Vector3 dampingVelocity;

    private float verticalVelocity;

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

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, smoothTime);

        if (navAgent != null)
        {
            if (impact.sqrMagnitude <= 0.2f*0.2f)
            {
                impact = Vector3.zero;
                navAgent.enabled = true;
            }

            /*
            if (impact == Vector3.zero)            
                navAgent.enabled = true;
            */            
        }        
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if(navAgent != null)
        {
            navAgent.enabled = false;
        }
    }
}
