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
            //slicedObjTop.gameObject.layer = LayerMask.NameToLayer("Sliced");                   // Kesilen objenin layer'�n� sliced yapt�k b�ylece b��ak ile etkile�ime girmeyecek.
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
           // slicedObjDown.gameObject.layer = LayerMask.NameToLayer("Sliced");                  // Kesilen objenin layer'�n� sliced yapt�k b�ylece b��ak ile etkile�ime girmeyecek.
            // slicedObjDown.GetComponent<Material>().color = matColors[Random.Range(0, matColors.Length)];
            AddComponent(slicedObjTop,false);
            AddComponent(slicedObjDown,false);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik ��nk� bir �st birde alt objesini ollu�turduk.
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
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik ��nk� bir �st birde alt objesini ollu�turduk.
            counter++;
        }
        sliceNum++;
    }
} 
