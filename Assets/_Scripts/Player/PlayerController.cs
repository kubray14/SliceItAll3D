using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 jumpForce; // Ýleri ve havaya itecek kuvvet. (y ve z kuvveti gerek)

    [SerializeField] private Vector3 angularVelocity; // Dönme hýzýmýz. (x eksenine etki gerek)

    [SerializeField] private Vector3 pushBackForce; // Geri itiþte kullanýlacak kuvvet. (-z ve y gerekli gibi)

    [SerializeField] private float angularSpeedSlowerFactor = 0.3f; // Býçaðý yavaþlatan deðer.

    [SerializeField] private sliceObj sliceObj;

    private Rigidbody rb;

    private bool canStuck = false;

    private bool falling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.ResetCenterOfMass(); // Bu ve alttaki satýr eðer parent obje boþ ise gereksiz. Ama parent obje býçaðýn sapý ise gerekli.
        rb.ResetInertiaTensor();
    }

    private void FixedUpdate()
    {
        rb.inertiaTensorRotation = Quaternion.identity; // Bunu internette buldum dönmeyi stabil hale getirmeye yarýyor sanýrým. Gözle göremedim etkisini test edeceðim.
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

    private void AngularVelocityControl() // Sadece karþýya bakarken yavaþlama
    {
        if (canStuck) // Saplanabiliyorsak havadayýz demektir. Böylece saplandýðýmýzda bir hýz kontrolü yapmýyoruz
        {
            if (falling) // Düþerken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;

                float startRotationZ = 0.88f;

                if (rotationZ >= startRotationZ) // Býçaðýn karþýya baktýðý durum.
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

    private void AngularVelocityControl2() // aþaðýya düþerkende yavaþlama dahil
    {
        if (canStuck) // Saplanabiliyorsak havadayýz demektir. Böylece saplandýðýmýzda bir hýz kontrolü yapmýyoruz
        {
            if (falling) // Düþerken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;
                float rotationY = transform.forward.y;

                float startRotationZ = 0.88f;
                float startRotationY = 0;
                float endRotationY = -1;

                bool knifeStartedLookForward = rotationZ >= startRotationZ; // Býçak karþýya bakmaya baþladý
                bool knifeStartedFall = (rotationY <= startRotationY && rotationY >= endRotationY) && rotationZ > 0; // Býçak karþýya bakarken düþmeye baþladý ve keskin taraf en alta bakana kadar bu koþul saðlanýyor.
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
        if (canStuck) // Saplanabiliyorsak havadayýz demektir. Böylece saplandýðýmýzda bir hýz kontrolü yapmýyoruz
        {
            if (falling) // Düþerken kontrol ediyoruz.
            {
                float rotationZ = transform.forward.z;
                float rotationY = transform.forward.y;

                float startRotationZ = 0.88f;
                float startRotationY = 0;
                float endRotationY = -1;

                bool knifeStartedLookForward = rotationZ >= startRotationZ; // Býçak karþýya bakmaya baþladý
                bool knifeStartedFall = (rotationY <= startRotationY && rotationY >= endRotationY) && rotationZ > 0; // Býçak karþýya bakarken düþmeye baþladý ve keskin taraf en alta bakana kadar bu koþul saðlanýyor.
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
        rb.isKinematic = false; // Zýpladýðýmýzda ileri gidiþe izin veriyoruz ve rigidbody'i açýyoruz.

        rb.angularVelocity = Vector3.zero;

        rb.velocity = Vector3.zero;

        rb.AddForce(jumpForce, ForceMode.Impulse); // Zýplama kuvveti ekliyoruz.

        rb.angularVelocity = angularVelocity;   // Açýsal hýzý deðiþtiriyoruz.

        sliceObj.ResetExplosionForce(); // Kesme kuvvetini resetliyoruz.

        StartCoroutine(Jump_Coroutine());
    }

    private IEnumerator Jump_Coroutine() // Bir süre bekletip saplanabilmeyi açýyoruz böylece saplý iken zýplamaya çalýþtýðýmýzda takýlmayacaðýz.
    {
        float timer = 0.3f;
        canStuck = false; // Zýpladýðýmýzda tekrar saplanabilmeyi kapatýyoruz.
        yield return new WaitForSecondsRealtime(timer);
        canStuck = true; // Bir süre sonra açýyoruz.
        yield break;

    }

    private void CheckFall() // Serbest düþüþ kontrolü.
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

    public void Stuck() // Saplanma , tüm hareketler kapanýyor ve hýzlar sýfýrlanýyor.
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
