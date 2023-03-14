using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedForward = 1.0f;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float jumpTorque = 1.0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovementForward();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void MovementForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * speedForward;
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;

        rb.AddTorque(transform.right * jumpTorque, ForceMode.Impulse);
    }
}
