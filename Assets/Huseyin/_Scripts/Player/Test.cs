using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform centerOfMassTransform;

    public float torque;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMassTransform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            rb.ResetCenterOfMass();
            rb.angularVelocity = Vector3.zero;
            rb.AddTorque(Vector3.right * torque, ForceMode.Force);
           
        }
    }

}
