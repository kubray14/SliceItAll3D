using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private float sliceSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxRotateSpeed;
    [SerializeField] private float minRotateSpeed;
    [SerializeField] private float speedChangeTimer = 0.15f;
    [SerializeField] private bool isSlicing;
    [SerializeField] private bool isStuck;
    [SerializeField] private bool canStuck;
    [SerializeField] private bool isFalling;
    [SerializeField] private Vector3 pushBackForce = new Vector3(0, 3, -3);
    public List<GameObject> trails;
    [SerializeField] private Transform trailTransform;
    [SerializeField] private GameObject myTrail;

    private bool isSpeedIncreasing;
    private bool isSpeedDecreasing;


    private SliceController sliceController;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sliceController = GetComponentInChildren<SliceController>();
    }

    private void Start()
    {
        GameManager.Instance.OnLevelStart += GameManager_OnLevelStart;
        InputManager.Instance.OnClick += InputManager_OnClick; // InputManagerdaki OnClick eventine subscribe olduk.
    }

    //private void OnDisable()
    //{
    //    GameManager.Instance.OnLevelStart -= GameManager_OnLevelStart;
    //}

    private void GameManager_OnLevelStart()
    {
        transform.parent = null; // KnifeSelecter'in parentligindan cikiyoruz.
        if (!gameObject.activeInHierarchy)
        {
            InputManager.Instance.OnClick -= InputManager_OnClick; // InputManagerdaki OnClick eventine subscribe olduk.
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnClick -= InputManager_OnClick; // Sahne Geçiþlerinde sorun çýkmamasý için unsubscribe olduk.
    }

    private void InputManager_OnClick()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        CheckFalling();
        RotateControl();
    }

    public void Jump()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            sliceController.sliceNum = 0;
            rb.isKinematic = false;
            isSlicing = false;
            isFalling = false;
            isStuck = false;
            rotateSpeed = maxRotateSpeed;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * forwardSpeed, ForceMode.Impulse);
            StartCoroutine(StuckProtect_Coroutine(0.4f));
            DOTween.KillAll(); // LerpRotateSpeed metodu çalýþýyorsa kapatýyoruz. 
        }

    }

    private void CheckFalling()
    {
        if (rb.velocity.y <= 0)
        {
            isFalling = true;
        }
        else
        {
            DOTween.KillAll(); // LerpRotateSpeed metodu çalýþýyorsa kapatýyoruz. 
        }
    }


    private void RotateControl()
    {
        //print(transform.rotation.eulerAngles.x); 
        if (!isSlicing) // Kesmiyorsak.
        {
            if (!isStuck) // Ve Saplanmadýysak dönüþ yapýyoruz.
            {
                if (isFalling) // Dönüþ hýzý sadece düþerken kontrol ediliyor.
                {
                    if (transform.rotation.eulerAngles.x >= 0 && transform.rotation.eulerAngles.x <= 90 && transform.rotation.eulerAngles.y == 0 && transform.rotation.eulerAngles.z == 0) // Yavaþlamamýz gereken açý
                    {
                        if (!isSpeedDecreasing) //Bu açýya ilk girdiðimizde bu deðer true oluyor ve 1 kere yavaþlama metodunu çaðýrýyoruz.
                        {
                            print("Azalacak");
                            LerpRotateSpeed(minRotateSpeed, speedChangeTimer);
                            isSpeedDecreasing = true;
                            isSpeedIncreasing = false;
                        }

                    }
                    else  // Yavaþlamamýz gereken açýdan çýktýðýmýzda.
                    {
                        if (!isSpeedIncreasing) // Ayný þekilde hýzlanmamýzý saðlayan deðer true oluyor ve 1 kere hýzlanma metodunu çaðýrýyoruz.
                        {
                            LerpRotateSpeed(maxRotateSpeed, speedChangeTimer);
                            isSpeedIncreasing = true;
                            isSpeedDecreasing = false;
                        }

                    }
                }

                transform.Rotate(rotateSpeed * 360 * Time.deltaTime, 0, 0, Space.Self);

            }
        }
        else // Kesiyorsak kesme pozisyonu alýyoruz.
        {
            if (transform.rotation.eulerAngles.x >= 15 && transform.rotation.eulerAngles.x <= 90 && transform.rotation.eulerAngles.y == 0 && transform.rotation.eulerAngles.z == 0) // Düzgün bir kesme açýsýndaysak.
            {
                //transform.Rotate(sliceSpeed * 360 * Time.deltaTime, 0, 0, Space.Self);
                return;
            }
            else
            {
                //float sliceAngle = 25f;
                //if (transform.rotation.eulerAngles.x >= 270 || transform.rotation.eulerAngles.x <= sliceAngle) // Býçaðýn ucu 25 derece ön aþaðýya bakana kadar. Yani kesme pozisyonu
                //{
                transform.Rotate(rotateSpeed * 360 * Time.deltaTime, 0, 0, Space.Self); // Döndürmeye devam ediyoruz.
                //}
            }
        }
    }


    private IEnumerator StuckProtect_Coroutine(float time)
    {
        canStuck = false;
        yield return new WaitForSecondsRealtime(time);
        canStuck = true;
        yield break;
    }

    private void LerpRotateSpeed(float newSpeed, float time) // rotateSpeed'i newSpeed'e time sürede getiriyor.
    {
        DOTween.To(() => rotateSpeed, (newSpeed) => rotateSpeed = newSpeed, newSpeed, time);
    }

    public void Stuck()
    {
        if (canStuck)
        {
            SoundManager.Instance.PlayStuckSound();
            sliceController.sliceNum = 0;
            isSlicing = false;
            isStuck = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    public void SliceStarted()
    {
        isSlicing = true;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    public void SelectTrail(int trailIndex)
    {
        if (trailTransform != null)
        {
            if (myTrail != null)
            {
                myTrail.gameObject.SetActive(false);
            }
            myTrail = trails[trailIndex];
            myTrail.gameObject.SetActive(true);
        }
    }

    public void PushBack() // Sapýn temas halinde geri tepmesi.
    {
        isSlicing = false;
        rb.velocity = Vector3.zero;
        rb.AddForce(pushBackForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable iHittable))
        {
            iHittable.Hit(this, false);
            SoundManager.Instance.PlayHandleHitSound();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out IHittable iHittable))
    //    {
    //        iHittable.Hit(this, false);
    //        SoundManager.Instance.PlayHandleHitSound();
    //    }
    //}
}
