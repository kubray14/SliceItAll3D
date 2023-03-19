using EzySlice;
using System.Collections;
using UnityEngine;

public class sliceObj : MonoBehaviour
{
    public Material[] sliceMats;                                                                    // random renk olacak sonrasýnda
    public float explosionForce = 150;
    public float explosionRadius;
    private float explosionForceStart;

    public bool gravity, kinematik;
    int counter = 0;

    private void Start()
    {
        explosionForceStart = explosionForce;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("canSlice"))
        {
            if (counter >= sliceMats.Length)
            {
                counter = 0;
            }
            float destroyTimer = 2f;
            SlicedHull sliceObj = Slice(other.gameObject, sliceMats[counter]);
            GameObject slicedObjTop = sliceObj.CreateUpperHull(other.gameObject, sliceMats[counter]);
            slicedObjTop.gameObject.layer = LayerMask.NameToLayer("Sliced");                   // Kesilen objenin layer'ýný sliced yaptýk böylece býçak ile etkileþime girmeyecek.
            Destroy(slicedObjTop.gameObject, destroyTimer);
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
            slicedObjDown.gameObject.layer = LayerMask.NameToLayer("Sliced");                  // Kesilen objenin layer'ýný sliced yaptýk böylece býçak ile etkileþime girmeyecek.
            Destroy(slicedObjDown.gameObject, destroyTimer);
            // slicedObjDown.GetComponent<Material>().color = matColors[Random.Range(0, matColors.Length)];
            AddComponent(slicedObjTop);
            AddComponent(slicedObjDown);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik çünkü bir üst birde alt objesini olluþturduk.
            counter++;
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesileceði nereye doðru kesileceði ve kesilen bölge materyali
    }

    void AddComponent(GameObject obj)
    {                                                                                         // kesilen objelere hangi komponentleri vermek istiyorsak onlarý eklediðimzi method
        var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere düþmesini ve hareketini kontrol edebilmek için rigidbody
        rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yaptýk.
        obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi için collider
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 parçanýn birbirinden uzaklaþmasý kodu
        explosionForce -= 15;                                                                 // her seferinde gücü azaltýyoruz ki kesilme efekti daha iyi otursun ve oyundaki gibi üst üste dökülerek düþsünler.
        StartCoroutine(waitForRb(0.1f, rigidbody));
        rigidbody.isKinematic = kinematik;
        // obj.tag = "canSlice";                                                               // tekrar kesilebilir olmasý için tag ama bize gerek yok
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay için kullandýk çünkü önce belli bir mesafe birbirlerinden uzaklaþýp düþmeleri için.
    {
        yield return new WaitForSeconds(sec);
        rb.useGravity = gravity;
        yield break;
    }

    public void ResetExplosionForce()
    {
        explosionForce = explosionForceStart;
    }
}

