using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speedForward = 1.0f;

    [SerializeField] private float pushBackForce = 1.0f;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float jumpTorque = 1.0f;

    private bool canMoveForward = false;

    private bool jumped = false;

    private bool cutting = false;

    private Rigidbody rb;

    private Vector3 cutRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cutRotation = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Update()
    {
        MovementForward();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }
    }

    private void MovementForward()
    {
        if (canMoveForward)
        {
            transform.position += Vector3.forward * Time.deltaTime * speedForward;
        }

    }

    private void Jump()
    {
        if (jumped)
        {
            rb.isKinematic = false;

            rb.velocity = Vector3.up * jumpForce;

            rb.AddTorque(-transform.right * jumpTorque, ForceMode.Impulse);

            jumped = false;

            canMoveForward = true; // Zýpladýðýmýzda ileri gidiþe izin veriyoruz.
        }

    }

    private void PushBack()
    {
        rb.AddForce(Vector3.back * pushBackForce, ForceMode.Impulse);
    }

    public void StopTurn()
    {
        rb.angularVelocity = Vector3.zero;
    }

    public bool IsCutting()
    {
        return cutting;
    }

    public void StartCutting()
    {
        cutting = true;
        canMoveForward = false; // Keserken ilerlemeyi kapatýyoruz.
        StopTurn();
    }


    public void StopCutting()
    {
        cutting = false;
    }

    public void StickToGround() // Yere saplanma
    {
        canMoveForward = false;
        canMoveForward = false;
        cutting = false;

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CuttableObject cuttableObject))
        {
            PushBack();
        }
    }
}
