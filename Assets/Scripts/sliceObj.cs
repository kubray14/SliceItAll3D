using EzySlice;
using System.Collections;
using UnityEngine;

public class sliceObj : MonoBehaviour
{
    public Material materialSliceSide;                                                         // kesilen bölgenin rengi, materyali
    public float explosionForce = 150;
    public float explosionRadius;

    public bool gravity, kinematik;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canSlice"))
        {
            SlicedHull sliceObj = Slice(other.gameObject, materialSliceSide);
            GameObject slicedObjTop = sliceObj.CreateUpperHull(other.gameObject, materialSliceSide);
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, materialSliceSide);
            AddComponent(slicedObjTop);
            AddComponent(slicedObjDown);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik çünkü bir üst birde alt objesini olluþturduk.
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesileceði nereye doðru kesileceði ve kesilen bölge materyali
    }

    void AddComponent(GameObject obj){                                                        // kesilen objelere hangi komponentleri vermek istiyorsak onlarý eklediðimzi method
        var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere düþmesini ve hareketini kontrol edebilmek için rigidbody
        rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yaptýk.
        obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi için collider
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 parçanýn birbirinden uzaklaþmasý kodu
        explosionForce -= 15;                                                                 // her seferinde gücü azaltýyoruz ki kesilme efekti daha iyi otursun ve oyundaki gibi üst üste dökülerek düþsünler.
        StartCoroutine(waitForRb(0.1f,rigidbody));
        rigidbody.isKinematic = kinematik;
       // obj.tag = "canSlice";                                                               // tekrar kesilebilir olmasý için tag ama bize gerek yok
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay için kullandýk çünkü önce belli bir mesafe birbirlerinden uzaklaþýp düþmeleri için.
    {
        yield return new WaitForSeconds(sec);
        rb.useGravity = gravity;
    }
}
