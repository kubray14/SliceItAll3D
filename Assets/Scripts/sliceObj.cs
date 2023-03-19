using EzySlice;
using System.Collections;
using UnityEngine;

public class sliceObj : MonoBehaviour
{
    public Material[] sliceMats;                                                                    // random renk olacak sonras�nda
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
            slicedObjTop.gameObject.layer = LayerMask.NameToLayer("Sliced");                   // Kesilen objenin layer'�n� sliced yapt�k b�ylece b��ak ile etkile�ime girmeyecek.
            Destroy(slicedObjTop.gameObject, destroyTimer);
            GameObject slicedObjDown = sliceObj.CreateLowerHull(other.gameObject, sliceMats[counter]);
            slicedObjDown.gameObject.layer = LayerMask.NameToLayer("Sliced");                  // Kesilen objenin layer'�n� sliced yapt�k b�ylece b��ak ile etkile�ime girmeyecek.
            Destroy(slicedObjDown.gameObject, destroyTimer);
            // slicedObjDown.GetComponent<Material>().color = matColors[Random.Range(0, matColors.Length)];
            AddComponent(slicedObjTop);
            AddComponent(slicedObjDown);
            Destroy(other.gameObject);                                                         // ana dokunulan objeyi yok ettik ��nk� bir �st birde alt objesini ollu�turduk.
            counter++;
        }
    }

    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.up, mat);                              // nereden  kesilece�i nereye do�ru kesilece�i ve kesilen b�lge materyali
    }

    void AddComponent(GameObject obj)
    {                                                                                         // kesilen objelere hangi komponentleri vermek istiyorsak onlar� ekledi�imzi method
        var rigidbody = obj.AddComponent<Rigidbody>();                                        // yere d��mesini ve hareketini kontrol edebilmek i�in rigidbody
        rigidbody.useGravity = false;                                                         // default rigidbody component gravitysini false yapt�k.
        obj.AddComponent<BoxCollider>();                                                      // tekrar kesilebilmesi i�in collider
        rigidbody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius); // 2 par�an�n birbirinden uzakla�mas� kodu
        explosionForce -= 15;                                                                 // her seferinde g�c� azalt�yoruz ki kesilme efekti daha iyi otursun ve oyundaki gibi �st �ste d�k�lerek d��s�nler.
        StartCoroutine(waitForRb(0.1f, rigidbody));
        rigidbody.isKinematic = kinematik;
        // obj.tag = "canSlice";                                                               // tekrar kesilebilir olmas� i�in tag ama bize gerek yok
    }

    private IEnumerator waitForRb(float sec, Rigidbody rb)                                    // Bu coroutini delay i�in kulland�k ��nk� �nce belli bir mesafe birbirlerinden uzakla��p d��meleri i�in.
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

