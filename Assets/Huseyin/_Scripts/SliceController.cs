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
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesileceði nereye doðru kesileceði ve kesilen bölge materyali
    }

    void AddComponent(GameObject obj, bool isStable)
    {
        if (isStable)
        {
            return;
        }
        else
        {
            var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere düþmesini ve hareketini kontrol edebilmek için rigidbody
            rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yaptýk.
            obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi için collider
            rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 parçanýn birbirinden uzaklaþmasý kodu
            StartCoroutine(waitForRb(0.1f, rigidbody));
            rigidbody.isKinematic = kinematik;
        }
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay için kullandýk çünkü önce belli bir mesafe birbirlerinden uzaklaþýp düþmeleri için.
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
        }

        if (other.gameObject.TryGetComponent(out CuttableObject cuttableObject))
        {
            sliceNumControl();
            if (counter >= sliceMats.Length)
            {
                counter = 0;
            }

            SlicedHull sliceObj = Slice(other.gameObject, sliceMats[counter]);
            GameObject slicedObjTop = sliceObj.CreateUpperHull(other.gameObject, sliceMats[counter]);
            //slicedObjTop.gameObject.layer = LayerMask.NameToLayer("Sliced");                   // Kesilen objenin layer'ýný sliced yaptýk böylece býçak ile etkileþime girmeyecek.
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
           // slicedObjDown.gameObject.layer = LayerMask.NameToLayer("Sliced");                  // Kesilen objenin layer'ýný sliced yaptýk böylece býçak ile etkileþime girmeyecek.
            // slicedObjDown.GetComponent<Material>().color = matColors[Random.Range(0, matColors.Length)];
            AddComponent(slicedObjTop,false);
            AddComponent(slicedObjDown,false);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik çünkü bir üst birde alt objesini olluþturduk.
            counter++;
        }

        if (other.gameObject.CompareTag("canSlice"))
        {
            sliceNumControl();
            if (counter >= sliceMats.Length)
            {
                counter = 0;
            }
            SlicedHull sliceObj = Slice(other.gameObject, sliceMats[counter]);
            GameObject slicedObjTop = sliceObj.CreateUpperHull(other.gameObject, sliceMats[counter]);
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
            AddComponent(slicedObjTop,false);
            AddComponent(slicedObjDown,true);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik çünkü bir üst birde alt objesini olluþturduk.
            counter++;
        }
        sliceNum++;
    }
} 
