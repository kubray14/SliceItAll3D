using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CuttableObject cuttableObject)) // Kesilen obje ile etkile�im.
        {
            // Cut Here
            if (playerMovement.IsCutting() == false) // E�er kesme i�lemi ba�lamad�ysa 
            {
                playerMovement.StartCutting(); // Kesme i�lemi ba�l�yor.
            }
            Destroy(cuttableObject.gameObject);
        }
        else if (other.gameObject.tag == "Ground") // Yere saplanma
        {
            playerMovement.StickToGround();
            print("Collided with ground");
        }
    }
}