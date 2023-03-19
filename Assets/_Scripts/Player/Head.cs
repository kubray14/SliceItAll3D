using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable iHittable))
        {
            iHittable.Hit(playerController, true);
        }
    }
}