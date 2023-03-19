using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 jumpForce; // �leri ve havaya itecek kuvvet. (y ve z kuvveti gerek)

    [SerializeField] private Vector3 angularVelocity; // D�nme h�z�m�z. (x eksenine etki gerek)

    [SerializeField] private Vector3 pushBackForce; // Geri iti�te kullan�lacak kuvvet. (-z ve y gerekli gibi)

    [SerializeField] private float angularSpeedSlowerFactor = 0.3f; // B��a�� yava�latan de�er.

    [SerializeField] private sliceObj sliceObj;

    private Rigidbody rb;

    private bool canStuck = false;

    private bool falling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.ResetCenterOfMass(); // Bu ve alttaki sat�r e�er parent obje bo� ise gereksiz. Ama parent obje b��a��n sap� ise gerekli.
        rb.ResetInertiaTensor();
    }

    private void FixedUpdate()
    {
        rb.inertiaTensorRotation = Quaternion.identity; // Bunu internette buldum d�nmeyi stabil hale getirmeye yar�yor san�r�m. G�zle g�remedim etkisini test edece�im.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        CheckFall();

        AngularVelocityControl2();

        print(transform.forward);
    }

    private void AngularVelocityControl() // Sadece kar��ya bakarken yava�lama
    {
        if (canStuck) // Saplanabiliyorsak havaday�z demektir. B�ylece sapland���m�zda bir h�z kontrol� yapm�yoruz
        {
            if (falling) // D��erken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;

                float startRotationZ = 0.88f;

                if (rotationZ >= startRotationZ) // B��a��n kar��ya bakt��� durum.
                {
                    float wantedVelocityX = angularVelocity.x * angularSpeedSlowerFactor;
                    float clampedAngulerVelocityX = Mathf.Clamp(rb.angularVelocity.x, -wantedVelocityX, wantedVelocityX);

                    rb.angularVelocity = new Vector3(clampedAngulerVelocityX, rb.angularVelocity.y, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = angularVelocity;
                }
            }

        }

    }

    private void AngularVelocityControl2() // a�a��ya d��erkende yava�lama dahil
    {
        if (canStuck) // Saplanabiliyorsak havaday�z demektir. B�ylece sapland���m�zda bir h�z kontrol� yapm�yoruz
        {
            if (falling) // D��erken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;
                float rotationY = transform.forward.y;

                float startRotationZ = 0.88f;
                float startRotationY = 0;
                float endRotationY = -1;

                bool knifeStartedLookForward = rotationZ >= startRotationZ; // B��ak kar��ya bakmaya ba�lad�
                bool knifeStartedFall = (rotationY <= startRotationY && rotationY >= endRotationY) && rotationZ > 0; // B��ak kar��ya bakarken d��meye ba�lad� ve keskin taraf en alta bakana kadar bu ko�ul sa�lan�yor.
                if (knifeStartedLookForward || knifeStartedFall)
                {
                    float wantedVelocityX = angularVelocity.x * angularSpeedSlowerFactor;
                    float clampedAngulerVelocityX = Mathf.Clamp(rb.angularVelocity.x, -wantedVelocityX, wantedVelocityX);

                    rb.angularVelocity = new Vector3(clampedAngulerVelocityX, rb.angularVelocity.y, rb.angularVelocity.z);
                }
                else
                {
                    rb.angularVelocity = angularVelocity;
                }
            }

        }
    }

    private void AngularVelocityControl3() // Lerp ile
    {
        if (canStuck) // Saplanabiliyorsak havaday�z demektir. B�ylece sapland���m�zda bir h�z kontrol� yapm�yoruz
        {
            if (falling) // D��erken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;
                float rotationY = transform.forward.y;

                float startRotationZ = 0.88f;
                float startRotationY = 0;
                float endRotationY = -1;

                bool knifeStartedLookForward = rotationZ >= startRotationZ; // B��ak kar��ya bakmaya ba�lad�
                bool knifeStartedFall = (rotationY <= startRotationY && rotationY >= endRotationY) && rotationZ > 0; // B��ak kar��ya bakarken d��meye ba�lad� ve keskin taraf en alta bakana kadar bu ko�ul sa�lan�yor.
                if (knifeStartedLookForward || knifeStartedFall)
                {
                    float wantedVelocityX = angularVelocity.x * angularSpeedSlowerFactor;

                    float clampedAngulerVelocityX = Mathf.Clamp(rb.angularVelocity.x, -wantedVelocityX, wantedVelocityX);
                    float v = Mathf.Lerp(rb.angularVelocity.x, clampedAngulerVelocityX, Time.deltaTime);
                    float lerpedVelocityX = v;

                    rb.angularVelocity = new Vector3(lerpedVelocityX, rb.angularVelocity.y, rb.angularVelocity.z);

                }
                else
                {
                    //rb.angularVelocity = angularVelocity;
                    float lerpedVelocityX = Mathf.Lerp(rb.angularVelocity.x, angularVelocity.x, Time.deltaTime);

                    rb.angularVelocity = new Vector3(lerpedVelocityX, rb.angularVelocity.y, rb.angularVelocity.z);
                }
            }

        }
    }

    private void Jump()
    {
        rb.isKinematic = false; // Z�plad���m�zda ileri gidi�e izin veriyoruz ve rigidbody'i a��yoruz.

        rb.angularVelocity = Vector3.zero;

        rb.velocity = Vector3.zero;

        rb.AddForce(jumpForce, ForceMode.Impulse); // Z�plama kuvveti ekliyoruz.

        rb.angularVelocity = angularVelocity;   // A��sal h�z� de�i�tiriyoruz.

        sliceObj.ResetExplosionForce(); // Kesme kuvvetini resetliyoruz.

        StartCoroutine(Jump_Coroutine());
    }

    private IEnumerator Jump_Coroutine() // Bir s�re bekletip saplanabilmeyi a��yoruz b�ylece sapl� iken z�plamaya �al��t���m�zda tak�lmayaca��z.
    {
        float timer = 0.3f;
        canStuck = false; // Z�plad���m�zda tekrar saplanabilmeyi kapat�yoruz.
        yield return new WaitForSecondsRealtime(timer);
        canStuck = true; // Bir s�re sonra a��yoruz.
        yield break;

    }

    private void CheckFall() // Serbest d���� kontrol�.
    {
        if (canStuck)
        {
            float velocityY = rb.velocity.y;

            if (velocityY < 0)
            {
                falling = true;
            }
            else
            {
                falling = false;
            }

        }
    }

    public void PushBack()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(pushBackForce, ForceMode.Impulse);
    }

    public void Stuck() // Saplanma , t�m hareketler kapan�yor ve h�zlar s�f�rlan�yor.
    {
        if (canStuck) // Saplanabiliyorsak.
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            canStuck = false;
        }
    }
}
