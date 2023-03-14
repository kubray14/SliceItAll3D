using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CuttableObject cuttableObject)) // Kesilen obje ile etkileþim.
        {
            // Cut Here
            if (playerMovement.IsCutting() == false) // Eðer kesme iþlemi baþlamadýysa 
            {
                playerMovement.StartCutting(); // Kesme iþlemi baþlýyor.
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