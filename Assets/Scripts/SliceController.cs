using EzySlice;
using System.Collections;
using UnityEngine;

public class SliceController : MonoBehaviour
{
    public Material[] sliceMats;
    public float explosionForce = 150;
    public float explosionRadius;

    public bool gravity, kinematik;
    int counter = 0;
    public int sliceNum = 0;
    private PlayerController playerController;


    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesilece�i nereye do�ru kesilece�i ve kesilen b�lge materyali
    }

    void AddComponent(GameObject obj, bool isStable)
    {
        if (isStable)
        {
            return;
        }
        else
        {
            var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere d��mesini ve hareketini kontrol edebilmek i�in rigidbody
            rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yapt�k.
            obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi i�in collider
            rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 par�an�n birbirinden uzakla�mas� kodu
            StartCoroutine(waitForRb(0.1f, rigidbody));
            rigidbody.isKinematic = kinematik;
        }
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay i�in kulland�k ��nk� �nce belli bir mesafe birbirlerinden uzakla��p d��meleri i�in.
    {
        yield return new WaitForSeconds(sec);
        rb.useGravity = gravity;
        yield break;
    }

    private void sliceNumControl()
    {
        if (sliceNum >= 3)
        {
            playerController.SliceStarted();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable iHittable))
        {
            iHittable.Hit(playerController, true);
            if (other.gameObject.TryGetComponent(out CuttableObject cuttableObject))
            {
                sliceNumControl();
                if (counter >= sliceMats.Length)
                {
                    counter = 0;
                }

                SlicedHull sliceObj = Slice(other.gameObject, sliceMats[counter]);
                GameObject slicedObjTop = sliceObj.CreateUpperHull(other.gameObject, sliceMats[counter]);
                GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
                if (cuttableObject.isCuttingEqual)
                {
                    AddComponent(slicedObjTop, false);
                    AddComponent(slicedObjDown, false);
                }
                else
                {
                    AddComponent(slicedObjTop, false);
                    AddComponent(slicedObjDown, true);
                }
                cuttableObject.ShowScoreCanvas(transform.position);
                Destroy(other.gameObject);
                counter++;
                sliceNum++;
            }
        }   
    }
} 
