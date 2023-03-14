using EzySlice;
using System.Collections;
using UnityEngine;

public class sliceObj : MonoBehaviour
{
    public Material materialSliceSide;                                                         // kesilen b�lgenin rengi, materyali
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
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik ��nk� bir �st birde alt objesini ollu�turduk.
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesilece�i nereye do�ru kesilece�i ve kesilen b�lge materyali
    }

    void AddComponent(GameObject obj){                                                        // kesilen objelere hangi komponentleri vermek istiyorsak onlar� ekledi�imzi method
        var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere d��mesini ve hareketini kontrol edebilmek i�in rigidbody
        rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yapt�k.
        obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi i�in collider
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 par�an�n birbirinden uzakla�mas� kodu
        explosionForce -= 15;                                                                 // her seferinde g�c� azalt�yoruz ki kesilme efekti daha iyi otursun ve oyundaki gibi �st �ste d�k�lerek d��s�nler.
        StartCoroutine(waitForRb(0.1f,rigidbody));
        rigidbody.isKinematic = kinematik;
       // obj.tag = "canSlice";                                                               // tekrar kesilebilir olmas� i�in tag ama bize gerek yok
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay i�in kulland�k ��nk� �nce belli bir mesafe birbirlerinden uzakla��p d��meleri i�in.
    {
        yield return new WaitForSeconds(sec);
        rb.useGravity = gravity;
    }
}
